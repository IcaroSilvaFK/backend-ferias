namespace backend.src.Database;

using backend.src.Application.Models;
using Microsoft.EntityFrameworkCore;
public class Persistence(DbContextOptions<Persistence> options) : DbContext(options)
{
    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
      // optionsBuilder.UseSqlServer("Server=localhost,1433; User Id=sa;Password=docker; Database=backend;");
      
      optionsBuilder.UseSqlite("DataSource=backend.db; Cache=Shared");
      base.OnConfiguring(optionsBuilder);
    }

    
}