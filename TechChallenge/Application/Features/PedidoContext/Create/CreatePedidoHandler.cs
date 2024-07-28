using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.Create
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoRequest, PedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoPresenter _presenter;

        public CreatePedidoHandler(
            NotificationContext notificationContext,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IClienteRepository clienteRepository,
            IPedidoPresenter presenter)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _presenter = presenter;
        }

        public async Task<PedidoResponse> Handle(CreatePedidoRequest request, CancellationToken cancellationToken)
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

                pedido.ReferenciarCliente(cliente.Id);
            }

            if (pedido.Invalid)
            {
                _notificationContext.AddNotifications(pedido.GetErrors());
                return null!;
            }

            await _pedidoRepository.Cria(pedido);

            return await _presenter.ToPedidoResponse(pedido);
        }
    }
}
