using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PedidoContext
{
    public class CheckoutPedido : IRequest<bool>
    {
        public CheckoutPedido(int pedidoId)
        {
            PedidoId = pedidoId;
        }

        public int PedidoId { get; set; }
    }
}
