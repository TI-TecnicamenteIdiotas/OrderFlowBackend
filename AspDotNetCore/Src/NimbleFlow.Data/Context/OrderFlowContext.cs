using Microsoft.EntityFrameworkCore;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Data.Context;

public partial class OrderFlowContext : DbContext
{
    public OrderFlowContext()
    {
    }

    public OrderFlowContext(DbContextOptions<OrderFlowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category>? Categories { get; set; }
    public virtual DbSet<Item>? Items { get; set; }
    public virtual DbSet<Product>? Products { get; set; }
    public virtual DbSet<Table>? Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_Items_ProductId");

            entity.HasIndex(e => e.TableId, "IX_Items_TableId");

            entity.Property(e => e.Additional).HasPrecision(16, 2);

            entity.Property(e => e.Count).HasDefaultValueSql("1");

            entity.Property(e => e.Discount).HasPrecision(16, 2);

            entity.Property(e => e.Note)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Status).HasDefaultValueSql("1");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Table)
                .WithMany(p => p.Items)
                .HasForeignKey(d => d.TableId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.Property(e => e.CategoryId).HasDefaultValueSql("1");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");

            entity.Property(e => e.Price).HasPrecision(16, 2);

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.PaidValue).HasPrecision(16, 2);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}