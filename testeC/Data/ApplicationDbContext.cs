using Microsoft.EntityFrameworkCore;
using TestC.Models;

namespace TestC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pedido> pedidos { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>(entity =>
    {
        entity.ToTable("usuarios");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Nome).HasColumnName("nome");
        entity.Property(e => e.Email).HasColumnName("email");
        entity.Property(e => e.Password).HasColumnName("password");
        entity.Property(e => e.DataCadastro).HasColumnName("data_cadastro");
    });
}

    }
}
