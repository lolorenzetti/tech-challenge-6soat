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

            Validar();
        }

        public string Nome { get; }
        public string Email { get; }
        public string Cpf { get; }

        public override void Validar()
        {
            ClienteValidatorFactory
                .Create()
                .Validar(this);
        }
    }
}
