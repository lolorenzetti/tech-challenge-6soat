using Application.Notifications;
using Domain.Entities;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidoContext
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedido, int>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public CreatePedidoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<int> Handle(CreatePedido request, CancellationToken cancellationToken)
        {
            List<PedidoItem> itens = new List<PedidoItem>();

            foreach (var i in request.Itens)
            {
                var produto = await _produtoRepository.ObterPorId(i.Id);
                var item = new PedidoItem(produto.Id, i.Quantidade, produto.Preco);

                if (item.Invalid)
                {
                    _notificationContext.AddNotifications(item.ValidationResult);
                    return -1;
                }
                else
                {
                    itens.Add(item);
                }
            }

            Pedido pedido = new();
            pedido.AdicionarItens(itens);

            if (request.ClienteId != null)
            {
                // Buscar o cliente aqui
                var clienteId = 1;
                pedido.ReferenciarCliente(clienteId);
            }

            if (pedido.Invalid)
            {
                _notificationContext.AddNotifications(pedido.ValidationResult);
                return -1;
            }

            await _pedidoRepository.Cria(pedido);

            return pedido.Id;
        }
    }
}
