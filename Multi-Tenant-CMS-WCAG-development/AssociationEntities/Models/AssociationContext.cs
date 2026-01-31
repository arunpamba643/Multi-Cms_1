using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssociationEntities.Models;

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

    public virtual DbSet<BlogAttribute> BlogAttributes { get; set; }

    public virtual DbSet<BlogImage> BlogImages { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<ComponentProperty> ComponentProperties { get; set; }

    public virtual DbSet<Container> Containers { get; set; }

    public virtual DbSet<CustomMenu> CustomMenus { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Row> Rows { get; set; }

    public virtual DbSet<Sampledatum> Sampledata { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantComponent> TenantComponents { get; set; }

    public DbSet<File> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=SAILS-DM241;Database=Association;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true;Trusted_Connection=True;");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Association.db");

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
            entity.Property(e => e.ContentFontStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Facebook).IsUnicode(false);
            entity.Property(e => e.FooterBackgroundColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FooterFontColor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FooterFontStyle)
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
            entity.Property(e => e.HeaderFontStyle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Heading)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Instagram).IsUnicode(false);
            entity.Property(e => e.LinkedIn).IsUnicode(false);
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
            entity.Property(e => e.Twitter).IsUnicode(false);
            entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.WhatsApp).IsUnicode(false);
        });

        modelBuilder.Entity<BlogAttribute>(entity =>
        {
            entity.HasKey(e => e.Baid).HasName("PK__BlogAttr__31BC6FE8CE1D4062");

            entity.Property(e => e.Baid).HasColumnName("BAId");
            entity.Property(e => e.AttributeTitle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AttributeType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Bp).WithMany(p => p.BlogAttributes)
                .HasForeignKey(d => d.BpId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__BlogAttrib__BpId__6FE99F9F");
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

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.Bpid).HasName("PK__BlogPost__5CE97E7EF2475066");

            entity.ToTable("BlogPost");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DetailItemId).HasMaxLength(50);
            entity.Property(e => e.Discipline)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.Division)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.HeaderImage).IsUnicode(false);
            entity.Property(e => e.Link).HasMaxLength(250);
            entity.Property(e => e.PublishedOn).HasColumnType("datetime");
            entity.Property(e => e.ScopeType).HasMaxLength(50);
            entity.Property(e => e.Tags).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.ValidFrom).HasColumnType("datetime");
            entity.Property(e => e.ValidTo).HasColumnType("datetime");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__Componen__D79CF04ED94FF617");

            entity.ToTable("Component");

            entity.Property(e => e.ComponentName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ComponentType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Container).WithMany(p => p.Components)
                .HasForeignKey(d => d.ContainerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Component_ContainerId");
        });

        modelBuilder.Entity<ComponentProperty>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__Componen__70C9A735E672483E");

            entity.ToTable("ComponentProperty");

            entity.Property(e => e.Key)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Value).IsUnicode(false);

            entity.HasOne(d => d.Component).WithMany(p => p.ComponentProperties)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ComponentPropery_ComponentId");
        });

        modelBuilder.Entity<Container>(entity =>
        {
            entity.HasKey(e => e.ContainerId).HasName("PK__Containe__037960BBFB6AA19F");

            entity.Property(e => e.ContainerName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Row).WithMany(p => p.Containers)
                .HasForeignKey(d => d.RowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Containers_RowId");
        });

        modelBuilder.Entity<CustomMenu>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("CustomMenu");

            entity.Property(e => e.MenuName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PageUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.ToTable("Page");

            entity.Property(e => e.PageScopeType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('draft')");
            entity.Property(e => e.PageTitle)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PublishEndDate).HasColumnType("datetime");
            entity.Property(e => e.PublishStartDate).HasColumnType("datetime");
            entity.Property(e => e.ResourcePath)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Row>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK__Rows__FFEE743177BA4A42");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsSaved).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Sampledatum>(entity =>
        {
            entity.HasKey(e => e.SdId).HasName("PK__sampleda__ADEB9F515C8CD872");

            entity.ToTable("sampledata");

            entity.Property(e => e.Data)
                .IsUnicode(false)
                .HasColumnName("data");
            entity.Property(e => e.SampleData)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sampleData");
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

        modelBuilder.Entity<TenantComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TenantCo__3213E83F54818C01");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ComponentTypeId).HasColumnName("component_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Ordernumber).HasColumnName("ordernumber");
            entity.Property(e => e.Text)
                .IsUnicode(false)
                .HasColumnName("text");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<File>(entity => { entity.HasKey(e => e.FileID); entity.Property(e => e.FileName).IsRequired().HasMaxLength(255); entity.Property(e => e.FilePath).IsRequired().HasMaxLength(255); entity.Property(e => e.FileSize).IsRequired(); entity.Property(e => e.FileType).IsRequired().HasMaxLength(50); entity.Property(e => e.UploadDate).IsRequired(false); entity.Property(e => e.TenantId).HasMaxLength(300); }); modelBuilder.Entity<Folder>(entity => { entity.HasKey(e => e.FolderID); entity.Property(e => e.FolderName).IsRequired().HasMaxLength(255); entity.Property(e => e.ParentFolderID).IsRequired(false); entity.Property(e => e.CreatedDate).IsRequired(false); entity.Property(e => e.TenantId).HasMaxLength(300); }); modelBuilder.Entity<Permission>(entity => { entity.HasKey(e => e.PermissionID); entity.Property(e => e.FileID).IsRequired(); entity.Property(e => e.TenantId).HasMaxLength(300); entity.Property(e => e.PermissionType).IsRequired().HasMaxLength(50); entity.Property(e => e.GrantedDate).IsRequired(false); entity.HasOne<File>().WithMany().HasForeignKey(e => e.FileID); });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
