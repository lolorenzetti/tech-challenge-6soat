using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Produto
    {
        public Produto(int id, string nome, string descricao, CategoriaProduto categoria, decimal valor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            Valor = valor;
        }

        public int Id { get; }
        public string Nome { get; } = string.Empty;
        public string Descricao { get; } = string.Empty;
        public CategoriaProduto Categoria { get; }
        public decimal Valor { get; }
    }
}
