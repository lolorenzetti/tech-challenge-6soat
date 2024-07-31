using Domain.Entities;

namespace Application.Features.PedidoContext
{
    public interface IPedidoPresenter
    {
        public Task<ListPedidosResponse> ToListPedidoResponse(List<Pedido> pedidos);
        public Task<PedidoResponse> ToPedidoResponse(Pedido pedido);
        public Task<CheckoutPedidoResponse> ToCheckoutPedidoResponse(Pedido pedido);
    }
}