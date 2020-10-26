using System;
using BurgerkingCaloriesCalculator.Infrastructure.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerkingCaloriesCalculator.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        internal DbSet<MenuDataModel> Menus { get; set; }

        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuDataModel>(b =>
            {
                b.HasKey(m => m.Id);
                b.ToTable("t_menus");

                b.Property(m => m.ProductIds)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("product_ids")
                    .HasConversion(
                        v => string.Join(';', v),
                        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries));
            });
        }
    }
}