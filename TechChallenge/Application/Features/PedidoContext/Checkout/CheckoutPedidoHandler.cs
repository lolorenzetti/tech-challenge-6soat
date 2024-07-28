using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.Checkout
{
    public class CheckoutPedidoHandler : IRequestHandler<CheckoutPedidoRequest, CheckoutPedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private IPedidoPresenter _presenter;
        private readonly IPagamentoExternoGateway _pagamentoGateway;

        public CheckoutPedidoHandler(
            NotificationContext notificationContext,
            IPedidoRepository pedidoRepository,
            IPedidoPresenter presenter,
            IPagamentoExternoGateway pagamentoGateway)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _presenter = presenter;
            _pagamentoGateway = pagamentoGateway;
        }

        public async Task<CheckoutPedidoResponse> Handle(CheckoutPedidoRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                _notificationContext.AddNotification("NullReference",
                    $"Pedido com identificador {request.PedidoId} não encontrado.");
                throw new Exception("Pedido não encontrado");
            }

            decimal valorPedido = pedido.CalculaValorTotal();
            string idExterno = await _pagamentoGateway.CriarPagamento(pedido);

            Pagamento pagamento = new(valorPedido, idExterno);
            pedido.ReferenciaPagamento(pagamento);
            _pedidoRepository.Atualiza(pedido);

            return await _presenter.ToCheckoutPedidoResponse(pedido);
        }
    }
}
