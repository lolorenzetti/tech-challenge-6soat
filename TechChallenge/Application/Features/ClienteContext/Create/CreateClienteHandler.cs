using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Features.ClienteContext.Create
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteRequest, ClienteResponse>
    {
        private NotificationContext _notificationContext;
        private IClienteRepository _clienteRepository;
        private IClientePresenter _presenter;

        public CreateClienteHandler(
            NotificationContext notificationContext,
            IClienteRepository clienteRepository,
            IClientePresenter presenter)
        {
            _notificationContext = notificationContext;
            _clienteRepository = clienteRepository;
            _presenter = presenter;
        }

        public async Task<ClienteResponse> Handle(CreateClienteRequest request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(request.Nome, request.Email, request.Cpf);

            if (cliente.Invalid)
                _notificationContext.AddNotifications(cliente.GetErrors());

            await _clienteRepository.CadastrarCliente(cliente);
            return await _presenter.ToClienteResponse(cliente);
        }
    }
}
