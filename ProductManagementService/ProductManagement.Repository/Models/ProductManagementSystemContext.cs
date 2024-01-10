using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Repository.Models;

public partial class ProductManagementSystemContext : DbContext
{
    public ProductManagementSystemContext()
    {
    }

    public ProductManagementSystemContext(DbContextOptions<ProductManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryRecord> Categories { get; set; }

    public virtual DbSet<ProductRecord> Products { get; set; }

    public virtual DbSet<SubCategoryRecord> SubCategories { get; set; }

    public virtual DbSet<UserRecord> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F9POT9N;Database=ProductManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryRecord>(entity =>
        {
            entity.ToTable("Category");
        });

        modelBuilder.Entity<ProductRecord>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductCode)
                .HasMaxLength(50)
                .HasColumnName("Product_Code");
            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategory_Id");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_Product_SubCategory");
        });

        modelBuilder.Entity<SubCategoryRecord>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Category");
        });

        modelBuilder.Entity<UserRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User1");

            entity.ToTable("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
