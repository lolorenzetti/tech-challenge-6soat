using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {

        private DatabaseContext _context;

        public ClienteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> BuscarNomePorId(int id)
        {
            var c = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            return c!.Nome;
        }

        public async Task<Cliente?> BuscarPorCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente?> BuscarPorId(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> CadastrarCliente(Cliente cliente)
        {
            var entity = await _context.Clientes.AddAsync(cliente);
            _context.SaveChanges();

            return entity.Entity;
        }
    }
}
