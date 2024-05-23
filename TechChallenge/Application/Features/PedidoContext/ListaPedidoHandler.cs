using Application.Models.ViewModel;
using Application.Notifications;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidoContext
{
    public class ListaPedidoHandler : IRequestHandler<ListaPedido, PedidoViewModel>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public ListaPedidoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<PedidoViewModel> Handle(ListaPedido request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.Id);

            List<PedidoItemViewModel> itens = new();

            foreach (var item in pedido.Itens)
            {
                var produto = await _produtoRepository.ObterPorId(item.ProdutoId);

                itens.Add(new()
                {
                    Nome = produto.Nome,
                    Preco = item.Preco,
                    Quantidade = item.Quantidade
                });
            }

            PedidoViewModel result = new()
            {
                Id = pedido.Id,
                ValorTotal = pedido.CalculaValorTotal(),
                Itens = itens
            };

            return result;
        }
    }
}
