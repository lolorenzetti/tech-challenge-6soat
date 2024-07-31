using MediatR;

namespace Application.Features.ProdutoContext.GetByCategoria
{
    public class GetProdutoByCategoriaRequest : IRequest<ListProdutoResponse>
    {
        public int CategoriaId { get; set; }
    }
}
