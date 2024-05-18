using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Pedido
    {
        public Pedido(int id, int? clienteId, List<Produto> produtos)
        {
            Id = id;
            ClienteId = clienteId;
            Status = StatusPedido.RECEBIDO;
            Produtos = produtos;
        }

        public int Id { get; }
        public int? ClienteId { get; }
        public StatusPedido Status { get; }
        public List<Produto> Produtos { get; }
    }
}
