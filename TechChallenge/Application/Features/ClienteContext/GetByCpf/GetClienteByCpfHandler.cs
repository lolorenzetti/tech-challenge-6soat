using Application.Notifications;
using Domain.Ports;
using MediatR;

namespace Application.Features.ClienteContext.GetByCpf
{
    public class GetClienteByCpfHandler : IRequestHandler<GetClienteByCpfRequest, ClienteResponse>
    {
        private NotificationContext _notificationContext;
        private IClienteRepository _clienteRepository;
        private IClientePresenter _presenter;

        public GetClienteByCpfHandler(
            NotificationContext notificationContext,
            IClienteRepository clienteRepository,
            IClientePresenter mapper)
        {
            _notificationContext = notificationContext;
            _clienteRepository = clienteRepository;
            _presenter = mapper;
        }

        public async Task<ClienteResponse> Handle(GetClienteByCpfRequest request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.BuscarPorCpf(request.Cpf);

            if (cliente is null)
                _notificationContext.AddNotification("NullReference", "Cliente não encontrado ou inexistente");

            return await _presenter.ToClienteResponse(cliente!);
        }
    }
}
