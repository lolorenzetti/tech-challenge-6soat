namespace Application.Features.ClienteContext.GetByCpf
{
    public class GetClienteByCpfResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
    }
}
