using Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.Validators.FluentValidator
{
    public class ClienteValidator : IValidador<Cliente>
    {
        public void Validar(Cliente entity)
        {
            var validationResult = new ClienteFluentValidator().Validate(entity);

            foreach (var error in validationResult.Errors)
            {
                entity.AddError(error.ErrorCode, error.ErrorMessage);
            }
        }
    }

    internal class ClienteFluentValidator : AbstractValidator<Cliente>
    {
        public ClienteFluentValidator()
        {
            RuleFor(e => e.Cpf)
                .Matches(new Regex("^[0-9]{3}[0-9]{3}[0-9]{3}[0-9]{2}"))
                .WithMessage("CPF inválido");

            RuleFor(e => e.Nome)
                .MinimumLength(5)
                .WithMessage("Nome deve ter no mínimo 5 caracteres");

            RuleFor(e => e.Email)
                .EmailAddress()
                .WithMessage("Email inválido");
        }
    }
}
