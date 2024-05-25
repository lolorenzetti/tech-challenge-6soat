using Application.Models.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext
{
    public class RequestProdutoById : IRequest<ProdutoViewModel>
    {
        public int Id { get; set; }
    }
}
