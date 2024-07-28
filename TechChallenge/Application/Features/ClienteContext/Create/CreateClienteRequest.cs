using MediatR;

namespace Application.Features.ClienteContext.Create
{
    public class CreateClienteRequest : IRequest<ClienteResponse>
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
    }
}
