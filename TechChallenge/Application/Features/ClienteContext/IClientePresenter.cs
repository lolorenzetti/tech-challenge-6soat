using Domain.Entities;

namespace Application.Features.ClienteContext
{
    public interface IClientePresenter
    {
        public Task<ClienteResponse> ToClienteResponse(Cliente cliente);
    }
}
