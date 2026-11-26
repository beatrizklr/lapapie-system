using Lapapie.Models;
using Lapapie.Models.Enums;
using Lapapie.Util;
using MySql.Data.MySqlClient;

namespace Lapapie.DAO
{
    /// <summary>Pedidos de compra a fornecedores e seus itens.</summary>
    public class PedidoCompraDAO
    {
        private const string SelectBase =
            @"SELECT pc.*, COALESCE(f.nome_fantasia, f.razao_social) AS fornecedor_nome, u.nome AS usuario_nome,
                     (SELECT COUNT(*) FROM itens_pedido_compra i WHERE i.pedido_compra_id = pc.id) AS qtd_itens
              FROM pedidos_compra pc
              JOIN fornecedores f ON f.id = pc.fornecedor_id
              JOIN usuarios u     ON u.id = pc.usuario_id";

        private static PedidoCompra Mapear(MySqlDataReader r) => new()
        {
            Id = r.Inteiro("id"),
            FornecedorId = r.Inteiro("fornecedor_id"),
            UsuarioId = r.Inteiro("usuario_id"),
            DataPedido = r.Data("data_pedido"),
            DataPrevistaEntrega = r.DataOuNulo("data_prevista_entrega"),
            DataRecebimento = r.DataOuNulo("data_recebimento"),
            Status = EnumBanco.DoBanco<StatusPedidoCompra>(r.Texto("status")),
            ValorTotal = r.Valor("valor_total"),
            Observacao = r.Texto("observacao"),
            FornecedorNome = r.Texto("fornecedor_nome"),
            UsuarioNome = r.Texto("usuario_nome"),
            QuantidadeItens = r.Inteiro("qtd_itens")
        };

        public List<PedidoCompra> Pesquisar(string texto, StatusPedidoCompra? status)
        {
            return DaoUtil.Executar(conexao =>
            {
                using var cmd = DaoUtil.Comando(conexao, "");
                var sql = SelectBase + " WHERE 1 = 1";
                if (!Validacao.Vazio(texto))
                {
                    sql += " AND (f.razao_social LIKE @texto OR f.nome_fantasia LIKE @texto OR pc.observacao LIKE @texto OR pc.id = @numero)";
                    cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
                    cmd.Parameters.AddWithValue("@numero", int.TryParse(texto.Trim().TrimStart('#'), out var n) ? n : -1);
                }
                if (status.HasValue)
                {
                    sql += " AND pc.status = @status";
                    cmd.Parameters.AddWithValue("@status", EnumBanco.ParaBanco(status.Value));
                }
                cmd.CommandText = sql + " ORDER BY pc.data_pedido DESC";
                return DaoUtil.Listar(cmd, Mapear);
            });
        }

