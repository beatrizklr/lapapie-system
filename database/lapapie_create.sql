-- =====================================================================
-- Lapapiê — Script de criação do banco de dados
-- SGBD: MySQL 8.0.16 ou superior (necessário para as restrições CHECK)
-- Projeto final — Programação Visual
-- ---------------------------------------------------------------------
-- Ordem de criação: primeiro as tabelas que não dependem de ninguém,
-- depois as que têm chaves estrangeiras apontando para elas.
-- =====================================================================

DROP DATABASE IF EXISTS lapapie;

-- utf8mb4: suporta qualquer caractere (acentos, ç, emojis).
-- utf8mb4_0900_ai_ci: comparação que ignora acentos e maiúsculas,
-- então pesquisar "lapis" encontra "Lápis".
CREATE DATABASE lapapie
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_0900_ai_ci;

USE lapapie;

-- =====================================================================
-- 1. USUÁRIOS — quem acessa o sistema (login e permissões)
-- =====================================================================
CREATE TABLE usuarios (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    nome            VARCHAR(100)    NOT NULL,
    login           VARCHAR(50)     NOT NULL,
    senha           VARCHAR(100)    NOT NULL,
    perfil          ENUM('ADMIN', 'VENDEDOR', 'ESTOQUISTA') NOT NULL DEFAULT 'VENDEDOR',
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,
    ultimo_login    DATETIME        NULL,
    criado_em       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT uk_usuarios_login UNIQUE (login)
) ENGINE = InnoDB;

-- =====================================================================
-- 2. CLIENTES
-- =====================================================================
CREATE TABLE clientes (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    nome            VARCHAR(100)    NOT NULL,
    cpf             CHAR(11)        NULL,      -- só números; opcional
    email           VARCHAR(100)    NULL,
    telefone        VARCHAR(20)     NULL,
    cep             CHAR(8)         NULL,
    logradouro      VARCHAR(150)    NULL,
    numero          VARCHAR(10)     NULL,
    complemento     VARCHAR(50)     NULL,
    bairro          VARCHAR(80)     NULL,
    cidade          VARCHAR(80)     NULL,
    uf              CHAR(2)         NULL,
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,
    criado_em       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT uk_clientes_cpf UNIQUE (cpf),
    CONSTRAINT ck_clientes_cpf CHECK (cpf REGEXP '^[0-9]{11}$'),
    CONSTRAINT ck_clientes_cep CHECK (cep REGEXP '^[0-9]{8}$'),
    INDEX idx_clientes_nome (nome)             -- acelera a pesquisa por nome
) ENGINE = InnoDB;

-- =====================================================================
-- 3. CATEGORIAS
-- =====================================================================
CREATE TABLE categorias (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    nome            VARCHAR(60)     NOT NULL,
    descricao       VARCHAR(255)    NULL,
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,

    PRIMARY KEY (id),
    CONSTRAINT uk_categorias_nome UNIQUE (nome)
) ENGINE = InnoDB;

-- =====================================================================
-- 4. FORNECEDORES
-- =====================================================================
CREATE TABLE fornecedores (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    razao_social    VARCHAR(150)    NOT NULL,
    nome_fantasia   VARCHAR(100)    NULL,
    cnpj            CHAR(14)        NOT NULL,  -- só números
    email           VARCHAR(100)    NULL,
    telefone        VARCHAR(20)     NULL,
    nome_contato    VARCHAR(100)    NULL,
    cidade          VARCHAR(80)     NULL,
    uf              CHAR(2)         NULL,
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,
    criado_em       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT uk_fornecedores_cnpj UNIQUE (cnpj),
    CONSTRAINT ck_fornecedores_cnpj CHECK (cnpj REGEXP '^[0-9]{14}$')
) ENGINE = InnoDB;

