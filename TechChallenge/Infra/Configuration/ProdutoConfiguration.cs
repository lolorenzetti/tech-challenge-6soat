using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.Invalid);
            builder.Ignore(p => p.Valid);

            builder.HasKey(p => p.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Categoria)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Preco)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");
        }
    }
}
