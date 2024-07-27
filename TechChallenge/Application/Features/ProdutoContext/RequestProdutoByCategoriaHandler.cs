using Application.Models.ViewModel;
using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.ProdutoContext
{
    public class RequestProdutoByCategoriaHandler : IRequestHandler<RequestProdutoByCategoria, ListProdutoViewModel>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public RequestProdutoByCategoriaHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }

        public async Task<ListProdutoViewModel> Handle(RequestProdutoByCategoria request, CancellationToken cancellationToken)
        {
            ListProdutoViewModel result = new();

            var produtos = await _produtoRepository
                .ObterPorCategoria(request.CategoriaId.ToCategoriaProduto());

            foreach (var p in produtos)
            {
                result.Produtos.Add(new ProdutoViewModel()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Categoria = p.Categoria.ToText(),
                    Preco = p.Preco
                });
            };

            return result;
        }
    }
}
