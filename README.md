<p align="center">
  <img src="src/Lapapie/Recursos/Marca/assinatura-horizontal-colorido.png" alt="Lapapiê" width="320">
</p>

<h1 align="center">Lapapiê — Sistema Gerenciador de Papelaria</h1>

<p align="center"><em>Projeto final da disciplina de Programação Visual · C# Windows Forms + MySQL</em></p>

---

Sistema desktop para gerenciar uma papelaria: caixa com promoções automáticas, controle de estoque com histórico, pedidos de compra, cadastros, combos e kits, relatórios com gráficos e exportação para PDF e Excel.

## Sumário
- [Tecnologias](#tecnologias)
- [Como executar](#como-executar)
- [Usuários de teste](#usuários-de-teste)
- [Funcionalidades](#funcionalidades)
- [Estrutura do repositório](#estrutura-do-repositório)
- [Documentação técnica](#documentação-técnica)
- [Atendimento aos requisitos](#atendimento-aos-requisitos)
- [Publicando no GitHub](#publicando-no-github)

## Tecnologias

| Item | Versão / pacote |
|---|---|
| Linguagem / interface | C# 12, Windows Forms, **.NET 8** (`net8.0-windows`) |
| Banco de dados | **MySQL 8.0.16 ou superior** (por causa das restrições `CHECK`) |
| Acesso ao banco | `MySql.Data` (conector oficial da Oracle) |
| Excel | `ClosedXML` |
| PDF | `QuestPDF` (licença Community, gratuita para uso acadêmico) |
| IDE | Visual Studio 2022 (17.8+) com a carga de trabalho **"Desenvolvimento para desktop com .NET"** |

Os pacotes NuGet são baixados automaticamente pelo Visual Studio na primeira compilação.

## Como executar

### 1. Criar o banco de dados
Execute os dois scripts da pasta `database/`, **nesta ordem**:

1. `lapapie_create.sql`: cria o banco `lapapie`, as 15 tabelas e as 3 views.
2. `lapapie_seed.sql`: insere a massa de teste (clientes, produtos, 129 vendas, movimentações...).

**Pelo MySQL Workbench:** *File → Open SQL Script* → abra o arquivo → clique no raio ⚡. Faça isso com um script e depois com o outro.

**Pelo terminal (PowerShell)**, na pasta do projeto:
```powershell
mysql -u root -p --default-character-set=utf8mb4 -e "source database/lapapie_create.sql"
```
```powershell
mysql -u root -p --default-character-set=utf8mb4 -e "source database/lapapie_seed.sql"
```
> Use o `--default-character-set=utf8mb4`. Sem ele, o console do Windows pode gravar os acentos errados (ex.: "ImpressÃ£o").

### 2. Configurar a conexão
O arquivo `src/Lapapie/appsettings.json` guarda os dados do MySQL:
```json
{
  "Conexao": { "Servidor": "localhost", "Porta": 3306, "Banco": "lapapie", "Usuario": "root", "Senha": "" }
}
```
Você também pode ajustar a conexão **sem editar arquivos**: na tela de login, clique em **"Configurar conexão com o banco"**, preencha os campos, clique em **Testar** e depois em **Salvar**.

> ⚠️ Não faça commit da sua senha real do MySQL. Se alterar o `appsettings.json` com uma senha, desfaça antes do commit ou use a tela de configuração, que grava na pasta `bin/` (ignorada pelo Git).

### 3. Abrir e rodar
1. Abra `Lapapie.sln` no Visual Studio 2022.
2. Pressione **F5**.

## Usuários de teste

Todos usam a senha **`Lapapie@123`**.

| Login | Perfil | O que acessa |
|---|---|---|
| `admin` | Administrador | Tudo |
| `juliana.martins` | Vendedor | Caixa, histórico de vendas, clientes e consultas |
| `rafael.souza` | Vendedor | Idem |
| `carlos.pereira` | Estoquista | Produtos, estoque, movimentações, fornecedores e pedidos de compra |
| `marcos.lima` | Vendedor **inativo** | Não consegue entrar (demonstra o bloqueio) |

> Na primeira entrada de cada usuário, a senha em texto puro da massa de teste é convertida automaticamente para hash PBKDF2. A senha continua a mesma.

## Funcionalidades

**Vendas**
- **Caixa (Nova venda, F2):** vitrine com imagens, busca por nome ou leitor de código de barras, filtro por categoria, carrinho com cálculo automático do total, promoções aplicadas sozinhas, troco, venda de kits e comprovante em PDF. Atalhos: **F3** busca e **F9** finaliza.
- **Histórico de vendas:** filtros por período, situação e cliente/vendedor/nº; detalhe dos itens; comprovante; cancelamento com devolução ao estoque (somente administrador).
- **Relatório de vendas:** indicadores, faturamento por dia, por categoria, por forma de pagamento e por vendedor, ranking de produtos e exportação para **PDF** (com gráficos) e **Excel** (várias abas).

**Estoque**
- **Controle de estoque:** saldo por produto, situação (normal/baixo/zerado), entrada e saída manual (ajuste, devolução, perda) e parâmetros (mínimo e localização).
- **Estoque baixo:** consulta da view `vw_produtos_estoque_baixo`, com botão que **gera um pedido de compra** sugerindo quantidades e o melhor fornecedor.
- **Movimentações:** histórico completo com filtros.
- **Pedidos de compra:** criar, receber (entrada automática no estoque) e cancelar.

**Cadastros:** produtos (com imagem, fornecedores e custos, margem), **kits e combos**, categorias, clientes (validação de CPF), fornecedores (validação de CNPJ), **promoções** (seleção de produtos) e usuários (perfis).

**Geral:** login, **dashboard** com indicadores e gráficos, permissões por perfil, exportação de qualquer lista para Excel/PDF, tema visual da marca, atalhos **F1** (início) e **F5** (atualizar).

## Estrutura do repositório

```
Lapapie/
├── Lapapie.sln
├── README.md
├── database/                  scripts do banco (criação + massa de teste)
├── docs/                      MER, modelagem física e decisões de projeto
└── src/Lapapie/
    ├── Program.cs             ponto de entrada (login → janela principal)
    ├── appsettings.json       conexão com o MySQL
    ├── Conexao/               classe de conexão (ConexaoBD) e configuração
    ├── Models/                entidades, enums e DTOs das consultas (views/relatórios)
    ├── DAO/                   acesso a dados: SQL parametrizado e transações
    ├── Forms/
    │   ├── FrmLogin, FrmPrincipal, FrmConfigConexao
    │   ├── Inicio/            dashboard
    │   ├── Vendas/            caixa, histórico e relatório
    │   ├── ControleEstoque/   estoque, movimentações, estoque baixo, pedidos de compra
    │   ├── Cadastros/         produtos, kits, categorias, clientes, fornecedores, promoções, usuários
    │   └── Comum/             telas-base, mensagens, exportação, imagens
    ├── Relatorios/            exportação Excel (ClosedXML) e PDF (QuestPDF)
    ├── Controles/             componentes visuais próprios (botões, cartões, gráficos)
    ├── Tema/                  paleta, fontes e ícones da identidade visual
    ├── Util/                  validações, formatação, sessão/permissões, senhas, log
    └── Recursos/              logotipos e ilustrações dos produtos
```

Cada tela tem dois arquivos: `Tela.cs` (código) e `Tela.Designer.cs` (layout). Dá para abrir as telas no **designer visual** do Visual Studio. Compile o projeto uma vez antes, para os controles personalizados aparecerem na Caixa de Ferramentas.

## Documentação técnica
- [docs/MER.md](docs/MER.md): modelo conceitual (entidades, atributos, relacionamentos, cardinalidades) e diagrama.
- [docs/modelagem-fisica.md](docs/modelagem-fisica.md): tabelas, tipos, chaves primárias/estrangeiras, restrições, views e índices.
- [docs/decisoes-de-projeto.md](docs/decisoes-de-projeto.md): normalização, transações, camadas, segurança e identidade visual.

## Atendimento aos requisitos

| Requisito | Onde está |
|---|---|
| MER e modelagem física | `docs/MER.md`, `docs/MER.png`, `docs/modelagem-fisica.md` |
| ≥ 3 relacionamentos N:N | 5: `itens_venda`, `fornecedor_produto`, `promocao_produto`, `itens_pedido_compra`, `kit_itens` |
| ≥ 10 tabelas, PK/FK | 15 tabelas + 3 views |
| Camadas Model / DAO / Formulários / Conexão em pastas | `Models/`, `DAO/`, `Forms/`, `Conexao/` |
| Login | `Forms/FrmLogin.cs`, `UsuarioDAO.Autenticar` |
| CRUD de clientes, produtos, fornecedores, categorias e usuários | `Forms/Cadastros/` + DAOs correspondentes |
| Controle e movimentação de estoque (entrada/saída) | `UcEstoque`, `FrmMovimentacao`, `UcMovimentacoes`, `EstoqueDAO` |
| Registro de venda + cálculo automático do total + baixa no estoque | `UcNovaVenda`, `Venda.RecalcularTotais`, `VendaDAO.RegistrarVenda` |
| Pesquisa de clientes e produtos, filtro por categoria | `UcClientes`, `UcProdutos`, caixa |
| Estoque baixo | `UcEstoqueBaixo` (view `vw_produtos_estoque_baixo`) |
| Histórico e relatório de vendas | `UcHistoricoVendas`, `UcRelatorioVendas`, `RelatorioDAO` |
| Tela principal (dashboard/menu) | `FrmPrincipal`, `UcDashboard` |
| SQL parametrizado | todos os arquivos de `DAO/` |
| Tratamento de exceções | `BancoDadosException`, `RegraNegocioException`, `Mensagem.TratarErro`, `Program.cs` |
| Validação de campos obrigatórios | `Validar()` nos Models + destaque nos campos (`CampoTexto.ComErro`) |
| Transações em operações críticas | venda, cancelamento, recebimento de compra, cadastro de produto/promoção |
| Massa de teste | `database/lapapie_seed.sql` |
| **Extras:** dashboard, gráficos, permissões, PDF, Excel, tema, promoções, kits | todos implementados |

## Publicando no GitHub

No PowerShell, dentro da pasta do projeto. Primeiro crie um repositório **vazio** no GitHub e troque `SEU-USUARIO` pelo seu usuário.

```powershell
git init
```
```powershell
git add .
```
```powershell
git commit -m "Lapapiê: sistema gerenciador de papelaria (C# WinForms + MySQL)"
```
```powershell
git branch -M main
```
```powershell
git remote add origin https://github.com/SEU-USUARIO/lapapie.git
```
```powershell
git push -u origin main
```

O `.gitignore` já exclui `bin/`, `obj/`, `.vs/` e `logs/`.

---
<p align="center"><sub>Identidade visual Lapapiê · paleta papel algodão, tinta grafite, argila rosada, sálvia e lacre dourado.</sub></p>