-- =====================================================================
-- 5. PRODUTOS
-- =====================================================================
CREATE TABLE produtos (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    categoria_id    INT UNSIGNED    NOT NULL,
    codigo_barras   VARCHAR(14)     NULL,
    nome            VARCHAR(120)    NOT NULL,
    descricao       VARCHAR(500)    NULL,
    marca           VARCHAR(60)     NULL,
    tipo            ENUM('SIMPLES', 'KIT') NOT NULL DEFAULT 'SIMPLES',  -- KIT = combo de outros produtos
    unidade_medida  VARCHAR(10)     NOT NULL DEFAULT 'UN',  -- UN, CX, PCT, RESMA...
    preco_venda     DECIMAL(10,2)   NOT NULL,
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,
    criado_em       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    atualizado_em   DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP
                                    ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT uk_produtos_codigo_barras UNIQUE (codigo_barras),
    CONSTRAINT ck_produtos_preco_venda CHECK (preco_venda > 0),
    CONSTRAINT fk_produtos_categoria
        FOREIGN KEY (categoria_id) REFERENCES categorias (id),
    INDEX idx_produtos_nome (nome),
    INDEX idx_produtos_tipo (tipo)
) ENGINE = InnoDB;

-- =====================================================================
-- 6. ESTOQUE — saldo atual de cada produto SIMPLES (relação 1:1 com produtos)
--    Kits não têm saldo próprio: a disponibilidade deles vem dos
--    componentes (ver vw_disponibilidade_kits).
-- =====================================================================
CREATE TABLE estoque (
    produto_id      INT UNSIGNED    NOT NULL,  -- é PK e FK ao mesmo tempo (garante o 1:1)
    quantidade      INT             NOT NULL DEFAULT 0,
    estoque_minimo  INT             NOT NULL DEFAULT 5,
    localizacao     VARCHAR(50)     NULL,      -- ex.: "Corredor 2 - Prateleira B"
    atualizado_em   DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP
                                    ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (produto_id),
    CONSTRAINT ck_estoque_quantidade CHECK (quantidade >= 0),     -- nunca vende o que não tem
    CONSTRAINT ck_estoque_minimo     CHECK (estoque_minimo >= 0),
    CONSTRAINT fk_estoque_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id)
        ON DELETE CASCADE
) ENGINE = InnoDB;

-- =====================================================================
-- 7. KIT_ITENS — composição dos combos e kits
--    Auto-relacionamento N:N de produtos: um kit (produto do tipo KIT)
--    contém vários produtos, e um produto pode estar em vários kits.
--    Ex.: "Kit Volta às Aulas" = 2 cadernos + 1 estojo + 3 canetas.
-- =====================================================================
CREATE TABLE kit_itens (
    kit_id          INT UNSIGNED    NOT NULL,  -- produto do tipo KIT
    produto_id      INT UNSIGNED    NOT NULL,  -- produto SIMPLES que compõe o kit
    quantidade      INT UNSIGNED    NOT NULL DEFAULT 1,

    PRIMARY KEY (kit_id, produto_id),
    CONSTRAINT ck_kit_itens_quantidade CHECK (quantidade > 0),
    CONSTRAINT ck_kit_itens_diferentes CHECK (kit_id <> produto_id),  -- kit não contém a si mesmo
    CONSTRAINT fk_kit_itens_kit
        FOREIGN KEY (kit_id) REFERENCES produtos (id),
    CONSTRAINT fk_kit_itens_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id)
) ENGINE = InnoDB;

-- =====================================================================
-- 8. FORNECEDOR_PRODUTO — N:N entre fornecedores e produtos
--    (quais fornecedores vendem quais produtos, e por quanto)
-- =====================================================================
CREATE TABLE fornecedor_produto (
    fornecedor_id        INT UNSIGNED       NOT NULL,
    produto_id           INT UNSIGNED       NOT NULL,
    codigo_no_fornecedor VARCHAR(30)        NULL,
    preco_custo          DECIMAL(10,2)      NOT NULL,
    prazo_entrega_dias   SMALLINT UNSIGNED  NULL,

    PRIMARY KEY (fornecedor_id, produto_id),
    CONSTRAINT ck_fornecedor_produto_custo CHECK (preco_custo >= 0),
    CONSTRAINT fk_fornecedor_produto_fornecedor
        FOREIGN KEY (fornecedor_id) REFERENCES fornecedores (id)
        ON DELETE CASCADE,
    CONSTRAINT fk_fornecedor_produto_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id)
        ON DELETE CASCADE
) ENGINE = InnoDB;

