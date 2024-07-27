using Domain.Enuns;
using Domain.Factory;

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

            Validar();
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

            Validar();
        }

        public override void Validar()
        {
            ProdutoValidatorFactory.Create().Validar(this);
        }
    }
}
