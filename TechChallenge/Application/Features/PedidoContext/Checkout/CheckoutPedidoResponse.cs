namespace Application.Features.PedidoContext.Checkout
{
    public class CheckOutPedidoResponse
    {
        public int Id { get; set; }
        public List<Item> Itens { get; set; } = new();
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? ClienteId { get; set; }
    }

    public record Item
    {
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; } = string.Empty;
    }
}
