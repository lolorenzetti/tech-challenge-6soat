using Application.Notifications;
using Domain.Entities;
using Domain.Enuns;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext.Create
{
    public class CreateProdutoHandler : IRequestHandler<CreateProdutoRequest, int>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public CreateProdutoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }

        public async Task<int> Handle(CreateProdutoRequest request, CancellationToken cancellationToken)
        {
            CategoriaProduto categoria;

            if (!Enum.TryParse(request.Categoria.ToString(), out categoria))
            {
                _notificationContext.AddNotification("NullReference",
                    $"Categoria com identificador {request.Categoria} produto inválida");

                return -1;
            }

            var produto = new Produto(
                request.Nome,
                request.Descricao,
                categoria,
                request.Preco
            );


            if (produto.Invalid)
            {
                _notificationContext.AddNotifications(produto.ValidationResult);
                return -1;
            }

            await _produtoRepository.Adicionar(produto);
            return produto.Id;
        }
    }
}
