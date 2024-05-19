using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Produto
    {
        public Produto(int id, string nome, string descricao, CategoriaProduto categoria, decimal preco)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            Preco = preco;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public CategoriaProduto Categoria { get; private set; }
        public decimal Preco { get; private set; }
    }
}
