using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder
                .HasOne<Pagamento>(e => e.Pagamento)
                .WithOne()
                .HasForeignKey<Pagamento>(e => e.PedidoId)
                .IsRequired(true);

            builder.Property(p => p.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.HasMany(e => e.Itens)
                .WithOne()
                .IsRequired();
        }
    }
}
