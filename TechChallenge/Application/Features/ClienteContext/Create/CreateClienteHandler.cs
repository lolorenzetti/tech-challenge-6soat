using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Features.ClienteContext.Create
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteRequest, CreateClienteResponse>
    {
        private NotificationContext _notificationContext;
        private IClienteRepository _clienteRepository;

        public CreateClienteHandler(NotificationContext notificationContext, IClienteRepository clienteRepository)
        {
            _notificationContext = notificationContext;
            _clienteRepository = clienteRepository;
        }

        public async Task<CreateClienteResponse> Handle(CreateClienteRequest request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(request.Nome, request.Email, request.Cpf);
            var result = new CreateClienteResponse();

            if (cliente.Invalid)
            {
                _notificationContext.AddNotifications(cliente.GetErrors());
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
