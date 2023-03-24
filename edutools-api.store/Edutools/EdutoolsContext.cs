using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace edutools_api.store.Edutools;

public partial class EdutoolsContext : DbContext
{
    public EdutoolsContext()
    {
    }

    public EdutoolsContext(DbContextOptions<EdutoolsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=edutools", ServerVersion.Parse("10.4.17-mariadb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
