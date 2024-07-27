using Domain.Entities;
using FluentValidation;

namespace Domain.Validators.FluentValidator
{
    public class ProdutoValidator : IValidador<Produto>
    {
        public void Validar(Produto entity)
        {
            var validationResult = new ProdutoFluentValidator().Validate(entity);

            foreach (var error in validationResult.Errors)
            {
                entity.AddError(error.ErrorCode, error.ErrorMessage);
            }
        }
    }

    public class ProdutoFluentValidator : AbstractValidator<Produto>
    {
        public ProdutoFluentValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Nome do produto deve ter tamanho mínimo de 3 caracteres");

            RuleFor(p => p.Nome)
                .MaximumLength(100)
                .WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(p => p.Preco)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que zero");

            RuleFor(p => p.Descricao)
                .MaximumLength(255)
                .WithMessage("Descrição deve ter no máximo 255 caracteres");

            RuleFor(p => p.Categoria)
                .IsInEnum()
                .WithMessage("Categoria inválida ou inexistente");
        }
    }
}
