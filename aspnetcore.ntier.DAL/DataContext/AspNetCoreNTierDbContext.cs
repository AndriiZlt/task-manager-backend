using aspnetcore.ntier.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.ntier.DAL.DataContext;

public class AspNetCoreNTierDbContext :DbContext
{
    public AspNetCoreNTierDbContext(DbContextOptions<AspNetCoreNTierDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Taskk> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<Friend> Friends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
             new User
             {
                 UserId = 1,
                 UserName = "johndoe",
                 Email = "johndoe@gmail.com",
                 Password = "123",
                 Name = "John",
                 Surname = "Doe",
             }
         );

        modelBuilder.Entity<Subtask>()
            .HasOne(s => s.Task)
            .WithMany(t => t.Subtasks)
            .HasForeignKey(t => t.TaskId)
            .IsRequired();
    }
}