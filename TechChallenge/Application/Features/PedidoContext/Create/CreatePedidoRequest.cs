using MediatR;

namespace Application.Features.PedidoContext.Create
{
    public class CreatePedidoRequest : IRequest<PedidoResponse>
    {
        public CreatePedidoRequest(List<PedidoItemRequest> itens)
        {
            Itens = itens;
        }

        public int? ClienteId { get; set; }
        public List<PedidoItemRequest> Itens { get; set; }
    }

    public record PedidoItemRequest
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string? Observacao { get; set; } = string.Empty;
    }
}
