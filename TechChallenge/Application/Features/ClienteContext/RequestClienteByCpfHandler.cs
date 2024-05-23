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
            var cliente = await _clienteRepository.BuscarPorCpf(request.Cpf);

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
