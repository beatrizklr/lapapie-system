# Modelagem física — banco `lapapie` (MySQL 8)

> Documento gerado a partir de `database/lapapie_create.sql`. Ele descreve **como** o modelo conceitual (ver [MER.md](MER.md)) virou tabelas, tipos, chaves e restrições.

**Convenções:** nomes em `snake_case`; chave primária `id` (ou composta, nas tabelas associativas); chaves estrangeiras `<entidade>_id`; restrições nomeadas com prefixo (`uk_` única, `ck_` verificação, `fk_` estrangeira, `idx_` índice). Todas as tabelas usam **InnoDB** (necessário para chaves estrangeiras e transações) e o banco usa `utf8mb4`.

| # | Tabela | Papel |
|---|---|---|
| 1 | `usuarios` | Quem acessa o sistema (login e permissões) |
| 2 | `clientes` | Clientes |
| 3 | `categorias` | Categorias |
| 4 | `fornecedores` | Fornecedores |
| 5 | `produtos` | Produtos |
| 6 | `estoque` | Saldo atual de cada produto SIMPLES (relação 1:1 com produtos) |
| 7 | `kit_itens` | Composição dos combos e kits |
| 8 | `fornecedor_produto` | N:N entre fornecedores e produtos |
| 9 | `promocoes` | Promoções |
| 10 | `promocao_produto` | N:N entre promoções e produtos |
| 11 | `vendas` | Cabeçalho da venda |
| 12 | `itens_venda` | N:N entre vendas e produtos |
| 13 | `pedidos_compra` | Compras feitas aos fornecedores |
| 14 | `itens_pedido_compra` | N:N entre pedidos de compra e produtos |
| 15 | `movimentacoes_estoque` | Histórico de toda entrada e saída |

**Total: 15 tabelas + 3 views.** Relacionamentos N:N resolvidos com tabelas associativas: `itens_venda`, `fornecedor_produto`, `promocao_produto`, `itens_pedido_compra` e `kit_itens` (5 — o mínimo exigido era 3).

## 1. `usuarios`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `nome` | `VARCHAR(100) NOT NULL` |  |
| `login` | `VARCHAR(50) NOT NULL` |  |
| `senha` | `VARCHAR(100) NOT NULL` |  |
| `perfil` | `ENUM('ADMIN', 'VENDEDOR', 'ESTOQUISTA') NOT NULL DEFAULT 'VENDEDOR'` |  |
| `ativo` | `BOOLEAN NOT NULL DEFAULT TRUE` |  |
| `ultimo_login` | `DATETIME NULL` |  |
| `criado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT uk_usuarios_login UNIQUE (login)`

## 2. `clientes`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `nome` | `VARCHAR(100) NOT NULL` |  |
| `cpf` | `CHAR(11) NULL` | só números; opcional |
| `email` | `VARCHAR(100) NULL` |  |
| `telefone` | `VARCHAR(20) NULL` |  |
| `cep` | `CHAR(8) NULL` |  |
| `logradouro` | `VARCHAR(150) NULL` |  |
| `numero` | `VARCHAR(10) NULL` |  |
| `complemento` | `VARCHAR(50) NULL` |  |
| `bairro` | `VARCHAR(80) NULL` |  |
| `cidade` | `VARCHAR(80) NULL` |  |
| `uf` | `CHAR(2) NULL` |  |
| `ativo` | `BOOLEAN NOT NULL DEFAULT TRUE` |  |
| `criado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT uk_clientes_cpf UNIQUE (cpf)`
- `CONSTRAINT ck_clientes_cpf CHECK (cpf REGEXP '^[0-9]{11}$')`
- `CONSTRAINT ck_clientes_cep CHECK (cep REGEXP '^[0-9]{8}$')`
- `INDEX idx_clientes_nome (nome)`

