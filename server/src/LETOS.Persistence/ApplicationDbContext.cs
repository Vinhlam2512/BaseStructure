using LETOS.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace LETOS.Persistence;

public sealed class ApplicationDbContext : DbContext
{

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        base.OnModelCreating(builder);
    }
}
