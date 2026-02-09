using Microsoft.EntityFrameworkCore;
using ApiAuth.Model.Schemas;

namespace ApiAuth.Model;

public class UserDatabase : DbContext
{
    public DbSet<User> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasIndex(user => user.Cpf)
        .IsUnique();

        modelBuilder.Entity<User>()
        .HasIndex(user => user.Email)
        .IsUnique();
    }
}
