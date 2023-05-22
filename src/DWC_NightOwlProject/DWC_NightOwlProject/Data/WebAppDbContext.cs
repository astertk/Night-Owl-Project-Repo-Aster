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
            entity.HasKey(e => e.Id).HasName("PK__Backstor__3214EC279ABA2780");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Characte__3214EC27448A9129");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Encounte__3214EC2768D18D5B");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Maps__3214EC2777A7BFD7");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC272D28071F");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quests__3214EC27E3C6272D");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Songs__3214EC276817A99E");
        });

        modelBuilder.Entity<World>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__World__3214EC277168821E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
