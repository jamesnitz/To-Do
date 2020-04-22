using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simple_ToDo.Models;

namespace simple_ToDo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<TodoStatus> ToDoStatus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "joe",
                LastName = "data",
                UserName = "joe@admin.com",
                NormalizedUserName = "JOE@ADMIN.COM",
                Email = "joe@admin.com",
                NormalizedEmail = "JOE@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin1!*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<TodoStatus>().HasData(
                new TodoStatus()
                {
                    Id = 1,
                    Title = "Not starting"
                },
                new TodoStatus()
                {
                    Id = 2,
                    Title = "Workinonit"
                },
                new TodoStatus()
                {
                    Id = 3,
                    Title = "FIN"
                });
            // Create two todo items
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem()
                {
                    Id = 1,
                    Title = "Mow the dog",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    TodoStatusId = 1

                },
                new TodoItem()
                {
                    Id = 2,
                    Title = "Walk the yard",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    TodoStatusId = 2
                },
                new TodoItem()
                {
                    Id = 3,
                    Title = "Yell at void",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    TodoStatusId = 3
                }
            );


        }
    }
}
