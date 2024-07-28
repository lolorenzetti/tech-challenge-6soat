using MediatR;

namespace Application.Features.ProdutoContext.Update
{
    public class UpdateProdutoRequest : IRequest<ProdutoResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
