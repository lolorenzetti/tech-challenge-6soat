using Domain.Entities;
using Domain.Enuns;

namespace Domain.Ports
{
    public interface IPagamentoRepository
    {
        public Task<Pagamento> CriaPagamento(Pagamento pagamento);
        public Task<StatusPagamento> ConsultaStatus(int id);

    }
}
