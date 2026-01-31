using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssociationEntities.FileManagerModels;

public partial class MlTntFileManangerContext : DbContext
{
    public MlTntFileManangerContext()
    {
    }

    public MlTntFileManangerContext(DbContextOptions<MlTntFileManangerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MlTntActivityLog> MlTntActivityLogs { get; set; }

    public virtual DbSet<MlTntFile> MlTntFiles { get; set; }

    public virtual DbSet<MlTntFileFolderPermission> MlTntFileFolderPermissions { get; set; }

    public virtual DbSet<MlTntFmuser> MlTntFmusers { get; set; }

    public virtual DbSet<MlTntFolder> MlTntFolders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=MlTntFileMananger_20250216;User Id=sa;Password=sa;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MlTntActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__MlTntAct__5E5499A8E1B8BCD9");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.File).WithMany(p => p.MlTntActivityLogs)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("FK__MlTntActi__FileI__59063A47");

            entity.HasOne(d => d.Folder).WithMany(p => p.MlTntActivityLogs)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("FK__MlTntActi__Folde__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.MlTntActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MlTntActi__UserI__5AEE82B9");
        });

        modelBuilder.Entity<MlTntFile>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__MlTntFil__6F0F989FB0BA7B21");

            entity.HasIndex(e => e.FilePath, "UQ__MlTntFil__48D910BDC799D8A1").IsUnique();

            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.FileData).HasColumnType("text");
            entity.Property(e => e.FileExtension).HasMaxLength(50);
            entity.Property(e => e.FileGuid)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StorageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Folder).WithMany(p => p.MlTntFiles)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("FK__MlTntFile__Folde__4CA06362");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.MlTntFiles)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK__MlTntFile__Uploa__4D94879B");
        });

        modelBuilder.Entity<MlTntFileFolderPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__MlTntFil__EFA6FB0F39F633D4");

            entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PermissionType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.File).WithMany(p => p.MlTntFileFolderPermissions)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("FK__MlTntFile__FileI__52593CB8");

            entity.HasOne(d => d.Folder).WithMany(p => p.MlTntFileFolderPermissions)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("FK__MlTntFile__Folde__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.MlTntFileFolderPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MlTntFile__UserI__5441852A");
        });

        modelBuilder.Entity<MlTntFmuser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__MlTntFMU__1788CCAC917F14A5");

            entity.ToTable("MlTntFMUsers");

            entity.HasIndex(e => e.Username, "UQ__MlTntFMU__536C85E4BA2C484E").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__MlTntFMU__A9D105347AB5AD99").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<MlTntFolder>(entity =>
        {
            entity.HasKey(e => e.FolderId).HasName("PK__MlTntFol__ACD7109FF46BDF7A");

            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FolderName).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.ParentFolderId).HasColumnName("ParentFolderID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MlTntFolders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__MlTntFold__Creat__45F365D3");

            entity.HasOne(d => d.ParentFolder).WithMany(p => p.InverseParentFolder)
                .HasForeignKey(d => d.ParentFolderId)
                .HasConstraintName("FK__MlTntFold__Paren__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
