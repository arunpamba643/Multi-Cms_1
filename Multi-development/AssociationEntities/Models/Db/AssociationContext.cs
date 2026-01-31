using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssociationEntities.Models.Db;

public partial class AssociationContext : DbContext
{
    public AssociationContext()
    {
    }

    public AssociationContext(DbContextOptions<AssociationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogImage> BlogImages { get; set; }

    public virtual DbSet<CustomMenu> CustomMenus { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ALIPL2453;Initial Catalog=Association;Persist Security Info=False;User ID=sa;Password=Accionlabs@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.Banner).IsUnicode(false);
            entity.Property(e => e.BlogName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.BrandName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ContentFontColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContentFontSize)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FooterBackgroundColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FooterText)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.HeaderBackgroundColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HeaderFontColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Heading)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.MainMenuColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MainMenuFontSize)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MainMenuFontStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PublishedDateTime).HasColumnType("datetime");
            entity.Property(e => e.SubChildFontStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubChildMenuColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubChildMenuFontSize)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubMenuColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubMenuFontSize)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubMenuFontStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TextContent).IsUnicode(false);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<BlogImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).IsUnicode(false);
            entity.Property(e => e.Text).IsUnicode(false);
            entity.Property(e => e.Udf)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("UDF");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogImages)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlogImages_Blog");

            entity.HasOne(d => d.Tenant).WithMany(p => p.BlogImages)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlogImages_Tenants");
        });

        modelBuilder.Entity<CustomMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("CustomMenu");

            entity.Property(e => e.MenuName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PageUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.Property(e => e.TenantId).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
