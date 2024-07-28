using MediatR;

namespace Application.Features.PedidoContext.ConfirmPayment
{
    public class WebhookPagamentoRequest : IRequest
    {
        public string PagamentoExternoId { get; set; } = string.Empty;
    }
}
