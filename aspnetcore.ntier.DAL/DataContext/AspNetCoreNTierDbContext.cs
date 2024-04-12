 using aspnetcore.ntier.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.ntier.DAL.DataContext;

public class AspNetCoreNTierDbContext : DbContext
{
    public AspNetCoreNTierDbContext(DbContextOptions<AspNetCoreNTierDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Taskk> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
             new User
             {
                 UserId = 1,
                 Username = "johndoe",
                 Email = "johndoe@gmail.com",
                 Password = "123",
                 Name = "John",
                 Surname = "Doe",
             }
         );

        modelBuilder.Entity<Taskk>().HasData(
            new Taskk
            {
                TaskId = 1,
                Title = "First task",
                Description = "Test task",
                Status = "undone"
            }
         );
    }
}