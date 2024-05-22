using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext
{
    public record CreateProduto : IRequest<int>
    {
        public string Nome { get; } = string.Empty;
        public string Descricao { get; } = string.Empty;
        public int Categoria { get; }
        public decimal Preco { get; }
    }
}
