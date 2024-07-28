namespace Application.Features.ProdutoContext
{
    public class ListProdutoResponse
    {
        public List<ProdutoResponse> Produtos = new();
    }

    public class ProdutoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
