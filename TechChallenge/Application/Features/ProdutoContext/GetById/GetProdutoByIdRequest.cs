using MediatR;

namespace Application.Features.ProdutoContext.GetById
{
    public class GetProdutoByIdRequest : IRequest<GetProdutoByIdResponse>
    {
        public int Id { get; set; }
    }
}
