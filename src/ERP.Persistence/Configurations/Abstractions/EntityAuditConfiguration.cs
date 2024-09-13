using ERP.Domain.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Persistence.Configurations.Abstractions;
public abstract class EntityAuditConfiguration<TEntity, T> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityAuditBase<T>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedBy)
            .HasDefaultValue(null)
            .IsRequired(false);

        builder.Property(e => e.CreatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.Now)
            .HasColumnType("datetime2");

        builder.Property(e => e.ModifiedBy)
            .IsRequired(false);

        builder.Property(e => e.ModifiedAt)
            .IsRequired(false)
            .HasColumnType("datetime2");
    }
}
