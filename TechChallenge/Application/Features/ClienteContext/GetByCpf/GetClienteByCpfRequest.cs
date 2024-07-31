using MediatR;

namespace Application.Features.ClienteContext.GetByCpf
{
    public record GetClienteByCpfRequest : IRequest<ClienteResponse>
    {
        public string Cpf { get; set; } = string.Empty;
    }
}
