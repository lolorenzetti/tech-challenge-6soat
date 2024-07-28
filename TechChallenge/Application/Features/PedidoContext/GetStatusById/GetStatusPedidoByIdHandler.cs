using Application.Notifications;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.GetStatusById
{
    public class GetStatusPedidoByIdHandler : IRequestHandler<GetStatusPedidoByIdRequest, PedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoPresenter _presenter;

        public GetStatusPedidoByIdHandler(
            IPedidoRepository pedidoRepository,
            IPedidoPresenter presenter,
            NotificationContext notificationContext)
        {
            _pedidoRepository = pedidoRepository;
            _presenter = presenter;
            _notificationContext = notificationContext;
        }

        public async Task<PedidoResponse> Handle(GetStatusPedidoByIdRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                _notificationContext.AddNotification("NullReference", "Pedido não encontrado");
                return null!;
            }

            return await _presenter.ToPedidoResponse(pedido);
        }
    }
}