## 3. `categorias`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `nome` | `VARCHAR(60) NOT NULL` |  |
| `descricao` | `VARCHAR(255) NULL` |  |
| `ativo` | `BOOLEAN NOT NULL DEFAULT TRUE` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT uk_categorias_nome UNIQUE (nome)`

## 4. `fornecedores`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `razao_social` | `VARCHAR(150) NOT NULL` |  |
| `nome_fantasia` | `VARCHAR(100) NULL` |  |
| `cnpj` | `CHAR(14) NOT NULL` | só números |
| `email` | `VARCHAR(100) NULL` |  |
| `telefone` | `VARCHAR(20) NULL` |  |
| `nome_contato` | `VARCHAR(100) NULL` |  |
| `cidade` | `VARCHAR(80) NULL` |  |
| `uf` | `CHAR(2) NULL` |  |
| `ativo` | `BOOLEAN NOT NULL DEFAULT TRUE` |  |
| `criado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT uk_fornecedores_cnpj UNIQUE (cnpj)`
- `CONSTRAINT ck_fornecedores_cnpj CHECK (cnpj REGEXP '^[0-9]{14}$')`

## 5. `produtos`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `categoria_id` | `INT UNSIGNED NOT NULL` |  |
| `codigo_barras` | `VARCHAR(14) NULL` |  |
| `nome` | `VARCHAR(120) NOT NULL` |  |
| `descricao` | `VARCHAR(500) NULL` |  |
| `marca` | `VARCHAR(60) NULL` |  |
| `tipo` | `ENUM('SIMPLES', 'KIT') NOT NULL DEFAULT 'SIMPLES'` | KIT = combo de outros produtos |
| `unidade_medida` | `VARCHAR(10) NOT NULL DEFAULT 'UN'` | UN, CX, PCT, RESMA... |
| `preco_venda` | `DECIMAL(10,2) NOT NULL` |  |
| `ativo` | `BOOLEAN NOT NULL DEFAULT TRUE` |  |
| `criado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |
| `atualizado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT uk_produtos_codigo_barras UNIQUE (codigo_barras)`
- `CONSTRAINT ck_produtos_preco_venda CHECK (preco_venda > 0)`
- `CONSTRAINT fk_produtos_categoria FOREIGN KEY (categoria_id) REFERENCES categorias (id)`
- `INDEX idx_produtos_nome (nome)`
- `INDEX idx_produtos_tipo (tipo)`

## 6. `estoque`

Kits não têm saldo próprio: a disponibilidade deles vem dos componentes (ver vw_disponibilidade_kits).

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `produto_id` | `INT UNSIGNED NOT NULL` | é PK e FK ao mesmo tempo (garante o 1:1) |
| `quantidade` | `INT NOT NULL DEFAULT 0` |  |
| `estoque_minimo` | `INT NOT NULL DEFAULT 5` |  |
| `localizacao` | `VARCHAR(50) NULL` | ex.: "Corredor 2 - Prateleira B" |
| `atualizado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (produto_id)`
- `CONSTRAINT ck_estoque_quantidade CHECK (quantidade >= 0)`
- `CONSTRAINT ck_estoque_minimo CHECK (estoque_minimo >= 0)`
- `CONSTRAINT fk_estoque_produto FOREIGN KEY (produto_id) REFERENCES produtos (id) ON DELETE CASCADE`

## 7. `kit_itens`

Auto-relacionamento N:N de produtos: um kit (produto do tipo KIT) contém vários produtos, e um produto pode estar em vários kits. Ex.: "Kit Volta às Aulas" = 2 cadernos + 1 estojo + 3 canetas.

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `kit_id` | `INT UNSIGNED NOT NULL` | produto do tipo KIT |
| `produto_id` | `INT UNSIGNED NOT NULL` | produto SIMPLES que compõe o kit |
| `quantidade` | `INT UNSIGNED NOT NULL DEFAULT 1` |  |

**Chaves e restrições**

- `PRIMARY KEY (kit_id, produto_id)`
- `CONSTRAINT ck_kit_itens_quantidade CHECK (quantidade > 0)`
- `CONSTRAINT ck_kit_itens_diferentes CHECK (kit_id <> produto_id)`
- `CONSTRAINT fk_kit_itens_kit FOREIGN KEY (kit_id) REFERENCES produtos (id)`
- `CONSTRAINT fk_kit_itens_produto FOREIGN KEY (produto_id) REFERENCES produtos (id)`

## 8. `fornecedor_produto`

(quais fornecedores vendem quais produtos, e por quanto)

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `fornecedor_id` | `INT UNSIGNED NOT NULL` |  |
| `produto_id` | `INT UNSIGNED NOT NULL` |  |
| `codigo_no_fornecedor` | `VARCHAR(30) NULL` |  |
| `preco_custo` | `DECIMAL(10,2) NOT NULL` |  |
| `prazo_entrega_dias` | `SMALLINT UNSIGNED NULL` |  |

**Chaves e restrições**

- `PRIMARY KEY (fornecedor_id, produto_id)`
- `CONSTRAINT ck_fornecedor_produto_custo CHECK (preco_custo >= 0)`
- `CONSTRAINT fk_fornecedor_produto_fornecedor FOREIGN KEY (fornecedor_id) REFERENCES fornecedores (id) ON DELETE CASCADE`
- `CONSTRAINT fk_fornecedor_produto_produto FOREIGN KEY (produto_id) REFERENCES produtos (id) ON DELETE CASCADE`

