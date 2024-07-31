using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        public PedidoRepository(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public DatabaseContext _context { get; set; }

        public void Atualiza(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            _context.Pagamentos.Update(pedido.Pagamento);
            _context.SaveChanges();
        }

        public async Task<int> Cria(Pedido pedido)
        {
            var p = await _context.Pedidos.AddAsync(pedido);
            await _context.PedidoItems.AddRangeAsync(pedido.Itens);
            await _context.Pagamentos.AddAsync(pedido.Pagamento);

            _context.SaveChanges();

            return p.Entity.Id;
        }

        public async Task<List<Pedido>> ListaTodos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido?> ObterPorId(int id)
        {
            return await _context.Pedidos
                .Where(p => p.Id == id)
                .Include(p => p.Itens)
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync();
        }

        public async Task<Pedido?> ObterPorIdPagamento(string id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .Include(p => p.Pagamento)
                .Where(p => p.Pagamento.PagamentoExternoId == id)
                .FirstAsync();
        }

        public async Task<List<Pedido>> ObterTodos()
        {
            return await _context.Pedidos
                .Include(p => p.Pagamento)
                .Include(p => p.Itens).ToListAsync();
        }
    }
}
