using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ApiAuth.Model.Schemas;

namespace ApiAuth.Model;

public class UserDatabase(DbContextOptions<UserDatabase> opt) : IdentityDbContext<User>(opt)
{
    public DbSet<User> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
        .HasIndex(user => user.Cpf)
        .IsUnique();
    }
}
