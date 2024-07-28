using Application.Features.PedidoContext.ConfirmPayment;
using Domain.Ports;
using MediatR;

namespace Application.Features.PedidoContext.WebhookPayment
{
    public class WebkookPagamentoHandler : IRequestHandler<WebhookPagamentoRequest>
    {
        private readonly IPagamentoExternoGateway _pagamentoExternoGateway;
        private readonly IPedidoRepository _pedidoRepository;

        public WebkookPagamentoHandler(IPagamentoExternoGateway pagamentoExternoGateway, IPedidoRepository pedidoRepository)
        {
            _pagamentoExternoGateway = pagamentoExternoGateway;
            _pedidoRepository = pedidoRepository;
        }

        public async Task Handle(WebhookPagamentoRequest request, CancellationToken cancellationToken)
        {
            var status = await _pagamentoExternoGateway.ConsultaStatus(request.PagamentoExternoId);
            var pedido = await _pedidoRepository.ObterPorIdPagamento(request.PagamentoExternoId);

            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado");
            }

            if (status == Domain.Enuns.StatusPagamento.APROVADO)
                pedido!.AprovaPagamento();

            _pedidoRepository.Atualiza(pedido);
        }
    }
}
