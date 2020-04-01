using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    // Representação do Banco de Dados em Memória
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }
    }
}