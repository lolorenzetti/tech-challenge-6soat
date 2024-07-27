using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Ignore(p => p.Valid);
            builder.Ignore(p => p.Invalid);
            builder.Ignore(p => p.Errors);

            builder.HasKey(p => p.Id);
        }
    }
}
