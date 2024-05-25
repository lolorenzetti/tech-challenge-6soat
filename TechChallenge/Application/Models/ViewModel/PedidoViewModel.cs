using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModel
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public List<PedidoItemViewModel> Itens { get; set; } = new();
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public record PedidoItemViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; } = string.Empty;
    }
}
