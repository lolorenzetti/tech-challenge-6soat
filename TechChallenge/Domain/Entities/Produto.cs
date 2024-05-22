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
            this.Nome = nome ?? this.Nome;
            this.Descricao = descricao ?? this.Descricao;

            if (categoria != null && categoria != this.Categoria)
            {
                this.Categoria = (CategoriaProduto)categoria;
            }

            if (preco != null && preco != this.Preco)
            {
                this.Preco = (decimal)preco;
            }
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
        }
    }
}
