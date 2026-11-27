# Lapapiê — Documentação da Modelagem do Banco de Dados

## 1. Visão geral

O banco `lapapie` (MySQL 8.0.16+) possui **15 tabelas**, **3 views** e **5 relacionamentos N:N**.

| # | Tabela | Papel |
|---|--------|-------|
| 1 | `usuarios` | Login e perfil de permissão |
| 2 | `clientes` | Cadastro de clientes |
| 3 | `categorias` | Classificação dos produtos |
| 4 | `fornecedores` | Cadastro de fornecedores |
| 5 | `produtos` | Catálogo de produtos |
| 6 | `estoque` | Saldo atual de cada produto simples (1:1 com produtos) |
| 7 | `kit_itens` | **N:N** Produto ↔ Produto (composição de combos e kits) |
| 8 | `fornecedor_produto` | **N:N** Fornecedor ↔ Produto |
| 9 | `promocoes` | Campanhas de desconto |
| 10 | `promocao_produto` | **N:N** Promoção ↔ Produto |
| 11 | `vendas` | Cabeçalho da venda |
| 12 | `itens_venda` | **N:N** Venda ↔ Produto |
| 13 | `pedidos_compra` | Compras feitas a fornecedores |
| 14 | `itens_pedido_compra` | **N:N** Pedido de Compra ↔ Produto |
| 15 | `movimentacoes_estoque` | Histórico de entradas e saídas |

Tabelas além do mínimo exigido: `fornecedor_produto` e `promocao_produto` (obrigatórias para representar os N:N) `movimentacoes_estoque` (necessária para a funcionalidade "Movimentação de estoque") e `kit_itens` (diferencial de combos e kits).

## 2. Do modelo conceitual ao físico

O modelo conceitual descreve o **negócio** e, por isso, usa nomes em linguagem natural, no singular e com acentuação (ex.: *Promoção*). O modelo físico descreve a **implementação** no MySQL e segue a convenção técnica: minúsculas, plural, `snake_case` e sem acentos ou "ç" — caracteres que causam problemas em código, terminais e ferramentas.

| Conceitual | Físico |
|---|---|
| Usuário | `usuarios` |
| Cliente | `clientes` |
| Categoria | `categorias` |
| Produto | `produtos` |
| Estoque | `estoque` |
| Fornecedor | `fornecedores` |
| Venda | `vendas` |
| Pedido de Compra | `pedidos_compra` |
| Promoção | `promocoes` |
| Movimentação de Estoque | `movimentacoes_estoque` |
| *contém* (Venda ↔ Produto) | `itens_venda` |
| *solicita* (Pedido de Compra ↔ Produto) | `itens_pedido_compra` |
| *fornece* (Fornecedor ↔ Produto) | `fornecedor_produto` |
| *aplica-se a* (Promoção ↔ Produto) | `promocao_produto` |
| *compõe* (Kit ↔ Produto) | `kit_itens` |

Os relacionamentos N:N não existem como tabela no conceitual: eles só viram tabelas associativas na passagem para o físico.

## 3. Cardinalidades

| Relacionamento | Cardinalidade | Leitura |
|---|---|---|
| Categoria — Produto | 1:N | Uma categoria classifica vários produtos; um produto tem exatamente uma categoria |
| Produto — Estoque | 1:0..1 | Cada produto simples tem um único registro de saldo; kits não têm |
| Cliente — Venda | 0..1:N | Uma venda pode ter um cliente ou nenhum (consumidor não identificado) |
| Usuário — Venda | 1:N | Toda venda é registrada por um usuário |
| Fornecedor — Pedido de Compra | 1:N | Todo pedido é feito a um fornecedor |
| Usuário — Pedido de Compra | 1:N | Todo pedido é emitido por um usuário |
| Venda ↔ Produto | **N:N** | Uma venda contém 1+ produtos; um produto aparece em 0+ vendas |
| Pedido de Compra ↔ Produto | **N:N** | Um pedido solicita 1+ produtos; um produto aparece em 0+ pedidos |
| Fornecedor ↔ Produto | **N:N** | Um fornecedor fornece vários produtos; um produto pode vir de vários fornecedores |
| Promoção ↔ Produto | **N:N** | Uma promoção abrange 1+ produtos; um produto pode estar em várias promoções |
| Kit ↔ Produto | **N:N** (auto-relacionamento) | Um kit é composto por 1+ produtos; um produto pode compor 0+ kits |
| Produto — Movimentação | 1:N | Cada movimentação se refere a um produto |
| Venda / Pedido — Movimentação | 0..1:N | A movimentação pode ter origem em uma venda, em um pedido, ou em nenhum (ajuste manual) |

