using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private DatabaseContext _context;

        public ProdutoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Produto Adicionar(Produto produto)
        {
            var newProduto = _context.Produtos.Add(produto);
            _context.SaveChanges();

            return newProduto.Entity;
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var produto = _context.Produtos.First(p => p.Id == id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Produto> ObterPorCategoria(CategoriaProduto categoria)
        {
            return _context.Produtos
                .Where(s => s.Categoria == categoria)
                .ToList();
        }

        public Produto ObterPorId(int id)
        {
            return _context.Produtos.First(s => s.Id == id);
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return _context.Produtos.ToList();
        }
    }
}
