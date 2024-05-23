using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PedidoItem : Entity
    {
        public PedidoItem(int produtoId, int quantidade, decimal preco)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Preco = preco;
            Validate(this, new PedidoItemValidator());
        }

        public int PedidoId { get; private set; } // Referência ao agregado root
        public int ProdutoId { get; private set; } // Referência ao agregado Produto
        public int Quantidade { get; private set; }
        public decimal Preco { get; private set; }

        public void voidAdicionaItemNoPedido(int id)
        {
            PedidoId = id;
        }

        public void AdicionarQuantidade(int qtd)
        {
            Quantidade += qtd;
        }

        public void RemoverQuantidade(int qtd)
        {
            Quantidade -= qtd;
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
        }
    }
}
