using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enuns
{
    public enum StatusPedido
    {
        PENDENTE_PAGAMENTO,
        CANCELADO,
        RECEBIDO,
        EM_PREPARACAO,
        PRONTO,
        FINALIZADO
    }
}
