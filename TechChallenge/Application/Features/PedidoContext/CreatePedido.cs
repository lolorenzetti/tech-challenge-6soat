using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidoContext
{
    public class CreatePedido : IRequest<int>
    {
        public CreatePedido(List<Itens> itens)
        {
            Itens = itens;
        }

        public int? ClienteId { get; set; }
        public List<Itens> Itens { get; set; }
    }

    public record Itens
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
