using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Produto : Entity
    {
        public Produto(string nome, string descricao, CategoriaProduto categoria, decimal preco)
        {
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            Preco = preco;

            Validate(this, new ProdutoValidator());
        }

        public string Nome { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public CategoriaProduto Categoria { get; private set; }
        public decimal Preco { get; private set; }

        public void Atualiza(string? nome, string? descricao, CategoriaProduto? categoria, decimal? preco)
        {
            this.Nome = string.IsNullOrEmpty(nome) ? this.Nome : nome;
            this.Descricao = string.IsNullOrEmpty(descricao) ? this.Descricao : descricao;

            if (categoria != null && categoria != this.Categoria)
            {
                this.Categoria = (CategoriaProduto)categoria;
            }

            if (preco != null && preco != this.Preco)
            {
                this.Preco = (decimal)preco;
            }

            Validate(this, new ProdutoValidator());
        }
    }

    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
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
