using MediatR;

namespace Application.Features.ProdutoContext.GetByCategoria
{
    public class GetProdutoByCategoriaRequest : IRequest<GetProdutoByCategoriaResponse>
    {
        public int CategoriaId { get; set; }
    }
}
