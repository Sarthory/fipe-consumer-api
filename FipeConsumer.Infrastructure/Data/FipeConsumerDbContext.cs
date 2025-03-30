using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Infrastructure.Data
{
    public class FipeConsumerDbContext(DbContextOptions<FipeConsumerDbContext> options) : DbContext(options)
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasKey(b => b.BrandId);

            modelBuilder.Entity<Model>().HasKey(m => m.ModelId);

            modelBuilder.Entity<Year>().HasKey(y => y.YearId);

            modelBuilder.Entity<Job>().HasKey(j => j.JobId);

            modelBuilder.Entity<Price>()
                .HasKey(p => p.PriceId);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.Model)
                .WithMany()
                .HasForeignKey(p => p.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.Year)
                .WithMany()
                .HasForeignKey(p => p.YearId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
