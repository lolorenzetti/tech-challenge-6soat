using Domain.Entities;
using Domain.Enuns;

namespace Domain.Ports
{
    public interface IPagamentoExternoGateway
    {
        public Task<string> CriarPagamento(Pedido pedido);
        public Task<StatusPagamento> ConsultaStatus(string id);
    }
}