## 9. `promocoes`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `nome` | `VARCHAR(100) NOT NULL` |  |
| `descricao` | `VARCHAR(255) NULL` |  |
| `tipo_desconto` | `ENUM('PERCENTUAL', 'VALOR_FIXO') NOT NULL` |  |
| `valor_desconto` | `DECIMAL(10,2) NOT NULL` |  |
| `data_inicio` | `DATETIME NOT NULL` |  |
| `data_fim` | `DATETIME NOT NULL` |  |
| `ativo` | `BOOLEAN NOT NULL DEFAULT TRUE` |  |
| `criado_em` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT ck_promocoes_periodo CHECK (data_fim > data_inicio)`
- `CONSTRAINT ck_promocoes_valor CHECK (valor_desconto > 0)`
- `CONSTRAINT ck_promocoes_percentual CHECK (tipo_desconto <> 'PERCENTUAL' OR valor_desconto <= 100)`

## 10. `promocao_produto`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `promocao_id` | `INT UNSIGNED NOT NULL` |  |
| `produto_id` | `INT UNSIGNED NOT NULL` |  |

**Chaves e restrições**

- `PRIMARY KEY (promocao_id, produto_id)`
- `CONSTRAINT fk_promocao_produto_promocao FOREIGN KEY (promocao_id) REFERENCES promocoes (id) ON DELETE CASCADE`
- `CONSTRAINT fk_promocao_produto_produto FOREIGN KEY (produto_id) REFERENCES produtos (id) ON DELETE CASCADE`

## 11. `vendas`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `cliente_id` | `INT UNSIGNED NULL` | NULL = consumidor não identificado |
| `usuario_id` | `INT UNSIGNED NOT NULL` | quem registrou a venda |
| `data_venda` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |
| `valor_subtotal` | `DECIMAL(10,2) NOT NULL DEFAULT 0.00` | soma de quantidade x preço |
| `valor_desconto` | `DECIMAL(10,2) NOT NULL DEFAULT 0.00` | soma dos descontos dos itens |
| `valor_total` | `DECIMAL(10,2) GENERATED ALWAYS AS (valor_subtotal - valor_desconto) STORED` |  |
| `forma_pagamento` | `ENUM('DINHEIRO', 'PIX', 'CARTAO_DEBITO', 'CARTAO_CREDITO') NOT NULL` |  |
| `status` | `ENUM('CONCLUIDA', 'CANCELADA') NOT NULL DEFAULT 'CONCLUIDA'` |  |
| `observacao` | `VARCHAR(255) NULL` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT ck_vendas_valores CHECK (valor_subtotal >= 0 AND valor_desconto >= 0 AND valor_desconto <= valor_subtotal)`
- `CONSTRAINT fk_vendas_cliente FOREIGN KEY (cliente_id) REFERENCES clientes (id)`
- `CONSTRAINT fk_vendas_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios (id)`
- `INDEX idx_vendas_data (data_venda)`

## 12. `itens_venda`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `venda_id` | `INT UNSIGNED NOT NULL` |  |
| `produto_id` | `INT UNSIGNED NOT NULL` |  |
| `quantidade` | `INT UNSIGNED NOT NULL` |  |
| `preco_unitario` | `DECIMAL(10,2) NOT NULL` | preço NO MOMENTO da venda (histórico) |
| `desconto` | `DECIMAL(10,2) NOT NULL DEFAULT 0.00` | desconto total do item |
| `subtotal` | `DECIMAL(10,2) GENERATED ALWAYS AS (quantidade * preco_unitario - desconto) STORED` |  |

**Chaves e restrições**

- `PRIMARY KEY (venda_id, produto_id)`
- `CONSTRAINT ck_itens_venda_quantidade CHECK (quantidade > 0)`
- `CONSTRAINT ck_itens_venda_preco CHECK (preco_unitario >= 0)`
- `CONSTRAINT ck_itens_venda_desconto CHECK (desconto >= 0 AND desconto <= quantidade * preco_unitario)`
- `CONSTRAINT fk_itens_venda_venda FOREIGN KEY (venda_id) REFERENCES vendas (id) ON DELETE CASCADE`
- `CONSTRAINT fk_itens_venda_produto FOREIGN KEY (produto_id) REFERENCES produtos (id)`

