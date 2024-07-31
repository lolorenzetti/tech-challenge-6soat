using AutoMapper;
using Domain;
using Domain.Entities;

namespace Application.Features.PedidoContext
{
    public class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            CreateMap<Pedido, PedidoResponse>()
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.CalculaValorTotal()))
                .ForMember(dest => dest.StatusPedido, opt => opt.MapFrom(src => src.Status.ToText()))
                .ForMember(dest => dest.StatusPagamento, opt => opt.MapFrom(src => src.Pagamento.Status.ToString()))
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens))
                .ForMember(dest => dest.ClienteNome, opt => opt.Ignore());

            CreateMap<Pedido, CheckoutPedidoResponse>()
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.CalculaValorTotal()))
                .ForMember(dest => dest.StatusPedido, opt => opt.MapFrom(src => src.Status.ToText()))
                .ForMember(dest => dest.StatusPagamento, opt => opt.MapFrom(src => src.Pagamento.Status.ToText()))
                .ForMember(dest => dest.PagamentoExternoId, opt => opt.MapFrom(src => src.Pagamento.PagamentoExternoId));

            CreateMap<PedidoItem, PedidoItemResponse>()
                .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId))
                .ForMember(dest => dest.Nome, opt => opt.Ignore())
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Preco))
                .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao));
        }
    }
}
