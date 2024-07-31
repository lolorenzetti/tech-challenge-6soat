using Domain.Entities;
using Domain.Enuns;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private DatabaseContext _context;

        public ProdutoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            var newProduto = await _context.Produtos.AddAsync(produto);
            _context.SaveChanges();

            return newProduto.Entity;
        }

        public async Task Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var produto = await _context.Produtos.FirstAsync(p => p.Id == id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> ObterNome(int id)
        {
            var p = await _context.Produtos.FirstOrDefaultAsync(s => s.Id == id);
            return p!.Nome;
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(CategoriaProduto categoria)
        {
            return await _context.Produtos
                .Where(s => s.Categoria == categoria)
                .ToListAsync();
        }

        public async Task<Produto?> ObterPorId(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

    }
}
