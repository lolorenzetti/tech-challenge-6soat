using Application.Features.ClienteContext;
using Application.Models.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<IActionResult> BuscarPorCpf([FromRoute] string cpf)
        {
            RequestClienteByCpf command = new() { Cpf = cpf };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCliente(CreateCliente command)
        {
            var result = await _mediator.Send(command);
            return Created("/clientes", result);
        }
    }
}
