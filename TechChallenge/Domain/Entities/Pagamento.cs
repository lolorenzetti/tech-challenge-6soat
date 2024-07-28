using Domain.Enuns;

namespace Domain.Entities
{
    public class Pagamento : Entity
    {
        public int PedidoId { get; set; }
        public string? PagamentoExternoId { get; private set; } // Referêcia do pagamento externo
        public decimal Valor { get; private set; }
        public StatusPagamento Status { get; private set; }

        public Pagamento(decimal valor)
        {
            this.Status = StatusPagamento.PENDENTE;
            this.Valor = valor;
        }

        public Pagamento(decimal valor, string pagamentoExternoId)
        {
            this.Status = StatusPagamento.PENDENTE;
            this.Valor = valor;
            this.PagamentoExternoId = pagamentoExternoId;
        }

        public void ReferenciaPagamentoExterno(string idExterno)
        {
            this.PagamentoExternoId = idExterno;
        }

        public bool PagamentoAprovado()
        {
            return Status == StatusPagamento.APROVADO;
        }

        public void AprovaPagamento()
        {
            this.Status = StatusPagamento.APROVADO;
        }

        public void ReprovaPagamento()
        {
            this.Status = StatusPagamento.REPROVADO;
        }
    }
}
