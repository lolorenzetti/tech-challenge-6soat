using Domain.Ports;
using Domain.Entities;
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

        Cliente IClienteRepository.buscarPorCpf(string cpf)
        {

          return _context.Clientes.First(c => c.Cpf == cpf);
        }

        Cliente IClienteRepository.cadastrarCliente(Cliente cliente)
        {
            var entity = _context.Clientes.Add(cliente).Entity;
            _context.SaveChanges();
            return entity;
        }
    }
}
