using MediatR;

namespace Application.Features.ClienteContext.GetByCpf
{
    public record GetClienteByCpfRequest : IRequest<GetClienteByCpfResponse>
    {
        public string Cpf { get; set; } = string.Empty;
    }
}
