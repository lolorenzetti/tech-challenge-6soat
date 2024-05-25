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
    public class ListPedidoHandler : IRequestHandler<ListPedidos, List<PedidoViewModel>>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public ListPedidoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<List<PedidoViewModel>> Handle(ListPedidos request, CancellationToken cancellationToken)
        {
            List<PedidoViewModel> result = new();

            var pedidos = await _pedidoRepository.ObterTodos();

            foreach (var pedido in pedidos)
            {
                PedidoViewModel pvm = new();
                pvm.Id = pedido.Id;
                pvm.ValorTotal = pedido.CalculaValorTotal();
                pvm.Status = pedido.Status.ToText();

                foreach (var pedidoItem in pedido.Itens)
                {
                    var produto = await _produtoRepository.ObterPorId(pedidoItem.ProdutoId);

                    PedidoItemViewModel pivm = new();
                    pivm.Nome = produto!.Nome;
                    pivm.Preco = pedidoItem.Preco;
                    pivm.Quantidade = pedidoItem.Quantidade;
                    pivm.Observacao = pedidoItem.Observacao!;

                    pvm.Itens.Add(pivm);
                }

                result.Add(pvm);
            }

            return result;
        }
    }
}
