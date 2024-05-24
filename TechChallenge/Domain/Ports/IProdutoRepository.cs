using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Ports
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterPorCategoria(CategoriaProduto categoria);
        Task<Produto?> ObterPorId(int id);
        Task<int> Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Deletar(int id);
    }
}
