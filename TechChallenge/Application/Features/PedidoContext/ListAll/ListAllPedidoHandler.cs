using Application.Notifications;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.ListAll
{
    public class ListAllPedidoHandler : IRequestHandler<ListAllPedidosRequest, ListPedidosResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoPresenter _presenter;

        public ListAllPedidoHandler(
            NotificationContext notificationContext,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IPedidoPresenter presenter)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _presenter = presenter;
        }

        public async Task<ListPedidosResponse> Handle(ListAllPedidosRequest request, CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepository.ObterTodos();
            return await _presenter.ToListPedidoResponse(pedidos);
        }
    }
}
