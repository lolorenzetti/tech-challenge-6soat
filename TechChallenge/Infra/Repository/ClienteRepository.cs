using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {

        private DatabaseContext _context;

        public ClienteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> BuscarPorCpf(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente> CadastrarCliente(Cliente cliente)
        {
            var entity = await _context.Clientes.AddAsync(cliente);
            _context.SaveChanges();

            return entity.Entity;
        }
    }
}
