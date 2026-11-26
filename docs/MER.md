# Modelo Entidade-Relacionamento (MER) — Lapapiê

Este documento apresenta a **modelagem conceitual** do banco: as entidades, os atributos, os relacionamentos e as cardinalidades. A tradução para tabelas e tipos do MySQL está em [modelagem-fisica.md](modelagem-fisica.md). Os motivos de cada escolha estão em [decisoes-de-projeto.md](decisoes-de-projeto.md).

## Diagrama

![Diagrama entidade-relacionamento](MER.png)

Versão editável (texto) do mesmo diagrama:

> O GitHub desenha o diagrama abaixo automaticamente (sintaxe Mermaid). No Visual Studio Code, use uma extensão de pré-visualização Mermaid.
> Leitura das pontas: `||` = exatamente um · `o|` = zero ou um · `}o` = zero ou muitos · `}|` = um ou muitos.

```mermaid
erDiagram
    USUARIOS ||--o{ VENDAS : "registra"
    USUARIOS ||--o{ PEDIDOS_COMPRA : "emite"
    USUARIOS ||--o{ MOVIMENTACOES_ESTOQUE : "realiza"
    CLIENTES |o--o{ VENDAS : "compra (opcional)"
    CATEGORIAS ||--o{ PRODUTOS : "agrupa"
    PRODUTOS ||--o| ESTOQUE : "tem saldo (só SIMPLES)"
    PRODUTOS ||--o{ KIT_ITENS : "é kit de"
    PRODUTOS ||--o{ KIT_ITENS : "compõe"
    FORNECEDORES ||--o{ FORNECEDOR_PRODUTO : "oferece"
    PRODUTOS ||--o{ FORNECEDOR_PRODUTO : "é oferecido"
    PROMOCOES ||--|{ PROMOCAO_PRODUTO : "inclui"
    PRODUTOS ||--o{ PROMOCAO_PRODUTO : "participa"
    VENDAS ||--|{ ITENS_VENDA : "contém"
    PRODUTOS ||--o{ ITENS_VENDA : "é vendido em"
    FORNECEDORES ||--o{ PEDIDOS_COMPRA : "recebe"
    PEDIDOS_COMPRA ||--|{ ITENS_PEDIDO_COMPRA : "contém"
    PRODUTOS ||--o{ ITENS_PEDIDO_COMPRA : "é comprado em"
    PRODUTOS ||--o{ MOVIMENTACOES_ESTOQUE : "movimenta"
    VENDAS |o--o{ MOVIMENTACOES_ESTOQUE : "origina"
    PEDIDOS_COMPRA |o--o{ MOVIMENTACOES_ESTOQUE : "origina"

    USUARIOS {
        int id PK
        string nome
        string login UK
        string senha "hash PBKDF2"
        enum perfil "ADMIN, VENDEDOR, ESTOQUISTA"
        bool ativo
        datetime ultimo_login
        datetime criado_em
    }
    CLIENTES {
        int id PK
        string nome
        string cpf UK "opcional"
        string email
        string telefone
        string cep
        string logradouro
        string numero
        string complemento
        string bairro
        string cidade
        string uf
        bool ativo
        datetime criado_em
    }
    CATEGORIAS {
        int id PK
        string nome UK
        string descricao
        bool ativo
    }
    FORNECEDORES {
        int id PK
        string razao_social
        string nome_fantasia
        string cnpj UK
        string email
        string telefone
        string nome_contato
        string cidade
        string uf
        bool ativo
        datetime criado_em
    }
    PRODUTOS {
        int id PK
        int categoria_id FK
        string codigo_barras UK
        string nome
        string descricao
        string marca
        enum tipo "SIMPLES ou KIT"
        string unidade_medida
        decimal preco_venda
        bool ativo
        datetime criado_em
        datetime atualizado_em
    }
    ESTOQUE {
        int produto_id PK, FK
        int quantidade
        int estoque_minimo
        string localizacao
        datetime atualizado_em
    }
    KIT_ITENS {
        int kit_id PK, FK
        int produto_id PK, FK
        int quantidade
    }
    FORNECEDOR_PRODUTO {
        int fornecedor_id PK, FK
        int produto_id PK, FK
        string codigo_no_fornecedor
        decimal preco_custo
        int prazo_entrega_dias
    }
    PROMOCOES {
        int id PK
        string nome
        string descricao
        enum tipo_desconto "PERCENTUAL ou VALOR_FIXO"
        decimal valor_desconto
        datetime data_inicio
        datetime data_fim
        bool ativo
        datetime criado_em
    }
    PROMOCAO_PRODUTO {
        int promocao_id PK, FK
        int produto_id PK, FK
    }
    VENDAS {
        int id PK
        int cliente_id FK "NULL = não identificado"
        int usuario_id FK
        datetime data_venda
        decimal valor_subtotal
        decimal valor_desconto
        decimal valor_total "coluna gerada"
        enum forma_pagamento
        enum status "CONCLUIDA ou CANCELADA"
        string observacao
    }
    ITENS_VENDA {
        int venda_id PK, FK
        int produto_id PK, FK
        int quantidade
        decimal preco_unitario "preço no momento da venda"
        decimal desconto
        decimal subtotal "coluna gerada"
    }
    PEDIDOS_COMPRA {
        int id PK
        int fornecedor_id FK
        int usuario_id FK
        datetime data_pedido
        date data_prevista_entrega
        datetime data_recebimento
        enum status "ABERTO, RECEBIDO, CANCELADO"
        decimal valor_total
        string observacao
    }
    ITENS_PEDIDO_COMPRA {
        int pedido_compra_id PK, FK
        int produto_id PK, FK
        int quantidade
        decimal custo_unitario
        decimal subtotal "coluna gerada"
    }
    MOVIMENTACOES_ESTOQUE {
        bigint id PK
        int produto_id FK
        int usuario_id FK
        enum tipo "ENTRADA ou SAIDA"
        enum motivo "VENDA, COMPRA, AJUSTE, DEVOLUCAO, PERDA"
        int quantidade
        int venda_id FK "opcional"
        int pedido_compra_id FK "opcional"
        string observacao
        datetime data_movimentacao
    }
```

