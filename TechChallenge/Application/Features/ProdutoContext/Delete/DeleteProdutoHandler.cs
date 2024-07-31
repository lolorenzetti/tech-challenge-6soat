using Application.Notifications;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProdutoContext.Delete
{
    public class DeleteProdutoHandler : IRequestHandler<DeleteProdutoRequest>
    {
        private readonly NotificationContext _notificationContext;
        private readonly IProdutoRepository _produtoRepository;

        public DeleteProdutoHandler(NotificationContext notificationContext, IProdutoRepository produtoRepository)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(DeleteProdutoRequest request, CancellationToken cancellationToken)
        {
            await _produtoRepository.Deletar(request.Id);
        }
    }
}
