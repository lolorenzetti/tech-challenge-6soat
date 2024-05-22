using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext
{
    public class CreateProdutoHandler : IRequestHandler<CreateProduto, int>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public CreateProdutoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }

        public async Task<int> Handle(CreateProduto request, CancellationToken cancellationToken)
        {
            var produto = new Produto(
                request.Nome,
                request.Descricao,
                request.Categoria.ToCategoriaProduto(),
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
