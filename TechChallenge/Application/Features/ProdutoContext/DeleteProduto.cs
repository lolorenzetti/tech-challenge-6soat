using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext
{
    public class DeleteProduto : IRequest
    {
        public int Id { get; set; }
    }
}
