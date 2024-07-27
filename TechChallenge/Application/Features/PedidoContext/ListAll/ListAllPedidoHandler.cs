using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.ListAll
{
    public class ListAllPedidoHandler : IRequestHandler<ListAllPedidosRequest, ListAllPedidosResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public ListAllPedidoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<ListAllPedidosResponse> Handle(ListAllPedidosRequest request, CancellationToken cancellationToken)
        {
            ListAllPedidosResponse result = new();

            var pedidos = await _pedidoRepository.ObterTodos();

            foreach (var pedido in pedidos)
            {
                PedidoResponse pvm = new();
                pvm.Id = pedido.Id;
                pvm.ValorTotal = pedido.CalculaValorTotal();
                pvm.Status = pedido.Status.ToText();
                pvm.ClienteId = pedido.ClienteId;

                foreach (var pedidoItem in pedido.Itens)
                {
                    var produto = await _produtoRepository.ObterPorId(pedidoItem.ProdutoId);

                    PedidoItemResponse pivm = new();
                    pivm.Nome = produto!.Nome;
                    pivm.Preco = pedidoItem.Preco;
                    pivm.Quantidade = pedidoItem.Quantidade;
                    pivm.Observacao = pedidoItem.Observacao!;

                    pvm.Itens.Add(pivm);
                }

                result.Pedidos.Add(pvm);
            }

            return result;
        }
    }
}
