using MediatR;

namespace Application.Features.PedidoContext.UpdateStatus
{
    public class UpdateStatusPedidoRequest : IRequest<PedidoResponse>
    {
        public int PedidoId { get; set; }
    }
}
