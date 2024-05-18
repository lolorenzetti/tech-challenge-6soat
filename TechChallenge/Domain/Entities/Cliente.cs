using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Cliente
    {
        public Cliente(int id, string nome, string email, string cpf)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public int Id { get; }
        public string Nome { get; }
        public string Email { get; }
        public string Cpf { get; }
    }
}
