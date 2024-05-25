using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IPedidoRepository
    {
        public Task<Pedido?> ObterPorId(int id);
        public Task<List<Pedido>> ObterTodos();
        public Task<int> Cria(Pedido pedido);
        public void Atualiza(Pedido pedido);
        public Task<List<Pedido>> ListaTodos();
    }
}
