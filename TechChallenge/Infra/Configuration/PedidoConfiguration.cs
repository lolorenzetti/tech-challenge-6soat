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
    public class PedidoConfiguration : BaseEntityConfiguration<Pedido>
    {
        public override void Configure(EntityTypeBuilder<Pedido> builder)
        {
            base.Configure(builder);

            builder
                .HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(c => c.ClienteId)
                .IsRequired(false);

            builder.Property(p => p.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.HasMany(e => e.Itens)
                .WithOne()
                .IsRequired();
        }
    }
}
