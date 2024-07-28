using AutoMapper;
using Domain.Entities;

namespace Application.Features.ClienteContext
{
    public class ClientePresenter : IClientePresenter
    {
        private IMapper _mapper;

        public ClientePresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ClienteResponse> ToClienteResponse(Cliente cliente)
        {
            return await Task.FromResult(_mapper.Map<ClienteResponse>(cliente));
        }
    }
}
