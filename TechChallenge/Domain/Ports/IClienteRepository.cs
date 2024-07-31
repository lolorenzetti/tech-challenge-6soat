using Domain.Entities;

namespace Domain.Ports
{
    public interface IClienteRepository
    {
        Task<Cliente> CadastrarCliente(Cliente cliente);
        Task<Cliente?> BuscarPorCpf(string cpf);
        Task<Cliente?> BuscarPorId(int id);
        Task<string> BuscarNomePorId(int id);
    }
}