-- =====================================================================
-- 9. PROMOÇÕES
-- =====================================================================
CREATE TABLE promocoes (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    nome            VARCHAR(100)    NOT NULL,
    descricao       VARCHAR(255)    NULL,
    tipo_desconto   ENUM('PERCENTUAL', 'VALOR_FIXO') NOT NULL,
    valor_desconto  DECIMAL(10,2)   NOT NULL,
    data_inicio     DATETIME        NOT NULL,
    data_fim        DATETIME        NOT NULL,
    ativo           BOOLEAN         NOT NULL DEFAULT TRUE,
    criado_em       DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT ck_promocoes_periodo CHECK (data_fim > data_inicio),
    CONSTRAINT ck_promocoes_valor   CHECK (valor_desconto > 0),
    CONSTRAINT ck_promocoes_percentual
        CHECK (tipo_desconto <> 'PERCENTUAL' OR valor_desconto <= 100)
) ENGINE = InnoDB;

-- =====================================================================
-- 10. PROMOCAO_PRODUTO — N:N entre promoções e produtos
-- =====================================================================
CREATE TABLE promocao_produto (
    promocao_id     INT UNSIGNED    NOT NULL,
    produto_id      INT UNSIGNED    NOT NULL,

    PRIMARY KEY (promocao_id, produto_id),
    CONSTRAINT fk_promocao_produto_promocao
        FOREIGN KEY (promocao_id) REFERENCES promocoes (id)
        ON DELETE CASCADE,
    CONSTRAINT fk_promocao_produto_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id)
        ON DELETE CASCADE
) ENGINE = InnoDB;

-- =====================================================================
-- 11. VENDAS — cabeçalho da venda
-- =====================================================================
CREATE TABLE vendas (
    id              INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    cliente_id      INT UNSIGNED    NULL,      -- NULL = consumidor não identificado
    usuario_id      INT UNSIGNED    NOT NULL,  -- quem registrou a venda
    data_venda      DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor_subtotal  DECIMAL(10,2)   NOT NULL DEFAULT 0.00,  -- soma de quantidade x preço
    valor_desconto  DECIMAL(10,2)   NOT NULL DEFAULT 0.00,  -- soma dos descontos dos itens
    -- Coluna gerada: o próprio MySQL calcula, então nunca fica inconsistente
    valor_total     DECIMAL(10,2)   GENERATED ALWAYS AS (valor_subtotal - valor_desconto) STORED,
    forma_pagamento ENUM('DINHEIRO', 'PIX', 'CARTAO_DEBITO', 'CARTAO_CREDITO') NOT NULL,
    status          ENUM('CONCLUIDA', 'CANCELADA') NOT NULL DEFAULT 'CONCLUIDA',
    observacao      VARCHAR(255)    NULL,

    PRIMARY KEY (id),
    CONSTRAINT ck_vendas_valores
        CHECK (valor_subtotal >= 0 AND valor_desconto >= 0 AND valor_desconto <= valor_subtotal),
    CONSTRAINT fk_vendas_cliente
        FOREIGN KEY (cliente_id) REFERENCES clientes (id),
    CONSTRAINT fk_vendas_usuario
        FOREIGN KEY (usuario_id) REFERENCES usuarios (id),
    INDEX idx_vendas_data (data_venda)         -- relatórios e histórico filtram por data
) ENGINE = InnoDB;

