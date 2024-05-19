using Application.Models.InputModel;
using Application.Models.ViewModel;
using Domain.Entities;
using Domain.Ports;

namespace Application.Services
{
    internal class ProdutoService
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
                    Categoria = nameof(p.Categoria),
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
                Categoria = nameof(produto.Categoria),
                Preco = produto.Preco
            };
        }

        public void Adicionar(ProdutoInputModel produtoViewModel)
        {
            var produto = new Produto(
                produtoViewModel.Id, 
                produtoViewModel.Nome, 
                produtoViewModel.Descricao, 
                (CategoriaProduto) produtoViewModel.Categoria,
                produtoViewModel.Preco
             );

            _produtoRepository.Adicionar(produto);
        }

        public void Atualizar(ProdutoInputModel produtoViewModel)
        {
            var produto = new Produto(
                produtoViewModel.Id,
                produtoViewModel.Nome,
                produtoViewModel.Descricao,
                (CategoriaProduto)produtoViewModel.Categoria,
                produtoViewModel.Preco
             );

            _produtoRepository.Atualizar(produto);
        }

        public void Deletar(int id)
        {
            _produtoRepository.Deletar(id);
        }
    }
}
