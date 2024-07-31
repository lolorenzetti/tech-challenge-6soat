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
            Pagamento = new(0);
        }

        public int? ClienteId { get; private set; } // Referência ao agregado Cliente
        public Pagamento Pagamento { get; private set; }
        public StatusPedido Status { get; private set; }
        public List<PedidoItem> Itens { get; private set; }

        public void ReferenciarCliente(Cliente cliente)
        {
            ClienteId = cliente.Id;
        }

        public void ReferenciaPagamento(Pagamento pagamento)
        {
            Pagamento = pagamento;
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

        public void AprovaPagamento()
        {
            this.Pagamento.AprovaPagamento();
            Status = StatusPedido.RECEBIDO;
        }

        public void Finalizar()
        {
            Status = StatusPedido.FINALIZADO;
        }

        public void AtualizaProximoStatus()
        {
            switch (Status)
            {
                case StatusPedido.RECEBIDO:
                    Status = StatusPedido.EM_PREPARACAO;
                    break;
                case StatusPedido.EM_PREPARACAO:
                    Status = StatusPedido.PRONTO;
                    break;
                case StatusPedido.PRONTO:
                    Status = StatusPedido.FINALIZADO;
                    break;
                default:
                    // Demais casos são alterados somente mediante alguma ação.
                    break;
            }
        }

        public void Validar()
        {
            base.Validar<Pedido>(this, PedidoValidatorFactory.Create());
        }
    }
}
