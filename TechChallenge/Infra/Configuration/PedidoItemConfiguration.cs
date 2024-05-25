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
    public class PedidoItemConfiguration : BaseEntityConfiguration<PedidoItem>
    {
        public override void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.Observacao)
                .IsRequired(false)
                .HasColumnType("varchar(255)");

            builder.HasOne<Produto>()
                .WithMany()
                .HasForeignKey(e => e.ProdutoId)
                .IsRequired();
        }
    }
}
