using Application.Models.ViewModel;
using Application.Notifications;
using Domain;
using Domain.Entities;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedido, PedidoViewModel>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;

        public CreatePedidoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, IClienteRepository clienteRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<PedidoViewModel> Handle(CreatePedido request, CancellationToken cancellationToken)
        {
            PedidoViewModel result = new();
            List<PedidoItem> itens = new List<PedidoItem>();

            foreach (var i in request.Itens)
            {
                var produto = await _produtoRepository.ObterPorId(i.Id);

                if (produto is null)
                {
                    _notificationContext.AddNotification("NullReference",
                        $"Produto com identificador '{i.Id}' não encontrado");
                    return result;
                }

                var item = new PedidoItem(produto.Id, i.Quantidade, produto.Preco, i.Observacao!);

                if (item.Invalid)
                {
                    _notificationContext.AddNotifications(item.ValidationResult);
                    return result;
                }

                itens.Add(item);

                result.Itens.Add(new()
                {
                    Nome = produto.Nome,
                    Preco = item.Preco,
                    Quantidade = item.Quantidade,
                    Observacao = item.Observacao!
                });
            }

            Pedido pedido = new();
            pedido.AdicionarItens(itens);
            result.ValorTotal = pedido.CalculaValorTotal();
            result.Status = pedido.Status.ToText();

            if (request.ClienteId != null)
            {
                var cliente = await _clienteRepository.BuscarPorId((int)request.ClienteId);

                if (cliente is null)
                {
                    _notificationContext.AddNotification("NullReference",
                        $"Cliente com identificador '{request.ClienteId}' não encontrado");

                    return result;
                }

                pedido.ReferenciarCliente(cliente.Id);
                result.ClienteId = pedido.ClienteId;
            }

            if (pedido.Invalid)
            {
                _notificationContext.AddNotifications(pedido.ValidationResult);
                return result;
            }

            await _pedidoRepository.Cria(pedido);
            result.Id = pedido.Id;


            return result;
        }
    }
}
