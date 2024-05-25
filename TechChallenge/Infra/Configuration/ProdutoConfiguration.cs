using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration
{
    public class ProdutoConfiguration : BaseEntityConfiguration<Produto>
    {
        public override void Configure(EntityTypeBuilder<Produto> builder)
        {
            base.Configure(builder);

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