-- =====================================================================
-- 12. ITENS_VENDA — N:N entre vendas e produtos
-- =====================================================================
CREATE TABLE itens_venda (
    venda_id        INT UNSIGNED    NOT NULL,
    produto_id      INT UNSIGNED    NOT NULL,
    quantidade      INT UNSIGNED    NOT NULL,
    preco_unitario  DECIMAL(10,2)   NOT NULL,  -- preço NO MOMENTO da venda (histórico)
    desconto        DECIMAL(10,2)   NOT NULL DEFAULT 0.00,  -- desconto total do item
    subtotal        DECIMAL(10,2)   GENERATED ALWAYS AS (quantidade * preco_unitario - desconto) STORED,

    PRIMARY KEY (venda_id, produto_id),
    CONSTRAINT ck_itens_venda_quantidade CHECK (quantidade > 0),
    CONSTRAINT ck_itens_venda_preco      CHECK (preco_unitario >= 0),
    CONSTRAINT ck_itens_venda_desconto
        CHECK (desconto >= 0 AND desconto <= quantidade * preco_unitario),
    CONSTRAINT fk_itens_venda_venda
        FOREIGN KEY (venda_id) REFERENCES vendas (id)
        ON DELETE CASCADE,
    CONSTRAINT fk_itens_venda_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id)
) ENGINE = InnoDB;

-- =====================================================================
-- 13. PEDIDOS_COMPRA — compras feitas aos fornecedores
-- =====================================================================
CREATE TABLE pedidos_compra (
    id                    INT UNSIGNED    NOT NULL AUTO_INCREMENT,
    fornecedor_id         INT UNSIGNED    NOT NULL,
    usuario_id            INT UNSIGNED    NOT NULL,
    data_pedido           DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_prevista_entrega DATE            NULL,
    data_recebimento      DATETIME        NULL,
    status                ENUM('ABERTO', 'RECEBIDO', 'CANCELADO') NOT NULL DEFAULT 'ABERTO',
    valor_total           DECIMAL(10,2)   NOT NULL DEFAULT 0.00,
    observacao            VARCHAR(255)    NULL,

    PRIMARY KEY (id),
    CONSTRAINT ck_pedidos_compra_valor CHECK (valor_total >= 0),
    CONSTRAINT ck_pedidos_compra_recebimento
        CHECK (status <> 'RECEBIDO' OR data_recebimento IS NOT NULL),
    CONSTRAINT fk_pedidos_compra_fornecedor
        FOREIGN KEY (fornecedor_id) REFERENCES fornecedores (id),
    CONSTRAINT fk_pedidos_compra_usuario
        FOREIGN KEY (usuario_id) REFERENCES usuarios (id)
) ENGINE = InnoDB;

-- =====================================================================
-- 14. ITENS_PEDIDO_COMPRA — N:N entre pedidos de compra e produtos
-- =====================================================================
CREATE TABLE itens_pedido_compra (
    pedido_compra_id INT UNSIGNED   NOT NULL,
    produto_id       INT UNSIGNED   NOT NULL,
    quantidade       INT UNSIGNED   NOT NULL,
    custo_unitario   DECIMAL(10,2)  NOT NULL,
    subtotal         DECIMAL(10,2)  GENERATED ALWAYS AS (quantidade * custo_unitario) STORED,

    PRIMARY KEY (pedido_compra_id, produto_id),
    CONSTRAINT ck_itens_pedido_quantidade CHECK (quantidade > 0),
    CONSTRAINT ck_itens_pedido_custo      CHECK (custo_unitario >= 0),
    CONSTRAINT fk_itens_pedido_pedido
        FOREIGN KEY (pedido_compra_id) REFERENCES pedidos_compra (id)
        ON DELETE CASCADE,
    CONSTRAINT fk_itens_pedido_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id)
) ENGINE = InnoDB;

