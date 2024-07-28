using AutoMapper;
using Domain.Entities;

namespace Application.Features.ProdutoContext
{
    public class ProdutoMapper : Profile
    {
        public ProdutoMapper()
        {
            CreateMap<Produto, ProdutoResponse>();
        }
    }
}
