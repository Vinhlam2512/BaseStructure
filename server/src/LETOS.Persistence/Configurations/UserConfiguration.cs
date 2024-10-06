using LETOS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LETOS.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(user => user.Name, name =>
        {
            name.Property(n => n.UserName)
                .IsRequired()
                .HasColumnName("UserName")
                .HasColumnType("varchar")
                .HasMaxLength(20);

            name.HasIndex(name => name.UserName).IsUnique();

            name.Property(n => n.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            name.Property(n => n.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("nvarchar")
                .HasMaxLength(20);
        });

        builder.Property(user => user.Email)
            .HasMaxLength(20)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(user => user.PassWordHashed)
           .HasMaxLength(200)
           .IsRequired()
           .HasColumnType("varchar");

        builder.Property(user => user.AccessFailed)
          .IsRequired()
          .HasColumnType("int");

        builder.Property(user => user.IsLocked)
          .IsRequired()
          .HasColumnType("bit");

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
