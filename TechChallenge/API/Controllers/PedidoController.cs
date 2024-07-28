using Application.Features.PedidoContext.Checkout;
using Application.Features.PedidoContext.ConfirmPayment;
using Application.Features.PedidoContext.Create;
using Application.Features.PedidoContext.GetStatusById;
using Application.Features.PedidoContext.ListAll;
using Application.Features.PedidoContext.UpdateStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PedidoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criar novo pedido
        /// </summary>
        /// <remarks>
        /// Cria um novo pedido na base de dados. Exemplo:
        /// { "clienteId": null, "itens": [{ "id": 1, "quantidade": 1, "observacao": null }] }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Retorna os dados do pedido</returns>
        /// <response code="201"></response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        public async Task<IActionResult> CriaPedido(CreatePedidoRequest command)
        {
            var id = await _mediator.Send(command);
            return Created("api/pedido", id);
        }

        /// <summary>
        /// Listar pedidos
        /// </summary>
        /// <remarks>
        /// Lista todos os pedidos cadastrados
        /// </remarks>
        /// <returns>Listagem dos pedidos</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        public async Task<IActionResult> ListaPedido()
        {
            ListAllPedidosRequest command = new();
            var list = await _mediator.Send(command);
            return Ok(list);
        }

        /// <summary>
        /// Faz o checkout do pedido
        /// </summary>
        /// <remarks>
        /// Faz o checkout do pedido, atualizando status para RECEBIDO
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Pedido</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [Route("{id}/checkout")]
        public async Task<IActionResult> CheckoutPedido([FromRoute] int id)
        {
            var command = new CheckoutPedidoRequest(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterStatusPedido([FromRoute] int id)
        {
            var command = new GetStatusPedidoByIdRequest() { PedidoId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("{id}/next-status")]
        public async Task<IActionResult> AtualizaProximoStatus([FromRoute] int id)
        {
            var command = new UpdateStatusPedidoRequest() { PedidoId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost]
        [Route("/webhook")]
        public async Task<IActionResult> WebhookPagamento(WebhookPagamentoRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
