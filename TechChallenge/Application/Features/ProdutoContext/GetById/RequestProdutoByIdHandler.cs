using Application.Notifications;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext.GetById
{
    public class RequestProdutoByIdHandler : IRequestHandler<GetProdutoByIdRequest, ProdutoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoPresenter _produtoPresenter;

        public RequestProdutoByIdHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IProdutoPresenter produtoPresenter)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _produtoPresenter = produtoPresenter;
        }
        public async Task<ProdutoResponse> Handle(GetProdutoByIdRequest request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(request.Id);
            return await _produtoPresenter.ToProdutoResponse(produto!);
        }
    }
}
