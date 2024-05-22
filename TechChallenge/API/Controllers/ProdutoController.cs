using Application.Features.ProdutoContext;
using Application.Models.InputModel;
using Application.Models.ViewModel;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ProdutoService _produtoService;

        public ProdutoController(IMediator mediator, ProdutoService produtoService)
        {
            _mediator = mediator;
            _produtoService = produtoService;
        }

        /// <summary>
        /// Obter todos os produtos
        /// </summary>
        /// <returns>Retorna uma lista com todos os produtos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public ActionResult<ListProdutoViewModel> ObterTodos()
        {
            return Ok(_produtoService.ObterTodos());
        }

        /// <summary>
        /// Obter um produto
        /// </summary>
        /// <remarks>
        /// Obtém o produto do id informado
        /// </remarks>
        /// <param name="id">Id do produto a ser buscado</param>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ListProdutoViewModel> ObterPorId([FromRoute] int id)
        {
            return Ok(_produtoService.ObterPorId(id));
        }

        /// <summary>
        /// Obter todos os produtos de uma categoria
        /// </summary>
        /// <remarks>
        /// Categorias:  [0 "LANCHE", 1 "ACOMPANHAMENTO", 2 "BEBIDA", 3 "SOBREMESA"]
        /// </remarks>
        /// <param name="categoria">Categoria em formato int</param>
        /// <example>/api/produto/categoria/1</example>
        /// <returns>Todos os produtos da categoria informada</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("categoria/{categoria}")]
        public ActionResult<ListProdutoViewModel> ObterPorCategoria([FromRoute] int categoria)
        {
            return Ok(_produtoService.ObterPorCategoria(categoria));
        }

        /// <summary>
        /// Adicionar um produto
        /// </summary>
        /// <remarks>Adiciona um novo produto. Exemplo:
        ///   {
        ///     "id": 1,
        ///     "nome": "X-Tudo",
        ///     "descricao": "Tudo o que tiver na cozinha",
        ///     "categoria": 0,
        ///     "preco": 27.50
        ///    }  
        /// </remarks>
        /// <param name="produto"></param>
        /// <response code="201">Cadastrado com sucesso</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CreateProduto command)
        {
            var id = await _mediator.Send(command);
            return Created($"api/produtos/{id}", id);
        }

        /// <summary>
        /// Deletar um produto 
        /// </summary>
        /// <remarks>Deleta o produto com o id informado na rota</remarks>
        /// <param name="id">id do produto a ser deletado</param>
        /// <response code="204">Deletado com sucesso</response>
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Deletar([FromRoute] int id)
        {
            _produtoService.Deletar(id);
            return NoContent();
        }

        /// <summary>
        /// Atualizar um produto
        /// </summary>
        /// <remarks>Atualiza um produto existente</remarks>
        /// <param name="produto">dados do produto atualizado</param>
        /// <response code="200">Produto atualizado com sucesso</response>
        [HttpPut]
        public ActionResult Atualizar(UpdateProdutoInputModel produto)
        {
            var updated = _produtoService.Atualizar(produto);
            return Ok(updated);
        }
    }
}
