using MediatR;

namespace Application.Features.ProdutoContext.GetById
{
    public class GetProdutoByIdRequest : IRequest<ProdutoResponse>
    {
        public int Id { get; set; }
    }
}
