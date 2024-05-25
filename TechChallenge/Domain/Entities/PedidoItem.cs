using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PedidoItem : Entity
    {
        public PedidoItem(int produtoId, int quantidade, decimal preco, string observacao)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Preco = preco;
            Observacao = observacao;
            Validate(this, new PedidoItemValidator());
        }

        public int PedidoId { get; private set; } // Referência ao agregado root (Pedido)
        public int ProdutoId { get; private set; } // Referência ao agregado Produto
        public int Quantidade { get; private set; }
        public decimal Preco { get; private set; }
        public string? Observacao { get; private set; }

        public void AdicionarQuantidade(int qtd)
        {
            Quantidade += qtd;
        }

        public void RemoverQuantidade(int qtd)
        {
            Quantidade -= qtd;
        }

        public void EditaObservacao(string observacao)
        {
            Observacao = observacao;
        }
    }

    public class PedidoItemValidator : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidator()
        {
            RuleFor(p => p.ProdutoId)
                .NotEmpty()
                .WithMessage("É necessário informar um produto válido");

            RuleFor(p => p.Quantidade)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Quantidade deve ser maior do que zero");

            RuleFor(p => p.Preco)
                .GreaterThan(0)
                .WithMessage("O preço deve ser maior que zero");

            RuleFor(p => p.Observacao)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo para observaçao é de 255 caracteres");
        }
    }
}
