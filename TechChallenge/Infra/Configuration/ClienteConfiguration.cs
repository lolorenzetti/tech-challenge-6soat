using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Configuration
{
    public class ClienteConfiguration : BaseEntityConfiguration<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("char(11)");
        }
    }
}
