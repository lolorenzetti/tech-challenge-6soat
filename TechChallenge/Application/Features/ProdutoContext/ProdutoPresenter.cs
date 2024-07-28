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
                result.Produtos.Add(await ToProdutoResponse(item));

            return result;
        }

        public Task<ProdutoResponse> ToProdutoResponse(Produto produto)
        {
            return Task.FromResult(_mapper.Map<ProdutoResponse>(produto));
        }
    }
}
