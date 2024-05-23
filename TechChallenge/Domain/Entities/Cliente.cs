using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente : Entity
    {
        public Cliente(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;

            Validate(this, new ClienteValidator());
        }

        public string Nome { get; }
        public string Email { get; }
        public string Cpf { get; }
    }

    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(e => e.Cpf)
                .Matches(new Regex("^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}"))
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
