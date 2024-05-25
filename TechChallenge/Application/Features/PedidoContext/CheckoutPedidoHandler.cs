using Application.Models.ViewModel;
using Application.Notifications;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidoContext
{
    public class CheckoutPedidoHandler : IRequestHandler<CheckoutPedido, PedidoViewModel>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CheckoutPedidoHandler(NotificationContext notificationContext, IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<PedidoViewModel> Handle(CheckoutPedido request, CancellationToken cancellationToken)
        {
            PedidoViewModel result = new();

            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                _notificationContext.AddNotification("NullReference", 
                    $"Pedido com identificador {request.PedidoId} não encontrado.");

                return result;
            }

            pedido.ReceberPedido();

            _pedidoRepository.Atualiza(pedido);

            result.Id = pedido.Id;
            result.Status = pedido.Status.ToText();
            result.ValorTotal = pedido.CalculaValorTotal();

            foreach (var i in pedido.Itens)
            {
                var produto = await _produtoRepository.ObterPorId(i.ProdutoId);

                result.Itens.Add(new()
                {
                    Nome = produto!.Nome,
                    Preco = i.Preco,
                    Quantidade = i.Quantidade,
                    Observacao = i.Observacao!
                });
            }

            return result;
        }
    }
}
