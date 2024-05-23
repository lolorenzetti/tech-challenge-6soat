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

        [HttpPost]
        public async Task<IActionResult> CriaPedido(CreatePedido command)
        {
            var id = await _mediator.Send(command);
            return Created("api/pedido", id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ListaPedido([FromRoute] int id)
        {
            ListaPedido command = new() { Id = id };
            var list = await _mediator.Send(command);
            return Ok(list);
        }
    }
}
