using Application.Models.ViewModel;
using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext
{
    public class RequestProdutoByIdHandler : IRequestHandler<RequestProdutoById, ProdutoViewModel>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public RequestProdutoByIdHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }
        public async Task<ProdutoViewModel> Handle(RequestProdutoById request, CancellationToken cancellationToken)
        {
            ProdutoViewModel result = new();

            var produto = await _produtoRepository.ObterPorId(request.Id);

            if (produto is null)
            {
                _notificationContext.AddNotification("NullReference", "Produto não encontrado ou inexistente");
            }
            else
            {
                result.Id = produto.Id;
                result.Nome = produto.Nome;
                result.Descricao = produto.Descricao;
                result.Categoria = produto.Categoria.ToText();
                result.Preco = produto.Preco;
            }

            return result;
        }
    }
}
