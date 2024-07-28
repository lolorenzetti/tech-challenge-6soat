using Domain.Entities;

namespace Application.Services
{
    public interface IPagamentoService
    {
        public Task<bool> CriaPagamento(Pedido pedido);
    }
}
