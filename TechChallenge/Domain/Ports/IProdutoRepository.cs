using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Ports
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> ObterTodos();
        IEnumerable<Produto> ObterPorCategoria(CategoriaProduto categoria);
        Produto ObterPorId(int id);
        Produto Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Deletar(int id);
    }
}
