using Application.Notifications;
using Domain.Entities;
using Domain.Enuns;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext.Create
{
    public class CreateProdutoHandler : IRequestHandler<CreateProdutoRequest, ProdutoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoPresenter _presenter;

        public CreateProdutoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IProdutoPresenter presenter)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _presenter = presenter;
        }

        public async Task<ProdutoResponse> Handle(CreateProdutoRequest request, CancellationToken cancellationToken)
        {
            CategoriaProduto categoria;

            if (!Enum.TryParse(request.Categoria.ToString(), out categoria))
            {
                _notificationContext.AddNotification("NullReference",
                    $"Categoria com identificador {request.Categoria} produto inválida");
                return null!;
            }

            var produto = new Produto(
                request.Nome,
                request.Descricao,
                categoria,
                request.Preco
            );


            if (produto.Invalid)
            {
                _notificationContext.AddNotifications(produto.GetErrors());
                return null!;
            }

            await _produtoRepository.Adicionar(produto);

            return await _presenter.ToProdutoResponse(produto);
        }
    }
}
