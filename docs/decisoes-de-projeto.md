# Decisões de projeto

Este documento explica **por que** o sistema foi construído desta forma — tanto no banco de dados quanto na aplicação.

---

## 1. Banco de dados

### 1.1 Normalização (até a 3ª Forma Normal)
- **1FN** — nenhum campo guarda listas: os itens de uma venda ficam em `itens_venda`, não em uma coluna "produtos". O endereço do cliente é separado em CEP, logradouro, número etc.
- **2FN** — nas tabelas com chave composta (`itens_venda`, `fornecedor_produto`...), cada atributo depende da chave **inteira**. Ex.: `preco_custo` depende de *fornecedor + produto*, e não só do produto.
- **3FN** — não há dependências entre atributos não-chave: o nome da categoria fica em `categorias`, e o produto guarda só `categoria_id`.

### 1.2 Desnormalizações conscientes
| Onde | Por quê |
|---|---|
| `itens_venda.preco_unitario` | O preço do produto muda com o tempo. A venda precisa guardar o preço **do momento** para o histórico e os relatórios não mudarem retroativamente. |
| `estoque.quantidade` | Daria para calcular o saldo somando todas as movimentações, mas isso fica lento à medida que a tabela cresce. O saldo é mantido pronto e **sempre atualizado na mesma transação** que grava a movimentação, então os dois nunca divergem. |
| `vendas.valor_total`, `itens_venda.subtotal` | São **colunas geradas** (`GENERATED ALWAYS ... STORED`): o MySQL calcula o valor sozinho, então é impossível gravar um total errado. |

### 1.3 Estoque 1:1 + histórico de movimentações
O saldo (`estoque`) fica separado do cadastro (`produtos`) porque muda o tempo todo, enquanto o cadastro quase não muda. Toda alteração de saldo gera uma linha em `movimentacoes_estoque` com **quem**, **quando**, **quanto** e **por quê** (venda, compra, ajuste, devolução, perda). Isso responde à pergunta "por que este produto está com este saldo?".

No sistema, o saldo **não pode ser digitado** no cadastro do produto. Ele só muda por movimentações: venda, recebimento de compra ou ajuste manual com justificativa.

### 1.4 Kits como autorrelacionamento N:N
Um kit é um produto (`tipo = 'KIT'`) que contém outros produtos (`kit_itens`). Kits **não têm estoque próprio**: a disponibilidade é calculada pela view `vw_disponibilidade_kits`, a partir do componente mais escasso. Na venda de um kit, a baixa é feita nos **componentes**. Assim um kit nunca é vendido sem que as peças existam.

### 1.5 Exclusão física × exclusão lógica (`ativo`)
As chaves estrangeiras impedem apagar um cliente que já comprou ou um produto que já foi vendido. Nesses casos o sistema oferece **inativar**: o registro some das pesquisas, mas o histórico continua íntegro. Registros sem histórico podem ser excluídos de verdade.

### 1.6 Restrições no banco (e não só na tela)
CPF/CNPJ só com dígitos, preço positivo, estoque não negativo e coerência entre motivo e tipo da movimentação são validados com `CHECK`. A tela também valida, para dar uma mensagem amigável. O banco é a **última barreira**: mesmo um script SQL externo não consegue gravar dados inválidos.

### 1.7 Tipos de dados
- `DECIMAL(10,2)` para dinheiro. `FLOAT`/`DOUBLE` geram erros de arredondamento (0,1 + 0,2 ≠ 0,3).
- `ENUM` para domínios fechados (perfil, status, forma de pagamento).
- `BIGINT` em `movimentacoes_estoque.id`, a tabela que mais cresce.
- `utf8mb4` com `utf8mb4_0900_ai_ci`: aceita qualquer caractere, e a pesquisa "lapis" encontra "Lápis".

---

## 2. Aplicação

### 2.1 Camadas
```
Forms (telas)  →  DAO (SQL)  →  Conexao (MySqlConnection)  →  MySQL
      ↘ Models (entidades + regras de validação) ↙
```
| Pasta | Responsabilidade | Não pode |
|---|---|---|
| `Models/` | Entidades (`Cliente`, `Venda`...), validações (`Validar()`) e cálculos (`RecalcularTotais()`, `Promocao.CalcularDesconto()`) | conhecer telas ou SQL |
| `DAO/` | Todo o SQL, sempre **parametrizado**, mais as transações | mostrar mensagens na tela |
| `Conexao/` | Criar conexões a partir do `appsettings.json` | — |
| `Forms/` | Telas e interação com a pessoa usuária | escrever SQL |
| `Relatorios/` | Exportação para Excel (ClosedXML) e PDF (QuestPDF) | depender de controles visuais |
| `Controles/`, `Tema/` | Componentes visuais da identidade Lapapiê | acessar o banco |
| `Util/` | Validação de CPF/CNPJ, formatação, sessão/permissões, senhas, log | — |

Com essa separação dá para testar o DAO sem abrir nenhuma tela, ou trocar uma tela inteira sem mexer no SQL.

