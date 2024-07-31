using MediatR;

namespace Application.Features.PedidoContext.GetStatusById
{
    public record GetStatusPedidoByIdRequest : IRequest<PedidoResponse>
    {
        public int PedidoId { get; set; }
    }
}
