using Application.Models.InputModel;
using Application.Models.ViewModel;
using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClienteContext
{
    public class CreateClienteHandler : IRequestHandler<CreateCliente, ClienteViewModel>
    {
        private NotificationContext _notificationContext;
        private IClienteRepository _clienteRepository;

        public CreateClienteHandler(NotificationContext notificationContext, IClienteRepository clienteRepository)
        {
            _notificationContext = notificationContext;
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteViewModel> Handle(CreateCliente request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(request.Nome, request.Email, request.Cpf);
            var result = new ClienteViewModel();

            if (cliente.Invalid)
            {
                _notificationContext.AddNotifications(cliente.ValidationResult);
            }
            else
            {
                await _clienteRepository.CadastrarCliente(cliente);

                result.Id = cliente.Id;
                result.Cpf = cliente.Cpf;
                result.Nome = cliente.Nome;
                result.Email = cliente.Email;
            }

            return result;
        }
    }
}