        public PedidoCompra ObterPorId(int id)
        {
            return DaoUtil.Executar(conexao =>
            {
                PedidoCompra pedido;
                using (var cmd = DaoUtil.Comando(conexao, SelectBase + " WHERE pc.id = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    pedido = DaoUtil.Listar(cmd, Mapear).FirstOrDefault();
                }
                if (pedido == null) return null;
                pedido.Itens = ListarItens(conexao, null, id);
                return pedido;
            });
        }

        private static List<ItemPedidoCompra> ListarItens(MySqlConnection conexao, MySqlTransaction transacao, int pedidoId)
        {
            using var cmd = DaoUtil.Comando(conexao,
                @"SELECT i.*, p.nome FROM itens_pedido_compra i
                  JOIN produtos p ON p.id = i.produto_id
                  WHERE i.pedido_compra_id = @id ORDER BY p.nome", transacao);
            cmd.Parameters.AddWithValue("@id", pedidoId);
            return DaoUtil.Listar(cmd, r => new ItemPedidoCompra
            {
                PedidoCompraId = r.Inteiro("pedido_compra_id"),
                ProdutoId = r.Inteiro("produto_id"),
                Quantidade = r.Inteiro("quantidade"),
                CustoUnitario = r.Valor("custo_unitario"),
                ProdutoNome = r.Texto("nome")
            });
        }

        /// <summary>Grava pedido + itens em transação. O valor total é recalculado a partir dos itens.</summary>
        public int Inserir(PedidoCompra pedido)
        {
            pedido.RecalcularTotal();
            return DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                using (var cmd = DaoUtil.Comando(conexao,
                    @"INSERT INTO pedidos_compra (fornecedor_id, usuario_id, data_pedido, data_prevista_entrega, status, valor_total, observacao)
                      VALUES (@fornecedor, @usuario, @data, @previsao, 'ABERTO', @total, @obs)", transacao))
                {
                    cmd.Parameters.AddWithValue("@fornecedor", pedido.FornecedorId);
                    cmd.Parameters.AddWithValue("@usuario", pedido.UsuarioId);
                    cmd.Parameters.AddWithValue("@data", pedido.DataPedido);
                    cmd.Parameters.AddWithValue("@previsao", DaoUtil.Nulo(pedido.DataPrevistaEntrega?.Date));
                    cmd.Parameters.AddWithValue("@total", pedido.ValorTotal);
                    cmd.Parameters.AddWithValue("@obs", DaoUtil.Nulo(pedido.Observacao));
                    cmd.ExecuteNonQuery();
                    pedido.Id = (int)cmd.LastInsertedId;
                }
                GravarItens(conexao, transacao, pedido);
                return pedido.Id;
            });
        }

        public void Atualizar(PedidoCompra pedido)
        {
            pedido.RecalcularTotal();
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                GarantirAberto(conexao, transacao, pedido.Id);
                using (var cmd = DaoUtil.Comando(conexao,
                    @"UPDATE pedidos_compra SET fornecedor_id = @fornecedor, data_prevista_entrega = @previsao,
                             valor_total = @total, observacao = @obs
                      WHERE id = @id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", pedido.Id);
                    cmd.Parameters.AddWithValue("@fornecedor", pedido.FornecedorId);
                    cmd.Parameters.AddWithValue("@previsao", DaoUtil.Nulo(pedido.DataPrevistaEntrega?.Date));
                    cmd.Parameters.AddWithValue("@total", pedido.ValorTotal);
                    cmd.Parameters.AddWithValue("@obs", DaoUtil.Nulo(pedido.Observacao));
                    cmd.ExecuteNonQuery();
                }
                GravarItens(conexao, transacao, pedido);
            });
        }

        private static void GravarItens(MySqlConnection conexao, MySqlTransaction transacao, PedidoCompra pedido)
        {
            using (var apagar = DaoUtil.Comando(conexao, "DELETE FROM itens_pedido_compra WHERE pedido_compra_id = @id", transacao))
            {
                apagar.Parameters.AddWithValue("@id", pedido.Id);
                apagar.ExecuteNonQuery();
            }
            using var inserir = DaoUtil.Comando(conexao,
                @"INSERT INTO itens_pedido_compra (pedido_compra_id, produto_id, quantidade, custo_unitario)
                  VALUES (@pedido, @produto, @qtd, @custo)", transacao);
            foreach (var item in pedido.Itens)
            {
                inserir.Parameters.Clear();
                inserir.Parameters.AddWithValue("@pedido", pedido.Id);
                inserir.Parameters.AddWithValue("@produto", item.ProdutoId);
                inserir.Parameters.AddWithValue("@qtd", item.Quantidade);
                inserir.Parameters.AddWithValue("@custo", item.CustoUnitario);
                inserir.ExecuteNonQuery();
            }
        }

        /// <summary>Trava o pedido e garante que ele ainda está ABERTO.</summary>
        private static void GarantirAberto(MySqlConnection conexao, MySqlTransaction transacao, int pedidoId)
        {
            using var cmd = DaoUtil.Comando(conexao, "SELECT status FROM pedidos_compra WHERE id = @id FOR UPDATE", transacao);
            cmd.Parameters.AddWithValue("@id", pedidoId);
            var status = cmd.ExecuteScalar() as string;
            if (status == null) throw new RegraNegocioException("Pedido não encontrado.");
            if (status != "ABERTO")
                throw new RegraNegocioException($"Este pedido já está {(status == "RECEBIDO" ? "recebido" : "cancelado")} e não pode ser alterado.");
        }

        /// <summary>
        /// Recebe a mercadoria: marca o pedido como RECEBIDO, soma as quantidades no
        /// estoque e registra as ENTRADAS por COMPRA — tudo na mesma transação.
        /// Também atualiza o último preço de custo daquele fornecedor.
        /// </summary>
        public void Receber(int pedidoId, int usuarioId)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                GarantirAberto(conexao, transacao, pedidoId);
                var itens = ListarItens(conexao, transacao, pedidoId);
                if (itens.Count == 0) throw new RegraNegocioException("O pedido não tem itens para receber.");

                int fornecedorId;
                using (var cmd = DaoUtil.Comando(conexao, "SELECT fornecedor_id FROM pedidos_compra WHERE id = @id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", pedidoId);
                    fornecedorId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                var agora = DateTime.Now;
                foreach (var item in itens)
                {
                    EstoqueDAO.AlterarSaldo(conexao, transacao, item.ProdutoId, item.Quantidade);
                    EstoqueDAO.InserirMovimentacao(conexao, transacao, new MovimentacaoEstoque
                    {
                        ProdutoId = item.ProdutoId,
                        UsuarioId = usuarioId,
                        Tipo = TipoMovimentacao.Entrada,
                        Motivo = MotivoMovimentacao.Compra,
                        Quantidade = item.Quantidade,
                        PedidoCompraId = pedidoId,
                        Observacao = $"Recebimento do pedido de compra nº {pedidoId}",
                        DataMovimentacao = agora
                    });

                    using var custo = DaoUtil.Comando(conexao,
                        @"INSERT INTO fornecedor_produto (fornecedor_id, produto_id, preco_custo) VALUES (@f, @p, @c)
                          ON DUPLICATE KEY UPDATE preco_custo = @c", transacao);
                    custo.Parameters.AddWithValue("@f", fornecedorId);
                    custo.Parameters.AddWithValue("@p", item.ProdutoId);
                    custo.Parameters.AddWithValue("@c", item.CustoUnitario);
                    custo.ExecuteNonQuery();
                }

                using (var cmd = DaoUtil.Comando(conexao,
                    "UPDATE pedidos_compra SET status = 'RECEBIDO', data_recebimento = @data WHERE id = @id", transacao))
                {
                    cmd.Parameters.AddWithValue("@id", pedidoId);
                    cmd.Parameters.AddWithValue("@data", agora);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public void Cancelar(int pedidoId, string motivo)
        {
            DaoUtil.ExecutarTransacao((conexao, transacao) =>
            {
                GarantirAberto(conexao, transacao, pedidoId);
                using var cmd = DaoUtil.Comando(conexao,
                    "UPDATE pedidos_compra SET status = 'CANCELADO', observacao = COALESCE(@obs, observacao) WHERE id = @id", transacao);
                cmd.Parameters.AddWithValue("@id", pedidoId);
                cmd.Parameters.AddWithValue("@obs", DaoUtil.Nulo(motivo?.Length > 255 ? motivo[..255] : motivo));
                cmd.ExecuteNonQuery();
            });
        }
    }
}
