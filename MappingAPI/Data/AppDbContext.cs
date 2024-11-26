using Microsoft.EntityFrameworkCore;
using MappingAPI.Models;

namespace MappingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Таблицы в базе данных
        public DbSet<MappingData> MappingData { get; set; }
        public DbSet<InputData> InputData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Дополнительная настройка таблиц (если требуется)
            modelBuilder.Entity<MappingData>().ToTable("MappingData");
            modelBuilder.Entity<InputData>().ToTable("InputData");
        }
    }
}