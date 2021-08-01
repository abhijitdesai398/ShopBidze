using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopBridge.Models
{
    [ExcludeFromCodeCoverage]
    public partial class InventoryDBContext : DbContext
    {
        public InventoryDBContext()
        {
        }

        public InventoryDBContext(DbContextOptions<InventoryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; }
              

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Inventor__2D10D16AD5C7D55A");

                entity.ToTable("Inventory");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.ContryOfOrigin)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contryOfOrigin");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("discription");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
