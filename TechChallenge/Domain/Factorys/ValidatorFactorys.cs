using Domain.Entities;
using Domain.Validators;
using Domain.Validators.FluentValidator;

namespace Domain.Factory
{
    public static class ClienteValidatorFactory
    {
        public static IValidador<Cliente> Create()
        {
            return new ClienteValidator();
        }
    }

    public static class ProdutoValidatorFactory
    {
        public static IValidador<Produto> Create()
        {
            return new ProdutoValidator();
        }
    }

    public static class PeditoItemValidatorFactory
    {
        public static IValidador<PedidoItem> Create()
        {
            return new PedidoItemValidator();
        }
    }

    public static class PedidoValidatorFactory
    {
        public static IValidador<Pedido> Create()
        {
            return new PedidoValidator();
        }
    }
}
