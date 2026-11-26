namespace Lapapie.Forms.Comum
{
    /// <summary>
    /// Carrega as ilustrações dos produtos da pasta Recursos/Produtos.
    /// <para>
    /// Decisão de projeto: o banco não tem coluna de imagem, então a imagem é
    /// associada pelo id do produto ("12.png"). Produtos sem imagem usam a
    /// ilustração genérica da categoria ("categoria-3.png"). As imagens ficam em
    /// cache para não serem lidas do disco a cada pintura.
    /// </para>
    /// </summary>
    public static class ImagensProduto
    {
        private static readonly Dictionary<string, Image> Cache = new();

        public static string Pasta => Path.Combine(AppContext.BaseDirectory, "Recursos", "Produtos");

        public static string CaminhoDoProduto(int produtoId) => Path.Combine(Pasta, $"{produtoId}.png");

        public static bool TemImagemPropria(int produtoId) => produtoId > 0 && File.Exists(CaminhoDoProduto(produtoId));

        /// <summary>Imagem do produto (ou da categoria, ou genérica) no tamanho pedido.</summary>
        public static Image Obter(int produtoId, int categoriaId, int tamanho = 480)
        {
            string[] candidatos =
            {
                CaminhoDoProduto(produtoId),
                Path.Combine(Pasta, $"categoria-{categoriaId}.png"),
                Path.Combine(Pasta, "sem-imagem.png")
            };
            var caminho = candidatos.FirstOrDefault(File.Exists);
            if (caminho == null) return null;

            string chave = caminho + "|" + tamanho;
            lock (Cache)
            {
                if (Cache.TryGetValue(chave, out var imagem)) return imagem;
                imagem = Carregar(caminho, tamanho);
                Cache[chave] = imagem;
                return imagem;
            }
        }

        /// <summary>Lê o arquivo sem deixá-lo travado (permite substituí-lo depois).</summary>
        private static Image Carregar(string caminho, int tamanho)
        {
            using var memoria = new MemoryStream(File.ReadAllBytes(caminho));
            using var original = Image.FromStream(memoria);
            var redimensionada = new Bitmap(tamanho, tamanho);
            using var g = Graphics.FromImage(redimensionada);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(original, 0, 0, tamanho, tamanho);
            return redimensionada;
        }

        /// <summary>Copia uma imagem escolhida pela pessoa para a pasta de produtos (convertendo para PNG quadrado).</summary>
        public static void Salvar(int produtoId, string caminhoOrigem)
        {
            Directory.CreateDirectory(Pasta);
            using var memoria = new MemoryStream(File.ReadAllBytes(caminhoOrigem));
            using var original = Image.FromStream(memoria);
            int lado = 480;
            using var destino = new Bitmap(lado, lado);
            using (var g = Graphics.FromImage(destino))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                float escala = Math.Min((float)lado / original.Width, (float)lado / original.Height);
                float w = original.Width * escala, h = original.Height * escala;
                g.DrawImage(original, (lado - w) / 2, (lado - h) / 2, w, h);
            }
            destino.Save(CaminhoDoProduto(produtoId), System.Drawing.Imaging.ImageFormat.Png);
            LimparCache();
        }

        public static void LimparCache()
        {
            // As imagens antigas NÃO são descartadas com Dispose(): elas podem estar sendo
            // exibidas em alguma tela aberta. O coletor de lixo as libera quando ninguém mais usar.
            lock (Cache) Cache.Clear();
        }

        public static Image Recurso(string nomeArquivo)
        {
            var caminho = Path.Combine(AppContext.BaseDirectory, "Recursos", nomeArquivo);
            if (!File.Exists(caminho)) return null;
            using var memoria = new MemoryStream(File.ReadAllBytes(caminho));
            return new Bitmap(Image.FromStream(memoria));
        }
    }
}
