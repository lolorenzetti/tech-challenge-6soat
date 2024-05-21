using Application.Models.InputModel;
using Application.Models.ViewModel;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("{cpf}")]
        public ActionResult<ClienteViewModel> buscarPorCpf([FromRoute] string cpf)
        {
            return Ok(_clienteService.buscarCliente(cpf));
        }

        [HttpPost]
        public ActionResult<ClienteViewModel> cadastrarCliente(ClienteInputModel clienteInputModel)
        {
            return Created("/clientes", _clienteService.cadastrarCliente(clienteInputModel));
        }

      

    }
}
