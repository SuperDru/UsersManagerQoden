using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManager.Database.Models;

namespace UsersManager.Database
{
    public static class EntitiesInit
    {
        public static void Init(this EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Guid = Guid.NewGuid(),
                    FirstName = "Jhon",
                    LastName = "Foster",
                    NickName = "JFoster",
                    Email = "jfoster@gmail.com",
                    PhoneNumber = 89129541254L,
                    InvitedAt = DateTime.Parse("2014-12-15"),
                    Description = "American",
                    DepartmentId = 1
                },
                new User
                {
                    Id = 2,
                    Guid = Guid.NewGuid(),
                    FirstName = "Alexander",
                    LastName = "Shishkin",
                    Patronymic = "Dmitrievich",
                    NickName = "AShishkin",
                    Email = "ashishkin@mail.ru",
                    PhoneNumber = 83149545254L,
                    InvitedAt = DateTime.Parse("2015-11-13"),
                    Description = "Russian",
                    DepartmentId = 2
                },
                new User
                {
                    Id = 3,
                    Guid = Guid.NewGuid(),
                    FirstName = "Andrey",
                    LastName = "Shurikov",
                    Patronymic = "Vasilievich",
                    NickName = "AShurikov",
                    Email = "ashurikov@mail.ru",
                    PhoneNumber = 83149565253L,
                    InvitedAt = DateTime.Parse("2011-01-29"),
                    Description = "Russian",
                    DepartmentId = 2
                });
        }
        public static void Init(this EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department() { Id = 1, Name = "developer"},
                new Department() { Id = 2, Name = "manager"});
        }
        public static void Init(this EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Id = 1, Name = "admin"},
                new Role() { Id = 2, Name = "manager"},
                new Role() { Id = 3, Name = "user"});
        }
        public static void Init(this EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole() { UserId = 1, RoleId = 3 },
                new UserRole() { UserId = 2, RoleId = 2 },
                new UserRole() { UserId = 3, RoleId = 1 });
        }
        public static void Init(this EntityTypeBuilder<SalaryRate> builder)
        {
            builder.HasData(
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
        }       
        public static void Init(this EntityTypeBuilder<SalaryRateRequest> builder)
        {
            var guid = Guid.NewGuid();
            builder.HasData(
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
        
        public static void Init(this EntityTypeBuilder<HashedCredential> builder)
        {
            var creds = new List<HashedCredential>();

            var salt = PasswordGenerator.GenerateSalt();
            var password = PasswordGenerator.HashPassword("1", salt);
            creds.Add(new HashedCredential()
            {
                UserId = 1,
                Salt = salt,
                HashedPassword = password
            });
            
            salt = PasswordGenerator.GenerateSalt();
            password = PasswordGenerator.HashPassword("12", salt);
            creds.Add(new HashedCredential()
            {
                UserId = 2,
                Salt = salt,
                HashedPassword = password
            });
            
            salt = PasswordGenerator.GenerateSalt();
            password = PasswordGenerator.HashPassword("123", salt);
            creds.Add(new HashedCredential()
            {
                UserId = 3,
                Salt = salt,
                HashedPassword = password
            });
            
            builder.HasData(creds);
        }       
    }
}