## 13. `pedidos_compra`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `INT UNSIGNED NOT NULL AUTO_INCREMENT` |  |
| `fornecedor_id` | `INT UNSIGNED NOT NULL` |  |
| `usuario_id` | `INT UNSIGNED NOT NULL` |  |
| `data_pedido` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |
| `data_prevista_entrega` | `DATE NULL` |  |
| `data_recebimento` | `DATETIME NULL` |  |
| `status` | `ENUM('ABERTO', 'RECEBIDO', 'CANCELADO') NOT NULL DEFAULT 'ABERTO'` |  |
| `valor_total` | `DECIMAL(10,2) NOT NULL DEFAULT 0.00` |  |
| `observacao` | `VARCHAR(255) NULL` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT ck_pedidos_compra_valor CHECK (valor_total >= 0)`
- `CONSTRAINT ck_pedidos_compra_recebimento CHECK (status <> 'RECEBIDO' OR data_recebimento IS NOT NULL)`
- `CONSTRAINT fk_pedidos_compra_fornecedor FOREIGN KEY (fornecedor_id) REFERENCES fornecedores (id)`
- `CONSTRAINT fk_pedidos_compra_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios (id)`

## 14. `itens_pedido_compra`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `pedido_compra_id` | `INT UNSIGNED NOT NULL` |  |
| `produto_id` | `INT UNSIGNED NOT NULL` |  |
| `quantidade` | `INT UNSIGNED NOT NULL` |  |
| `custo_unitario` | `DECIMAL(10,2) NOT NULL` |  |
| `subtotal` | `DECIMAL(10,2) GENERATED ALWAYS AS (quantidade * custo_unitario) STORED` |  |

**Chaves e restrições**

- `PRIMARY KEY (pedido_compra_id, produto_id)`
- `CONSTRAINT ck_itens_pedido_quantidade CHECK (quantidade > 0)`
- `CONSTRAINT ck_itens_pedido_custo CHECK (custo_unitario >= 0)`
- `CONSTRAINT fk_itens_pedido_pedido FOREIGN KEY (pedido_compra_id) REFERENCES pedidos_compra (id) ON DELETE CASCADE`
- `CONSTRAINT fk_itens_pedido_produto FOREIGN KEY (produto_id) REFERENCES produtos (id)`

## 15. `movimentacoes_estoque`

| Coluna | Tipo e restrições | Observação |
|---|---|---|
| `id` | `BIGINT UNSIGNED NOT NULL AUTO_INCREMENT` | tabela que mais cresce |
| `produto_id` | `INT UNSIGNED NOT NULL` |  |
| `usuario_id` | `INT UNSIGNED NOT NULL` |  |
| `tipo` | `ENUM('ENTRADA', 'SAIDA') NOT NULL` |  |
| `motivo` | `ENUM('VENDA', 'COMPRA', 'AJUSTE', 'DEVOLUCAO', 'PERDA') NOT NULL` |  |
| `quantidade` | `INT UNSIGNED NOT NULL` |  |
| `venda_id` | `INT UNSIGNED NULL` | preenchido quando a origem é uma venda |
| `pedido_compra_id` | `INT UNSIGNED NULL` | preenchido quando a origem é uma compra |
| `observacao` | `VARCHAR(255) NULL` |  |
| `data_movimentacao` | `DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP` |  |

**Chaves e restrições**

- `PRIMARY KEY (id)`
- `CONSTRAINT ck_mov_quantidade CHECK (quantidade > 0)`
- `CONSTRAINT ck_mov_venda CHECK (motivo <> 'VENDA' OR (tipo = 'SAIDA' AND venda_id IS NOT NULL))`
- `CONSTRAINT ck_mov_compra CHECK (motivo <> 'COMPRA' OR (tipo = 'ENTRADA' AND pedido_compra_id IS NOT NULL))`
- `CONSTRAINT ck_mov_perda CHECK (motivo <> 'PERDA' OR tipo = 'SAIDA')`
- `CONSTRAINT fk_mov_produto FOREIGN KEY (produto_id) REFERENCES produtos (id)`
- `CONSTRAINT fk_mov_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios (id)`
- `CONSTRAINT fk_mov_venda FOREIGN KEY (venda_id) REFERENCES vendas (id)`
- `CONSTRAINT fk_mov_pedido_compra FOREIGN KEY (pedido_compra_id) REFERENCES pedidos_compra (id)`
- `INDEX idx_mov_produto_data (produto_id, data_movimentacao)`

## Views

| View | Para que serve | Usada em |
|---|---|---|
| `vw_produtos_estoque_baixo` | Produtos ativos com saldo ≤ estoque mínimo e o déficit | Tela *Estoque baixo*, contador do menu e dashboard |
| `vw_historico_vendas` | Vendas com cliente, vendedor, total de itens e valores | Telas *Histórico de vendas*, *Relatório* e *Início* |
| `vw_disponibilidade_kits` | Quantos kits dá para montar: `MIN(FLOOR(saldo do componente / qtd. exigida))` | Caixa, *Kits e combos* e dashboard |

## Índices além das chaves

- `idx_clientes_nome`, `idx_produtos_nome`: pesquisas por nome nas telas de cadastro e no caixa.
- `idx_produtos_tipo`: separa SIMPLES e KIT nos filtros.
- `idx_vendas_data`: histórico e relatórios sempre filtram por período.
- `idx_mov_produto_data` (composto): histórico de movimentações de um produto em ordem cronológica — a tabela que mais cresce.

> No InnoDB, toda chave estrangeira também ganha um índice automaticamente, o que acelera os JOINs.
