using Domain.Entities;

namespace Application.Features.ProdutoContext
{
    public interface IProdutoPresenter
    {
        public Task<ListProdutoResponse> ToListProdutoResponse(List<Produto> produtos);
        public Task<ProdutoResponse> ToProdutoResponse(Produto produto);
    }
}
