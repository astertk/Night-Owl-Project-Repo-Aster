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
        => optionsBuilder.UseSqlServer("Name=AppConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC277DF42520");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Template__3214EC272597D55B");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__World__3214EC2718A658A3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}