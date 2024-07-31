using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext.GetByCategoria
{
    public class RequestProdutoByCategoriaHandler : IRequestHandler<GetProdutoByCategoriaRequest, ListProdutoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoPresenter _presenter;

        public RequestProdutoByCategoriaHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IProdutoPresenter presenter)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _presenter = presenter;
        }

        public async Task<ListProdutoResponse> Handle(GetProdutoByCategoriaRequest request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository
                .ObterPorCategoria(request.CategoriaId.ToCategoriaProduto());

            return await _presenter.ToListProdutoResponse(produtos.ToList());
        }
    }
}
