using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WEBManagerZ.Models;
using WEBManagerZ.ViewModels;

#nullable disable

namespace WEBManagerZ.Data
{
    public partial class ManagerZContext : IdentityDbContext<AppUser>
    {
        public ManagerZContext()
        {
        }

        public ManagerZContext(DbContextOptions<ManagerZContext> options)
            : base(options)
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Day>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MostCommonCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MostCommonProduct)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TotalMade).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.TotalSpent).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");               

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.OrderId, "IX_Products_OrderId");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CostToMake).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<Cart>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                .WithOne(e => e.Cart)
                .HasForeignKey<AppUser>(e => e.CartId);
            });

            modelBuilder.Entity<CartProduct>()
            .HasKey(e => new { e.CartId, e.ProductId });

            modelBuilder.Entity<CartProduct>()
            .HasOne(t => t.Cart)
            .WithMany(t => t.CartProduct)
            .HasForeignKey(t => t.CartId);

            modelBuilder.Entity<CartProduct>()
            .HasOne(t => t.Product)
            .WithMany(t => t.CartProduct)
            .HasForeignKey(t => t.ProductId);

            modelBuilder.Entity<CartProduct>().Property("Quantity").HasDefaultValue(1);

            modelBuilder.Entity<Order>(entity => entity.Property(e => e.Cost).HasColumnType("decimal(18, 3)"));

            OnModelCreatingPartial(modelBuilder);
        }

        public DbSet<Day> Days { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProduct { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<WEBManagerZ.ViewModels.OrderViewModel> OrderViewModel { get; set; }
    }
}
