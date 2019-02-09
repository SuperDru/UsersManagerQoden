using System;
using Lesson1.Helpers;
using Microsoft.EntityFrameworkCore;
using UsersManager.Database.Models;

namespace UsersManager.Database
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SalaryRateRequest> SalaryRateRequests { get; set; }     
        public virtual DbSet<SalaryRate> SalaryRates { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.ApplySnakeCase(builder);
            
            builder.Entity<UserRole>().HasKey(u => new {u.RoleId, u.UserId});
            builder.Entity<SalaryRate>().HasKey(s => new {s.UpdatedAt, s.UserId});
            builder.Entity<SalaryRateRequest>().HasKey(s => new {s.Guid, s.UpdatedAt});

            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Guid = Guid.NewGuid().ToString(),
                    FirstName = "Jhon",
                    LastName = "Foster",
                    NickName = "JFoster",
                    Password = "1",
                    Email = "jfoster@gmail.com",
                    PhoneNumber = 89129541254L,
                    InvitedAt = DateTime.Parse("2014-12-15"),
                    Description = "American",
                    DepartmentId = 1
                },
                new User
                {
                    Id = 2,
                    Guid = Guid.NewGuid().ToString(),
                    FirstName = "Alexander",
                    LastName = "Shishkin",
                    Patronymic = "Dmitrievich",
                    NickName = "AShishkin",
                    Password = "12",
                    Email = "ashishkin@mail.ru",
                    PhoneNumber = 83149545254L,
                    InvitedAt = DateTime.Parse("2015-11-13"),
                    Description = "Russian",
                    DepartmentId = 2
                },
                new User
                {
                    Id = 3,
                    Guid = Guid.NewGuid().ToString(),
                    FirstName = "Andrey",
                    LastName = "Shurikov",
                    Patronymic = "Vasilievich",
                    NickName = "AShurikov",
                    Password = "123",
                    Email = "ashurikov@mail.ru",
                    PhoneNumber = 83149565253L,
                    InvitedAt = DateTime.Parse("2011-01-29"),
                    Description = "Russian",
                    DepartmentId = 2
                });

            builder.Entity<Department>().HasData(
                new Department() { Id = 1, Name = "developer"},
                new Department() { Id = 2, Name = "manager"});
            
            builder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "admin"},
                new Role() { Id = 2, Name = "manager"},
                new Role() { Id = 3, Name = "user"});
            
            builder.Entity<UserRole>().HasData(
                new UserRole() { UserId = 1, RoleId = 3 },
                new UserRole() { UserId = 2, RoleId = 2 },
                new UserRole() { UserId = 3, RoleId = 1 });

            builder.Entity<SalaryRate>().HasData(
                new SalaryRate()
                {
                    Rate = 1000,
                    UpdatedAt = DateTime.Parse("2014-12-15"),
                    UserId = 1
                }, 
                new SalaryRate()
                {
                    Rate = 1200,
                    UpdatedAt = DateTime.Parse("2016-11-12"),
                    UserId = 1
                },
                new SalaryRate()
                {
                    Rate = 1500,
                    UpdatedAt = DateTime.Parse("2015-11-13"),
                    UserId = 2
                },
                new SalaryRate()
                {
                    Rate = 1800,
                    UpdatedAt = DateTime.Parse("2015-02-25"),
                    UserId = 2
                },
                new SalaryRate()
                {
                    Rate = 2500,
                    UpdatedAt = DateTime.Parse("2011-01-29"),
                    UserId = 3
                });


            var guid = Guid.NewGuid().ToString();
            builder.Entity<SalaryRateRequest>().HasData(
                new SalaryRateRequest()
                {
                    Guid = guid,
                    UserId = 1,
                    ManagerId = 2,
                    Rate = 1500,
                    UpdatedAt = DateTime.Parse("2017-05-11"),
                    Reasons = "Just so",
                    Explanation = "You haven't enough experience.",
                    PrivateExplanation = "He's way too dumb.",
                    Status = Status.Declined
                },
                new SalaryRateRequest()
                {
                    Guid = guid,
                    UserId = 1,
                    ManagerId = 2,
                    Rate = 1500,
                    UpdatedAt = DateTime.Parse("2017-05-08"),
                    Reasons = "Just so",
                    Status = Status.Pending
                });
        }
    }
}