**Atributos dos relacionamentos N:N** (viram colunas das tabelas associativas):
- Venda ↔ Produto: quantidade, preço unitário, desconto, subtotal
- Pedido ↔ Produto: quantidade, custo unitário, subtotal
- Fornecedor ↔ Produto: código no fornecedor, preço de custo, prazo de entrega
- Promoção ↔ Produto: nenhum (o desconto é definido na promoção)
- Kit ↔ Produto: quantidade do componente no kit

## 4. Decisões de modelagem

### 4.1 Motor InnoDB e charset utf8mb4
InnoDB é o motor do MySQL que suporta **transações** e **chaves estrangeiras** — sem ele, nem a venda transacional nem a integridade referencial funcionariam. O collation `utf8mb4_0900_ai_ci` ignora acentos e maiúsculas nas comparações, então a pesquisa por "lapis" encontra "Lápis".

### 4.2 Nomenclatura
- Tabelas em `snake_case`, no plural (`produtos`); tabelas associativas nomeadas pelas duas entidades (`fornecedor_produto`).
- Chave primária sempre `id`; chave estrangeira sempre `<entidade>_id`.
- Restrições com prefixo: `pk_`, `fk_`, `uk_` (único), `ck_` (check), `idx_` (índice). Assim, uma mensagem de erro como `ck_estoque_quantidade violated` já diz qual regra foi quebrada.

### 4.3 Chaves primárias compostas nas tabelas N:N
Em `itens_venda`, a PK é `(venda_id, produto_id)`. Isso impede, no próprio banco, que o mesmo produto apareça duas vezes na mesma venda — a aplicação deve somar a quantidade em vez de criar outra linha.

### 4.4 Exclusão lógica (`ativo`)
Clientes, produtos, fornecedores, categorias e usuários não são apagados de verdade: o "excluir" do CRUD marca `ativo = FALSE`. Motivo: uma venda antiga referencia o produto e o cliente; se eles fossem apagados, o histórico perderia o sentido. As FKs sem `ON DELETE CASCADE` (padrão RESTRICT) reforçam isso, bloqueando a exclusão física de registros com histórico.

### 4.5 Estoque separado do produto + tabela de movimentações
- `produtos` guarda dados cadastrais (mudam pouco); `estoque` guarda o saldo (muda a cada venda). Separar facilita o bloqueio da linha de saldo durante a venda sem travar o cadastro.
- `estoque.quantidade` responde rápido "quanto tenho agora?"; `movimentacoes_estoque` responde "por que tenho isso?". Toda alteração de saldo deve gerar uma movimentação.
- A restrição `CHECK (quantidade >= 0)` faz o banco **recusar** uma venda que deixaria o estoque negativo, mesmo que a aplicação falhe na validação.

### 4.6 Combos e kits como produtos
Um kit é cadastrado na própria tabela `produtos`, com `tipo = 'KIT'`, e sua composição fica em `kit_itens` (auto-relacionamento N:N: produto se relaciona com produto).

Por que não uma tabela `kits` separada? Porque o kit precisa de tudo que um produto já tem — nome, preço, categoria, código de barras, entrar em promoções e, principalmente, ser vendido. Sendo um produto, ele entra em `itens_venda` sem nenhuma alteração na estrutura de vendas, relatórios ou promoções.

- **Estoque:** o kit não tem saldo próprio (não tem linha em `estoque`). Ele é montado na hora da venda a partir dos componentes. A view `vw_disponibilidade_kits` calcula quantos kits podem ser vendidos: o menor valor de *estoque do componente ÷ quantidade no kit*, pois o componente mais escasso limita o kit.
- **Venda de um kit:** o kit vai para `itens_venda` com o preço dele; a baixa de estoque e as movimentações (SAÍDA, VENDA) são feitas **em cada componente**, na mesma transação da venda.
- **Validações na aplicação:** o banco impede que um kit contenha a si mesmo (`CHECK kit_id <> produto_id`), mas garantir que `kit_id` aponte para um produto do tipo KIT e que um kit não contenha outro kit exigiria triggers; essas regras ficam na camada de acesso a dados.

### 4.7 Preço gravado no item da venda
`itens_venda.preco_unitario` copia o preço do produto no momento da venda. Isso **não é redundância**: o preço do produto muda com o tempo, e o histórico precisa mostrar quanto foi cobrado de fato.

### 4.8 Colunas geradas (`GENERATED ALWAYS AS ... STORED`)
`itens_venda.subtotal`, `itens_pedido_compra.subtotal` e `vendas.valor_total` são calculados pelo próprio MySQL. Como ninguém consegue gravar nelas diretamente, elas nunca ficam inconsistentes com os valores de origem. São `STORED` (gravadas em disco) para que relatórios não precisem recalcular.

