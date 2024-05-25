using Application.Models.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ClienteContext
{
    public record RequestClienteByCpf : IRequest<ClienteViewModel>
    {
        public string Cpf { get; set; } = string.Empty;
    }
}
