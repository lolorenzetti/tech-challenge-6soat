using Application.Models.ViewModel;
using Application.Notifications;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClienteContext
{
    public class RequestClienteByCpfHandler : IRequestHandler<RequestClienteByCpf, ClienteViewModel>
    {
        private NotificationContext _notificationContext;
        private IClienteRepository _clienteRepository;

        public RequestClienteByCpfHandler(NotificationContext notificationContext, IClienteRepository clienteRepository)
        {
            _notificationContext = notificationContext;
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteViewModel> Handle(RequestClienteByCpf request, CancellationToken cancellationToken)
        {
            ClienteViewModel result = new();

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
