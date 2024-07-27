namespace Application.Features.ProdutoContext.GetByCategoria
{
    public class GetProdutoByCategoriaResponse
    {
        public List<ProdutoResponse> Produtos = new();
    }

    public class ProdutoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
