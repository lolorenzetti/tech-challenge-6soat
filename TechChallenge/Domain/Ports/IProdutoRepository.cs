using Domain.Entities;
using Domain.Enuns;

namespace Domain.Ports
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterPorCategoria(CategoriaProduto categoria);
        Task<Produto?> ObterPorId(int id);
        Task<string> ObterNome(int id);
        Task<Produto> Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Deletar(int id);
    }
}