### 2.2 SQL parametrizado
Nenhum valor digitado é concatenado no SQL. Por exemplo:
```csharp
cmd.CommandText = "SELECT ... FROM clientes WHERE nome LIKE @texto";
cmd.Parameters.AddWithValue("@texto", DaoUtil.Like(texto));
```
Isso impede **SQL Injection**: se alguém digitar `' OR 1=1 --`, o texto é tratado como dado e não como comando. Até as listas `IN (...)` usam parâmetros (`DaoUtil.ParametrosIn`).

### 2.3 Transações nas operações críticas
| Operação | O que acontece dentro da transação |
|---|---|
| Registrar venda (`VendaDAO.RegistrarVenda`) | trava as linhas de estoque (`SELECT ... FOR UPDATE`) → confere o saldo → grava venda e itens → dá baixa → registra as movimentações |
| Cancelar venda | marca como cancelada → devolve exatamente o que saiu (lido das movimentações da venda) |
| Receber pedido de compra | soma ao estoque → registra as entradas → atualiza o custo do fornecedor → marca como recebido |
| Cadastrar/alterar produto | produto + estoque + saldo inicial + composição do kit + fornecedores |
| Salvar promoção | promoção + produtos participantes |

Se qualquer passo falhar, o `Rollback` desfaz tudo. O `FOR UPDATE` resolve outro problema: dois caixas vendendo a última unidade ao mesmo tempo. O segundo espera o primeiro terminar e então vê o saldo já zerado.

### 2.4 Tratamento de exceções
- O DAO converte `MySqlException` em `BancoDadosException`, com mensagens em português. Por exemplo, o erro 1062 vira "Já existe um cliente com este CPF" e o 1451 vira "Registro em uso".
- Violações de regra de negócio, como estoque insuficiente ou venda já cancelada, geram `RegraNegocioException`.
- `Mensagem.TratarErro` decide o que mostrar. Erros inesperados são gravados em `logs/erros.log`, e um tratador global em `Program.cs` evita que o sistema feche sozinho.

### 2.5 Promoções
O caixa aplica automaticamente a **melhor** promoção vigente de cada produto, ou seja, a que dá o maior desconto. O desconto é calculado no Model (`Promocao.CalcularDesconto`) e **gravado no item da venda**, então excluir ou alterar uma promoção depois não muda vendas antigas.

### 2.6 Permissões por perfil
A matriz fica em um único lugar (`Util/Sessao.cs`). O menu só mostra o que o perfil pode acessar, e as telas escondem botões de ações não permitidas.

| Funcionalidade | Admin | Vendedor | Estoquista |
|---|:-:|:-:|:-:|
| Caixa, histórico de vendas, clientes | ✔ | ✔ | |
| Cancelar venda, relatório, promoções, usuários | ✔ | | |
| Produtos (editar), categorias, fornecedores, movimentações, pedidos de compra | ✔ | | ✔ |
| Consultar produtos, kits, estoque, estoque baixo, início | ✔ | ✔ | ✔ |

### 2.7 Senhas
A massa de teste grava as senhas em texto puro. O sistema confere os dois formatos e, no **primeiro login bem-sucedido**, troca a senha pelo hash **PBKDF2-SHA256** com sal aleatório. Senhas novas já nascem com hash. O formato (`pbkdf2$iterações$sal$hash`) cabe no `VARCHAR(100)` existente, então **o script do banco não precisou mudar**.

### 2.8 Imagens dos produtos
O banco não tem coluna de imagem, e a decisão foi **não alterar o script entregue**. A imagem é associada pelo id (`Recursos/Produtos/{id}.png`). Sem imagem própria, o produto usa a ilustração da categoria (`categoria-{id}.png`). No cadastro de produto dá para escolher uma imagem nova, que é convertida para PNG quadrado.

As ilustrações foram desenhadas para o projeto na paleta da marca e não reproduzem logos ou marcas reais.

### 2.9 Identidade visual
- **Paleta oficial**: papel algodão (fundos), tinta grafite (texto e botões principais), argila rosada (destaques), sálvia (sucesso/apoio) e lacre dourado só em detalhes (títulos de seção, alertas), como recomenda o manual da marca.
- **Referência visual** (e-commerce): títulos grandes em caixa alta, cartões brancos arredondados, botões em pílula com seta ("Shop now →"), selos em pílula, vitrine de produtos com botão de adicionar e a faixa "OFERTA · OFERTA" rolando.
- **Tipografia**: Bahnschrift (grotesca, nativa do Windows 10/11) nos títulos, Segoe UI nos textos e Georgia itálico nos subtítulos, conversando com o logotipo em serifa itálica.
- **Componentes próprios** (`Controles/`): `BotaoPill`, `PainelCartao`, `CampoTexto`, `AbasPill`, `CartaoProduto`, `CartaoIndicador` e gráficos de barras, linha e rosca **desenhados com GDI+**, sem bibliotecas externas.

### 2.10 Escalabilidade e manutenção
- Pesquisas feitas no banco com índices, e não carregando tudo para a memória.
- Pesquisa com *debounce*: espera a pessoa parar de digitar antes de consultar.
- Conexões abertas e fechadas a cada operação (`using`), aproveitando o *pool* de conexões do conector.
- String de conexão fora do código (`appsettings.json`), editável pela tela **Configurar conexão**.
- Tema e permissões centralizados: mudar a cor primária ou dar acesso a um perfil é uma alteração em um só arquivo.
