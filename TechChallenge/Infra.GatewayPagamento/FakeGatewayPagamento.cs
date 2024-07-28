using Domain.Entities;
using Domain.Enuns;
using Domain.Ports;

namespace Infra.GatewayPagamento
{
    public class FakeGatewayPagamento : IPagamentoExternoGateway
    {
        public Task<StatusPagamento> ConsultaStatus(string id)
        {
            // NOTA DEV: Consultar pagamento pelo id e retornar o status
            return Task.FromResult(StatusPagamento.APROVADO);
        }

        public Task<string> CriarPagamento(Pedido pedido)
        {
            // NOTA DEV: Chamar rota para criar o pagamento e retonar o id externo;
            return Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}
