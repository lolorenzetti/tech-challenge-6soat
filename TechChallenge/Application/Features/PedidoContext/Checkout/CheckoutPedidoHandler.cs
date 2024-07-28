using Application.Notifications;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.Checkout
{
    public class CheckoutPedidoHandler : IRequestHandler<CheckoutPedidoRequest, PedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private IPedidoPresenter _presenter;

        public CheckoutPedidoHandler(NotificationContext notificationContext, IPedidoRepository pedidoRepository, IPedidoPresenter presenter)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _presenter = presenter;
        }

        public async Task<PedidoResponse> Handle(CheckoutPedidoRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                _notificationContext.AddNotification("NullReference",
                    $"Pedido com identificador {request.PedidoId} não encontrado.");
                throw new Exception("Pedido não encontrado");
            }

            pedido.ReceberPedido();
            _pedidoRepository.Atualiza(pedido);
            return await _presenter.ToPedidoResponse(pedido);
        }
    }
}
