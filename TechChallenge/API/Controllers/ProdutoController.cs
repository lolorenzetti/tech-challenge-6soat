using Application.Features.ProdutoContext.Create;
using Application.Features.ProdutoContext.Delete;
using Application.Features.ProdutoContext.GetByCategoria;
using Application.Features.ProdutoContext.GetById;
using Application.Features.ProdutoContext.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obter um produto
        /// </summary>
        /// <remarks>
        /// Obtém o produto do id informado
        /// </remarks>
        /// <param name="id">Id do produto a ser buscado</param>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Não encontrado ou inexistente</response>
        /// <response code="500">Erro no servidor</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] int id)
        {
            GetProdutoByIdRequest command = new() { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Obter todos os produtos de uma categoria
        /// </summary>
        /// <remarks>
        /// Categorias:  [0 "LANCHE", 1 "ACOMPANHAMENTO", 2 "BEBIDA", 3 "SOBREMESA"]
        /// </remarks>
        /// <param name="id">Categoria em formato int</param>
        /// <example>/api/produto/categoria/1</example>
        /// <returns>Todos os produtos da categoria informada</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Erro no servidor</response>
        [HttpGet]
        [Route("categoria/{id}")]
        public async Task<IActionResult> ObterPorCategoria([FromRoute] int id)
        {
            GetProdutoByCategoriaRequest command = new() { CategoriaId = id };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Adicionar um produto
        /// </summary>
        /// <remarks>Adiciona um novo produto. Exemplo:
        ///   {
        ///     "nome": "X-Tudo",
        ///     "descricao": "Tudo o que tiver na cozinha",
        ///     "categoria": 0,
        ///     "preco": 27.50
        ///    }  
        /// </remarks>
        /// <param name="command"></param>
        /// <response code="201">Cadastrado com sucesso</response>
        /// <response code="400">Erros de validação</response>
        /// <response code="500">Erro no servidor</response>
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CreateProdutoRequest command)
        {
            var id = await _mediator.Send(command);
            return Created($"api/produtos/{id}", id);
        }

        /// <summary>
        /// Deletar um produto 
        /// </summary>
        /// <remarks>Deleta o produto com o id informado na rota</remarks>
        /// <param name="id">id do produto a ser deletado</param>
        /// <response code="204">Sucesso</response>
        /// <response code="500">Erro no servidor</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Deletar([FromRoute] int id)
        {
            DeleteProdutoRequest command = new() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Atualizar um produto
        /// </summary>
        /// <remarks>Atualiza um produto existente</remarks>
        /// <param name="command">dados do produto atualizado</param>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="500">Erro no servidor</response>
        [HttpPut]
        public async Task<IActionResult> Atualizar(UpdateProdutoRequest command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
