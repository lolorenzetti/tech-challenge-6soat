using Application.Models.InputModel;
using Application.Models.ViewModel;
using Domain.Entities;
using Domain.Ports;

namespace Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public ListProdutoViewModel ObterTodos()
        {
            ListProdutoViewModel result = new();

            IEnumerable<Produto> produtos = _produtoRepository.ObterTodos();

            foreach (var p in produtos)
            {
                result.Produtos.Add(new ProdutoViewModel()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Categoria = p.Categoria.ToText(),
                    Preco = p.Preco
                });
            };

            return result;
        }

        public ListProdutoViewModel ObterPorCategoria(int categoria)
        {
            ListProdutoViewModel result = new();

            var produtos = _produtoRepository.ObterPorCategoria((CategoriaProduto)categoria);

            foreach (var p in produtos)
            {
                result.Produtos.Add(new ProdutoViewModel()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Categoria = p.Categoria.ToText(),
                    Preco = p.Preco
                });
            };

            return result;
        }

        public ProdutoViewModel ObterPorId(int id)
        {
            var produto = _produtoRepository.ObterPorId(id);

            return new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Categoria = produto.Categoria.ToText(),
                Preco = produto.Preco
            };
        }

        public ProdutoViewModel Adicionar(ProdutoInputModel produtoInputModel)
        {
            var produto = new Produto(
                produtoInputModel.Nome,
                produtoInputModel.Descricao,
                produtoInputModel.Categoria.ToCategoriaProduto(),
                produtoInputModel.Preco
             );

            var created = _produtoRepository.Adicionar(produto);

            return new ProdutoViewModel()
            {
                Id = created.Id,
                Nome = created.Nome,
                Descricao = created.Descricao,
                Categoria = produto.Categoria.ToText(),
                Preco = produto.Preco
            };
        }

        public ProdutoViewModel Atualizar(UpdateProdutoInputModel produtoInputModel)
        {
            var produto = _produtoRepository.ObterPorId(produtoInputModel.Id);

            produto.Atualiza(
                produtoInputModel.Nome,
                produtoInputModel.Descricao,
                produtoInputModel.Categoria?.ToCategoriaProduto(),
                produtoInputModel.Preco
             );

            _produtoRepository.Atualizar(produto);

            return new ProdutoViewModel()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Categoria = produto.Categoria.ToText(),
                Preco = produto.Preco
            };
        }

        public void Deletar(int id)
        {
            _produtoRepository.Deletar(id);
        }
    }
}
