namespace backend.src.Database;

using backend.src.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class Persistence(DbContextOptions<Persistence> options) : DbContext(options)
{
  public DbSet<TaskModel> Tasks { get; set; }
  public DbSet<UserModel> Users { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder
         .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserModel>()
      .HasIndex(u => u.Email)
      .IsUnique();

    modelBuilder.Entity<TaskModel>()
   .HasOne(t => t.User)
   .WithMany(u => u.Tasks)
   .HasForeignKey(t => t.UserId);
    base.OnModelCreating(modelBuilder);
  }

}