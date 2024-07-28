using MediatR;

namespace Application.Features.ProdutoContext.Create
{
    public record CreateProdutoRequest : IRequest<ProdutoResponse>
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
