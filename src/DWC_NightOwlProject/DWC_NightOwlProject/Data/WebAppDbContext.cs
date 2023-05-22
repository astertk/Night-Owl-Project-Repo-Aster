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

    public virtual DbSet<Backstory> Backstories { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Map> Maps { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<World> Worlds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=AppConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Backstory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Backstor__3214EC27ED3E1715");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Characte__3214EC27967F5D78");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Encounte__3214EC27BB146F92");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Items__3214EC27EFE00D5F");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Maps__3214EC271126AE15");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC27FAFBA4A9");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quests__3214EC27F4BD68A0");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Songs__3214EC27382DBEB2");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__World__3214EC27661A529C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
