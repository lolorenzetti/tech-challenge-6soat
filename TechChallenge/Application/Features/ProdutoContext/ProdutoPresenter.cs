using AutoMapper;
using Domain.Entities;

namespace Application.Features.ProdutoContext
{
    public class ProdutoPresenter : IProdutoPresenter
    {
        private IMapper _mapper;

        public ProdutoPresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ListProdutoResponse> ToListProdutoResponse(List<Produto> produtos)
        {
            ListProdutoResponse result = new();

            foreach (var item in produtos)
            {
                var p = await ToProdutoResponse(item);
                result.Produtos.Add(p);
            }

            return result;
        }

        public async Task<ProdutoResponse> ToProdutoResponse(Produto produto)
        {
            return await Task.FromResult(_mapper.Map<ProdutoResponse>(produto));
        }
    }
}
