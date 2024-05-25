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
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
