using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext.Update
{
    internal class UpdateProdutoHandler : IRequestHandler<UpdateProdutoRequest, ProdutoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoPresenter _produtoPresenter;

        public UpdateProdutoHandler(
            NotificationContext notificationContext,
            IProdutoRepository produtoRepository,
            IProdutoPresenter produtoPresenter)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _produtoPresenter = produtoPresenter;
        }

        public async Task<ProdutoResponse> Handle(UpdateProdutoRequest request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(request.Id);

            if (produto == null)
            {
                _notificationContext.AddNotification("NullReference", "Produto não encontrado!");
                return null!;
            }

            produto.Atualiza(request.Nome, request.Descricao, request.Categoria.ToCategoriaProduto(), request.Preco);

            await _produtoRepository.Atualizar(produto);

            return await _produtoPresenter.ToProdutoResponse(produto);
        }
    }
}
