using MediatR;

namespace Application.Features.PedidoContext.Checkout
{
    public class CheckoutPedidoRequest : IRequest<PedidoResponse>
    {
        public CheckoutPedidoRequest(int pedidoId)
        {
            PedidoId = pedidoId;
        }

        public int PedidoId { get; set; }
    }
}
