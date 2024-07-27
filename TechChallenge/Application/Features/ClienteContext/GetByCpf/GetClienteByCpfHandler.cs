using Application.Notifications;
using Domain.Ports;
using MediatR;

namespace Application.Features.ClienteContext.GetByCpf
{
    public class GetClienteByCpfHandler : IRequestHandler<GetClienteByCpfRequest, GetClienteByCpfResponse>
    {
        private NotificationContext _notificationContext;
        private IClienteRepository _clienteRepository;

        public GetClienteByCpfHandler(NotificationContext notificationContext, IClienteRepository clienteRepository)
        {
            _notificationContext = notificationContext;
            _clienteRepository = clienteRepository;
        }

        public async Task<GetClienteByCpfResponse> Handle(GetClienteByCpfRequest request, CancellationToken cancellationToken)
        {
            GetClienteByCpfResponse result = new();

            var cliente = await _clienteRepository.BuscarPorCpf(request.Cpf);

            if (cliente is null)
            {
                _notificationContext.AddNotification("NullReference", "Cliente não encontrado ou inexistente");
            }
            else
            {
                result.Id = cliente.Id;
                result.Cpf = cliente.Cpf;
                result.Email = cliente.Email;
                result.Nome = cliente.Nome;
            }

            return result;
        }
    }
}
