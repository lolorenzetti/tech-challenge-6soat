using Domain.Factory;

namespace Domain.Entities
{
    public class Cliente : Entity
    {
        public Cliente(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;

            Validar<Cliente>(this, ClienteValidatorFactory.Create());
        }

        public string Nome { get; }
        public string Email { get; }
        public string Cpf { get; }
    }
}
