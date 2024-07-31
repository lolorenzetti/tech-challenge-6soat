namespace Application.Features.ProdutoContext
{
    public class ListProdutoResponse
    {
        public List<ProdutoResponse> Produtos { get; set; } = new List<ProdutoResponse>();
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