-- =====================================================================
-- 15. MOVIMENTACOES_ESTOQUE — histórico de toda entrada e saída
-- =====================================================================
CREATE TABLE movimentacoes_estoque (
    id                BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,  -- tabela que mais cresce
    produto_id        INT UNSIGNED    NOT NULL,
    usuario_id        INT UNSIGNED    NOT NULL,
    tipo              ENUM('ENTRADA', 'SAIDA') NOT NULL,
    motivo            ENUM('VENDA', 'COMPRA', 'AJUSTE', 'DEVOLUCAO', 'PERDA') NOT NULL,
    quantidade        INT UNSIGNED    NOT NULL,
    venda_id          INT UNSIGNED    NULL,    -- preenchido quando a origem é uma venda
    pedido_compra_id  INT UNSIGNED    NULL,    -- preenchido quando a origem é uma compra
    observacao        VARCHAR(255)    NULL,
    data_movimentacao DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (id),
    CONSTRAINT ck_mov_quantidade CHECK (quantidade > 0),
    -- Coerência entre motivo, tipo e origem:
    CONSTRAINT ck_mov_venda
        CHECK (motivo <> 'VENDA'  OR (tipo = 'SAIDA'   AND venda_id IS NOT NULL)),
    CONSTRAINT ck_mov_compra
        CHECK (motivo <> 'COMPRA' OR (tipo = 'ENTRADA' AND pedido_compra_id IS NOT NULL)),
    CONSTRAINT ck_mov_perda
        CHECK (motivo <> 'PERDA'  OR tipo = 'SAIDA'),
    CONSTRAINT fk_mov_produto
        FOREIGN KEY (produto_id) REFERENCES produtos (id),
    CONSTRAINT fk_mov_usuario
        FOREIGN KEY (usuario_id) REFERENCES usuarios (id),
    CONSTRAINT fk_mov_venda
        FOREIGN KEY (venda_id) REFERENCES vendas (id),
    CONSTRAINT fk_mov_pedido_compra
        FOREIGN KEY (pedido_compra_id) REFERENCES pedidos_compra (id),
    INDEX idx_mov_produto_data (produto_id, data_movimentacao)
) ENGINE = InnoDB;

-- =====================================================================
-- VIEWS — consultas prontas usadas pelas telas
-- =====================================================================

-- Consulta de produtos com estoque baixo
CREATE VIEW vw_produtos_estoque_baixo AS
SELECT
    p.id                              AS produto_id,
    p.nome                            AS produto,
    c.nome                            AS categoria,
    e.quantidade,
    e.estoque_minimo,
    (e.estoque_minimo - e.quantidade) AS deficit
FROM produtos p
JOIN categorias c ON c.id = p.categoria_id
JOIN estoque    e ON e.produto_id = p.id
WHERE p.ativo = TRUE
  AND e.quantidade <= e.estoque_minimo;

-- Histórico de vendas (base para a tela de histórico e o relatório)
CREATE VIEW vw_historico_vendas AS
SELECT
    v.id                                          AS venda_id,
    v.data_venda,
    COALESCE(cl.nome, 'Consumidor não identificado') AS cliente,
    u.nome                                        AS vendedor,
    v.forma_pagamento,
    v.status,
    SUM(iv.quantidade)                            AS total_itens,
    v.valor_subtotal,
    v.valor_desconto,
    v.valor_total
FROM vendas v
JOIN usuarios        u  ON u.id = v.usuario_id
LEFT JOIN clientes   cl ON cl.id = v.cliente_id
LEFT JOIN itens_venda iv ON iv.venda_id = v.id
GROUP BY v.id, v.data_venda, cl.nome, u.nome, v.forma_pagamento, v.status,
         v.valor_subtotal, v.valor_desconto, v.valor_total;

-- Quantos kits dá para montar com o estoque atual dos componentes.
-- É o MENOR valor entre (estoque do componente / quantidade exigida no kit):
-- o componente mais escasso limita o kit.
CREATE VIEW vw_disponibilidade_kits AS
SELECT
    k.id                                        AS kit_id,
    k.nome                                      AS kit,
    k.preco_venda,
    MIN(FLOOR(e.quantidade / ki.quantidade))    AS quantidade_disponivel
FROM produtos k
JOIN kit_itens ki ON ki.kit_id = k.id
JOIN estoque   e  ON e.produto_id = ki.produto_id
WHERE k.tipo = 'KIT'
  AND k.ativo = TRUE
GROUP BY k.id, k.nome, k.preco_venda;
