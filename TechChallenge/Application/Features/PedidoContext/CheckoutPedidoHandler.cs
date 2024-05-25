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
    public class CheckoutPedidoHandler : IRequestHandler<CheckoutPedido, bool>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;

        public CheckoutPedidoHandler(NotificationContext notificationContext, IPedidoRepository pedidoRepository)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(CheckoutPedido request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido.Invalid)
            {
                _notificationContext.AddNotifications(pedido.ValidationResult);
                return false;
            }

            pedido.ReceberPedido();
            return true;
        }
    }
}
