namespace Application.Features.PedidoContext
{
    public class ListPedidosResponse
    {
        public List<PedidoResponse> Pedidos { get; set; } = new List<PedidoResponse>();
    }

    public class PedidoResponse
    {
        public int Id { get; set; }
        public List<PedidoItemResponse> Itens { get; set; } = new();
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ClienteNome { get; set; } = string.Empty;
    }

    public record PedidoItemResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; } = string.Empty;
    }
}
