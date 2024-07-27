using Domain.Entities;
using FluentValidation;

namespace Domain.Validators.FluentValidator
{
    public class PedidoValidator : IValidador<Pedido>
    {
        public void Validar(Pedido entity)
        {
            var validationResult = new PedidoFluentValidator().Validate(entity);

            foreach (var error in validationResult.Errors)
            {
                entity.AddError(error.ErrorCode, error.ErrorMessage);
            }
        }
    }
    public class PedidoFluentValidator : AbstractValidator<Pedido>
    {
        public PedidoFluentValidator()
        {
            RuleFor(p => p.Itens)
                .NotEmpty()
                .WithMessage("Não é possível criar um pedido sem itens");
        }
    }
}
