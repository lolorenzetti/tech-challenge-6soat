using AutoMapper;
using Domain.Entities;
using Domain.Ports;

namespace Application.Features.PedidoContext
{
    public class PedidoPresenter : IPedidoPresenter
    {
        private IProdutoRepository _produtoRepository;
        private IClienteRepository _clienteRepository;
        private IMapper _mapper;

        public PedidoPresenter(IProdutoRepository produtoRepository, IMapper mapper, IClienteRepository clienteRepository)
        {
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<CheckoutPedidoResponse> ToCheckoutPedidoResponse(Pedido pedido)
        {
            var map = _mapper.Map<CheckoutPedidoResponse>(pedido);
            return await Task.FromResult(map);
        }

        public async Task<ListPedidosResponse> ToListPedidoResponse(List<Pedido> pedidos)
        {
            ListPedidosResponse result = new();

            foreach (var item in pedidos)
            {
                var p = await ToPedidoResponse(item);
                result.Pedidos.Add(p);
            }

            return result;
        }

        public async Task<PedidoResponse> ToPedidoResponse(Pedido pedido)
        {
            var map = _mapper.Map<PedidoResponse>(pedido);

            foreach (var item in map.Itens)
                item.Nome = await _produtoRepository.ObterNome(item.ProdutoId);

            if (pedido.ClienteId != null)
                map.ClienteNome = await _clienteRepository.BuscarNomePorId((int)pedido.ClienteId);

            return map;
        }
    }
}
