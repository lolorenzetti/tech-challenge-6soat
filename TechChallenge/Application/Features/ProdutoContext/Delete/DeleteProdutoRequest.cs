using MediatR;

namespace Application.Features.ProdutoContext.Delete
{
    public class DeleteProdutoRequest : IRequest
    {
        public int Id { get; set; }
    }
}
