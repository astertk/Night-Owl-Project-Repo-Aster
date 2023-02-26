using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DWC_NightOwlProject.Data;

public partial class WebAppDbContext : DbContext
{
    public WebAppDbContext()
    {
    }

    public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<World> Worlds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CampaignCreator;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC27912A6978");

            entity.HasOne(d => d.Template).WithMany(p => p.Materials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Material_Fk_Template");

            entity.HasOne(d => d.World).WithMany(p => p.Materials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Material_Fk_World");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Template__3214EC279491F26B");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__World__3214EC27F7511FB6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
