using Application.Features.PedidoContext;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> CriaPedido(CreatePedido command)
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
            ListPedidos command = new();
            var list = await _mediator.Send(command);
            return Ok(list);
        }
    }
}
