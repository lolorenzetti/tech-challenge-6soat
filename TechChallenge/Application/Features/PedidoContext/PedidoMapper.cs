using AutoMapper;
using Domain.Entities;

namespace Application.Features.PedidoContext
{
    public class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            CreateMap<Pedido, PedidoResponse>()
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.CalculaValorTotal()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens))
                .ForMember(dest => dest.ClienteNome, opt => opt.Ignore());

            CreateMap<PedidoItem, PedidoItemResponse>()
                .ForMember(dest => dest.Nome, opt => opt.Ignore())
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco))
                .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao));
        }
    }
}
