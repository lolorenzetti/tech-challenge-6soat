using Application.Models.ViewModel;

public record ListProdutoViewModel
{
    public List<ProdutoViewModel> Produtos { get; set; } = new List<ProdutoViewModel>();
}