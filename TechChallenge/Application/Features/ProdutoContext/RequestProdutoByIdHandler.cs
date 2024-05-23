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
            var produto = await _produtoRepository.ObterPorId(request.Id);

            return new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Categoria = produto.Categoria.ToText(),
                Preco = produto.Preco
            };
        }
    }
}
