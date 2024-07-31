using Application.Features.ClienteContext;
using Application.Features.ClienteContext.Create;
using Application.Features.ClienteContext.GetByCpf;
using MediatR;
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

        /// <summary>
        /// Buscar cliente por CPF
        /// </summary>
        /// <remarks>
        /// Busca um cliente na base de dados a partir do CPF informado.
        /// Exemplo: 12345678910
        /// </remarks>
        /// <param name="cpf"></param>
        /// <returns>Dados do cliente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("{cpf}")]
        public async Task<IActionResult> BuscarPorCpf([FromRoute] string cpf)
        {
            GetClienteByCpfRequest command = new() { Cpf = cpf };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Cadastrar novo cliente
        /// </summary>
        /// <remarks>
        /// Cadastra um novo cliente. Exemplo:
        /// { "nome": "João Ferreira da Silva", "email": "joao.silva@provedor.com", "cpf": "12345678910" }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Retorna os dados do cliente cadastrado</returns>
        /// <response code="201">Criado com sucesso</response>
        /// <response code="400">Erros de validação</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        public async Task<ActionResult<ClienteResponse>> CadastrarCliente(CreateClienteRequest command)
        {
            var result = await _mediator.Send(command);
            return Created("/clientes", result);
        }
    }
}
