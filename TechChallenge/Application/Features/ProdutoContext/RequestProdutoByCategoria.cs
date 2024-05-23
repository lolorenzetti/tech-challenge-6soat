using Application.Models.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext
{
    public class RequestProdutoByCategoria : IRequest<ListProdutoViewModel>
    {
        public int CategoriaId { get; set; }
    }
}
