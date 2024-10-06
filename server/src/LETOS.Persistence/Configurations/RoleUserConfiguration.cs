using LETOS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LETOS.Persistence.Configurations;

public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.ToTable("RoleUsers");

        builder.HasKey(roleUser => new { roleUser.RoleId, roleUser.UserId });

        builder
            .HasOne(ru => ru.User)
            .WithMany(u => u.RoleUsers)
            .HasForeignKey(ru => ru.UserId);

        builder
            .HasOne(ru => ru.Role)
            .WithMany(r => r.RoleUsers)
            .HasForeignKey(ru => ru.RoleId);
    }
}