## Entidades

| Entidade | O que representa | Identificador |
|---|---|---|
| **Usuário** | Pessoa que acessa o sistema, com um perfil de permissão | `id` (e `login` único) |
| **Cliente** | Quem compra; CPF e endereço são opcionais (balcão) | `id` (e `cpf` único quando informado) |
| **Categoria** | Grupo de produtos (Cadernos, Escrita, Informática...) | `id` (e `nome` único) |
| **Fornecedor** | Empresa que vende para a papelaria | `id` (e `cnpj` único) |
| **Produto** | Item à venda. Pode ser **SIMPLES** ou **KIT** (combo de outros produtos) | `id` (e `codigo_barras` único) |
| **Estoque** | Saldo atual, estoque mínimo e localização de um produto simples | `produto_id` |
| **Promoção** | Desconto com período de validade | `id` |
| **Venda** | Cabeçalho da venda: cliente, vendedor, totais e pagamento | `id` |
| **Pedido de compra** | Compra feita a um fornecedor | `id` |
| **Movimentação de estoque** | Registro histórico de cada entrada/saída | `id` |

**Entidades associativas** (nascem dos relacionamentos N:N e têm atributos próprios): **Item de venda**, **Item de pedido de compra**, **Fornecedor × Produto**, **Promoção × Produto** e **Item de kit**.

## Relacionamentos e cardinalidades

| Relacionamento | Cardinalidade | Leitura |
|---|---|---|
| Categoria — Produto | 1 : N | Uma categoria agrupa vários produtos; cada produto tem exatamente uma categoria. |
| Produto — Estoque | 1 : 0..1 | Todo produto **simples** tem uma linha de estoque; kits não têm (a disponibilidade deles é calculada). |
| **Venda — Produto** | **N : N** | Uma venda tem vários produtos e um produto aparece em várias vendas → `itens_venda` (quantidade, preço do momento, desconto). |
| **Fornecedor — Produto** | **N : N** | Um fornecedor vende vários produtos e um produto pode ter vários fornecedores → `fornecedor_produto` (custo, prazo, código). |
| **Promoção — Produto** | **N : N** | Uma promoção vale para vários produtos e um produto pode estar em várias promoções → `promocao_produto`. |
| **Pedido de compra — Produto** | **N : N** | Um pedido tem vários produtos e um produto aparece em vários pedidos → `itens_pedido_compra` (quantidade, custo). |
| **Kit — Produto** (autorrelacionamento) | **N : N** | Um kit é composto por vários produtos e um produto pode estar em vários kits → `kit_itens` (quantidade). |
| Cliente — Venda | 0..1 : N | Um cliente faz várias vendas; a venda pode não ter cliente (consumidor não identificado). |
| Usuário — Venda / Pedido / Movimentação | 1 : N | Toda operação registra quem a fez (auditoria). |
| Fornecedor — Pedido de compra | 1 : N | Cada pedido é feito a um único fornecedor. |
| Produto — Movimentação | 1 : N | Cada produto acumula seu histórico de entradas e saídas. |
| Venda / Pedido — Movimentação | 0..1 : N | A movimentação aponta a origem quando vem de uma venda ou de uma compra; ajustes manuais não têm origem. |

São **5 relacionamentos muitos-para-muitos** (o enunciado pede no mínimo 3).

## Regras de negócio representadas no modelo

- **Estoque nunca negativo** (`CHECK quantidade >= 0`) e baixa feita **em transação** junto com a venda.
- **Kit não contém a si mesmo** (`CHECK kit_id <> produto_id`).
- **Promoção coerente**: fim depois do início; percentual até 100%.
- **Movimentação coerente com o motivo**: VENDA é sempre saída e tem `venda_id`; COMPRA é sempre entrada e tem `pedido_compra_id`; PERDA é sempre saída.
- **Pedido recebido tem data de recebimento** (`CHECK status <> 'RECEBIDO' OR data_recebimento IS NOT NULL`).
- **Totais calculados pelo banco** (`valor_total`, `subtotal` são colunas geradas) — não há como gravar um total que não bate com os itens.
