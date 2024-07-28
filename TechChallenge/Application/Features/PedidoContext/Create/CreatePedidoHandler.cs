using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.Create
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoRequest, CheckoutPedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoPresenter _presenter;
        private readonly IPagamentoExternoGateway _pagamentoExternoGateway;

        public CreatePedidoHandler(
            NotificationContext notificationContext,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IClienteRepository clienteRepository,
            IPedidoPresenter presenter,
            IPagamentoExternoGateway pagamentoExternoGateway)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _presenter = presenter;
            _pagamentoExternoGateway = pagamentoExternoGateway;
        }

        public async Task<CheckoutPedidoResponse> Handle(CreatePedidoRequest request, CancellationToken cancellationToken)
        {
            List<PedidoItem> itens = new List<PedidoItem>();

            foreach (var i in request.Itens)
            {
                var produto = await _produtoRepository.ObterPorId(i.Id);

                if (produto is null)
                {
                    _notificationContext.AddNotification("NullReference",
                        $"Produto com identificador '{i.Id}' não encontrado");
                    return null!;
                }

                var item = new PedidoItem(produto.Id, i.Quantidade, produto.Preco, i.Observacao!);

                if (item.Invalid)
                {
                    _notificationContext.AddNotifications(item.GetErrors());
                    return null!;
                }

                itens.Add(item);
            }

            Pedido pedido = new();
            pedido.AdicionarItens(itens);

            if (request.ClienteId != null)
            {
                var cliente = await _clienteRepository.BuscarPorId((int)request.ClienteId);

                if (cliente is null)
                {
                    _notificationContext.AddNotification("NullReference",
                        $"Cliente com identificador '{request.ClienteId}' não encontrado");

                    return null!;
                }

                pedido.ReferenciarCliente(cliente);
            }


            if (pedido.Invalid)
            {
                _notificationContext.AddNotifications(pedido.GetErrors());
                return null!;
            }

            decimal valorPedido = pedido.CalculaValorTotal();
            string idExterno = await _pagamentoExternoGateway.CriarPagamento(pedido);
            Pagamento pagamento = new(valorPedido, idExterno);
            pedido.ReferenciaPagamento(pagamento);

            await _pedidoRepository.Cria(pedido);

            return await _presenter.ToCheckoutPedidoResponse(pedido);
        }
    }
}
