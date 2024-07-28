namespace Application.Features.PedidoContext
{
    public class ListPedidosResponse
    {
        public List<PedidoResponse> Pedidos { get; set; } = new List<PedidoResponse>();
    }

    public record CheckoutPedidoResponse
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public string StatusPedido { get; set; } = string.Empty;
        public string StatusPagamento { get; set; } = string.Empty;
        public string PagamentoExternoId { get; set; } = string.Empty;
    }


    public class PedidoResponse
    {
        public int Id { get; set; }
        public string? ClienteNome { get; set; } = string.Empty;
        public string StatusPedido { get; set; } = string.Empty;
        public string StatusPagamento { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public List<PedidoItemResponse> Itens { get; set; } = new();
    }


    public record PedidoItemResponse
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; } = string.Empty;
    }
}
