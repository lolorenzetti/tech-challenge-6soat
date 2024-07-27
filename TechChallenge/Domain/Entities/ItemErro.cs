namespace Domain.Entities
{
    public record ItemErro
    {
        public string Message { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
    }
}
