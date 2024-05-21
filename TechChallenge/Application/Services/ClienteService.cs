using Application.Models.InputModel;
using Application.Models.ViewModel;
using Domain.Entities;
using Domain.Ports;

namespace Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public ClienteViewModel buscarCliente(string cpf)
        {
            var cliente =  _clienteRepository.buscarPorCpf(cpf);

            return new ClienteViewModel()
            {
                Id = cliente.Id,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome    
            };
        }

        public ClienteViewModel cadastrarCliente(ClienteInputModel clienteInputModel)
        {
            Cliente clienteEntity = new Cliente(clienteInputModel.Nome, clienteInputModel.Email, clienteInputModel.Cpf);
            var cliente = _clienteRepository.cadastrarCliente(clienteEntity);

            return new ClienteViewModel()
            {
                Id = cliente.Id,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome
            };
        }

    }


}