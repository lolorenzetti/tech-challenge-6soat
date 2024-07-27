using Domain.Enuns;
using Domain.Factory;

namespace Domain.Entities
{
    public class Pedido : Entity
    {
        public Pedido()
        {
            Status = StatusPedido.PENDENTE_PAGAMENTO;
            Itens = new();
        }

        public int? ClienteId { get; private set; } // Referência ao agregado Cliente
        public StatusPedido Status { get; private set; }
        public List<PedidoItem> Itens { get; private set; }

        public void ReferenciarCliente(int? id)
        {
            ClienteId = id;
        }

        public void AdicionarItens(List<PedidoItem> itens)
        {
            Itens.AddRange(itens);
            Validar();
        }

        public void AdicionarItem(PedidoItem item)
        {
            Itens.Add(item);
            Validar();
        }

        public void RemoverItem(PedidoItem item)
        {
            Itens.Remove(item);
            Validar();
        }

        public decimal CalculaValorTotal()
        {
            decimal result = 0;

            foreach (var item in Itens)
            {
                result += item.Preco * item.Quantidade;
            }

            return result;
        }

        public void Cancelar()
        {
            Status = StatusPedido.CANCELADO;
        }

        public void ReceberPedido()
        {
            Status = StatusPedido.RECEBIDO;
        }

        public void Finalizar()
        {
            Status = StatusPedido.FINALIZADO;
        }

        public override void Validar()
        {
            PedidoValidatorFactory.Create().Validar(this);
        }
    }
}