### 4.9 Totais da venda
`valor_subtotal` e `valor_desconto` ficam no cabeçalho da venda (calculados pela aplicação a partir dos itens) para que histórico e relatórios não precisem somar os itens toda vez. O `valor_total` é derivado deles pela coluna gerada.

### 4.10 Permissões por perfil (ENUM)
`usuarios.perfil` usa ENUM (`ADMIN`, `VENDEDOR`, `ESTOQUISTA`). Uma tabela de perfis/permissões seria mais flexível, mas para três perfis fixos o ENUM atende sem adicionar tabelas. A aplicação decide quais telas cada perfil acessa.

### 4.11 ENUM para status e tipos
Status de venda, forma de pagamento, tipo de movimentação etc. são listas pequenas e estáveis, por isso ENUM: o banco já rejeita valores inválidos.

### 4.12 Restrições CHECK
Validam regras de negócio no banco: CPF/CNPJ só com dígitos, preços positivos, data final da promoção depois da inicial, percentual ≤ 100, movimentação do tipo VENDA precisa ser SAÍDA e ter `venda_id`, etc. Exigem MySQL 8.0.16+ (em versões anteriores são ignoradas silenciosamente).

### 4.13 Regras de negócio na aplicação (sem triggers)
Cálculo do total e baixa de estoque ficam na **camada de acesso a dados**, dentro de uma transação, e não em triggers. Assim a lógica fica visível no código (mais fácil de explicar, testar e manter) e atende ao requisito de uso de transações.

**Fluxo da venda (uma única transação):**
1. `START TRANSACTION`
2. Para cada item: `SELECT quantidade FROM estoque WHERE produto_id = ? FOR UPDATE` (bloqueia o saldo para ninguém vender o mesmo item ao mesmo tempo) e verificar se há quantidade suficiente (se o item for um kit, isso é feito para cada componente)
3. `INSERT` em `vendas`
4. `INSERT` em `itens_venda` para cada item
5. `UPDATE estoque SET quantidade = quantidade - ?` para cada item (kits: para cada componente, multiplicando pela quantidade de kits vendidos)
6. `INSERT` em `movimentacoes_estoque` (tipo SAIDA, motivo VENDA) para cada item
7. `UPDATE vendas` com subtotal e desconto
8. `COMMIT` — ou `ROLLBACK` se qualquer passo falhar (nada fica pela metade)

**Cancelamento:** a venda não é apagada; muda para `CANCELADA` e gera movimentações de ENTRADA com motivo DEVOLUCAO.

**Recebimento de pedido de compra:** status `RECEBIDO` + entrada no estoque + movimentação (ENTRADA, COMPRA), também em transação.

### 4.14 Índices
Além dos índices criados automaticamente para PKs, UKs e FKs, há índices em `clientes.nome` e `produtos.nome` (pesquisas), `vendas.data_venda` (histórico e relatório por período) `produtos.tipo` (separar kits de produtos simples) e `movimentacoes_estoque (produto_id, data_movimentacao)` (extrato de um produto).

### 4.15 Views
- `vw_produtos_estoque_baixo`: produtos ativos com quantidade ≤ estoque mínimo.
- `vw_historico_vendas`: vendas com cliente, vendedor e totais, base para histórico e relatório.
- `vw_disponibilidade_kits`: quantos kits podem ser montados com o estoque atual.

## 5. Normalização

- **1FN:** todos os atributos são atômicos; itens de venda e de pedido estão em tabelas próprias, não em listas dentro de uma coluna.
- **2FN:** nas tabelas com PK composta, os atributos dependem da chave inteira (ex.: `quantidade` depende da venda **e** do produto).
- **3FN:** não há dependências transitivas relevantes — o nome da categoria está em `categorias`, não repetido em `produtos`.

**Simplificação aceita:** o endereço do cliente fica na própria tabela `clientes`. Em rigor, `cidade` e `uf` dependem do `cep` (dependência transitiva), mas criar tabelas de CEP/cidade/estado acrescentaria complexidade sem ganho real para o escopo do projeto.

## 6. Escalabilidade e manutenção

- `movimentacoes_estoque.id` é `BIGINT` por ser a tabela que mais cresce.
- Valores monetários sempre `DECIMAL(10,2)`, nunca `FLOAT` (que gera erros de arredondamento com dinheiro).
- Novas categorias, perfis de promoção ou fornecedores não exigem mudança de estrutura.
- O diferencial não implementado (auditoria) podem ser adicionados depois com novas tabelas, sem alterar as existentes.
