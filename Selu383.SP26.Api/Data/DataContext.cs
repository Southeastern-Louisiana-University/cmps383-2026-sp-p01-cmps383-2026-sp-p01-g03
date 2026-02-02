using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Entities;

namespace Selu383.SP26.Api.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(x => x.Address)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.TableCount)
                .IsRequired();

            entity.HasData(
                new Location { Id = 1, Name = "Downtown Commons", Address = "100 Main St", TableCount = 15 },
                new Location { Id = 2, Name = "University Lounge", Address = "200 College Dr", TableCount = 25 },
                new Location { Id = 3, Name = "Lakefront Terrace", Address = "300 River Rd", TableCount = 12 }
            );
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(x => x.UserName)
                .HasMaxLength(64)
                .IsRequired();

            entity.Property(x => x.DisplayName)
                .HasMaxLength(128)
                .IsRequired();

            entity.HasData(
                new User { Id = 1, UserName = "bob", DisplayName = "Bob Builder" },
                new User { Id = 2, UserName = "sue", DisplayName = "Sue Storm" }
            );
        });
    }
}
