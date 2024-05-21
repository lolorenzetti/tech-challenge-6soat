using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IClienteRepository
    {

        Cliente buscarPorCpf(string cpf);

        Cliente cadastrarCliente(Cliente cliente);

    }
}
