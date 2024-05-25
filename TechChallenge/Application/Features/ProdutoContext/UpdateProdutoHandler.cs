using Application.Models.ViewModel;
using Application.Notifications;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext
{
    internal class UpdateProdutoHandler : IRequestHandler<UpdateProduto>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public UpdateProdutoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(UpdateProduto request, CancellationToken cancellationToken)
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
