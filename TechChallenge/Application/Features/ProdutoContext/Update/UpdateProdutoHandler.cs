using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext.Update
{
    internal class UpdateProdutoHandler : IRequestHandler<UpdateProdutoRequest>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public UpdateProdutoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(UpdateProdutoRequest request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorId(request.Id);

            produto!.Atualiza(
                request.Nome,
                request.Descricao,
                request.Categoria.ToCategoriaProduto(),
                request.Preco
             );

            await _produtoRepository.Atualizar(produto);
        }
    }
}
