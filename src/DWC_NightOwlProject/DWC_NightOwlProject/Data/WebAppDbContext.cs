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
            entity.HasKey(e => e.Id).HasName("PK__Backstor__3214EC2780871493");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Characte__3214EC271898FACD");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Encounte__3214EC27E0C5FC6D");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Items__3214EC27D1EF8547");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Maps__3214EC27F0A37D13");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC27E8F5D9A6");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quests__3214EC27868B5930");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Songs__3214EC27404750AA");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__World__3214EC2776A6CC87");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
