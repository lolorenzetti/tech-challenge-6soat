using AutoMapper;
using Domain.Entities;

namespace Application.Features.ClienteContext
{
    public class ClienteMapper : Profile
    {
        public ClienteMapper()
        {
            CreateMap<Cliente, ClienteResponse>();
        }
    }
}
