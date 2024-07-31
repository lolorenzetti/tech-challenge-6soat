using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration
{
    public class PagamentoConfiguration : BaseEntityConfiguration<Pagamento>
    {
        public override void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.PagamentoExternoId)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
