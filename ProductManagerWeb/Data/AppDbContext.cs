using Microsoft.EntityFrameworkCore;
using ProductManager.Models.Entities;

namespace ProductManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Preco).IsRequired();
                entity.Property(p => p.DataCadastro).IsRequired();
            });
        }
    }

}
