using Application.Models.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidoContext
{
    public class CreatePedido : IRequest<PedidoViewModel>
    {
        public CreatePedido(List<Item> itens)
        {
            Itens = itens;
        }

        public int? ClienteId { get; set; }
        public List<Item> Itens { get; set; }
    }

    public record Item
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string? Observacao { get; set; } = string.Empty;
    }
}
