using Domain.Factory;

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

            Validar<PedidoItem>(this, PeditoItemValidatorFactory.Create());
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
}
