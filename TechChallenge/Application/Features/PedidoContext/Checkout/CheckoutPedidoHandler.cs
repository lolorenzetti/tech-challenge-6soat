using Application.Notifications;
using Domain;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.Checkout
{
    public class CheckoutPedidoHandler : IRequestHandler<CheckoutPedidoRequest, CheckOutPedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CheckoutPedidoHandler(NotificationContext notificationContext, IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<CheckOutPedidoResponse> Handle(CheckoutPedidoRequest request, CancellationToken cancellationToken)
        {
            CheckOutPedidoResponse result = new();

            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                _notificationContext.AddNotification("NullReference",
                    $"Pedido com identificador {request.PedidoId} não encontrado.");

                return result;
            }

            pedido.ReceberPedido();

            _pedidoRepository.Atualiza(pedido);

            result.Id = pedido.Id;
            result.Status = pedido.Status.ToText();
            result.ValorTotal = pedido.CalculaValorTotal();
            result.ClienteId = pedido.ClienteId;

            foreach (var i in pedido.Itens)
            {
                var produto = await _produtoRepository.ObterPorId(i.ProdutoId);

                result.Itens.Add(new()
                {
                    Nome = produto!.Nome,
                    Preco = i.Preco,
                    Quantidade = i.Quantidade,
                    Observacao = i.Observacao!
                });
            }

            return result;
        }
    }
}
