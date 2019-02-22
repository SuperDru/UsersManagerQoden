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
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<HashedCredential> Credentials { get; set; }
        public DbSet<SalaryRateRequest> SalaryRateRequests { get; set; }     
        public DbSet<SalaryRate> SalaryRates { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.ApplySnakeCase(builder);
            
            builder.Entity<UserRole>().HasKey(u => new {u.RoleId, u.UserId});
            builder.Entity<SalaryRate>().HasKey(s => new {s.UpdatedAt, s.UserId});
            builder.Entity<SalaryRateRequest>().HasKey(s => new {s.Guid, s.UpdatedAt});
            builder.Entity<HashedCredential>().HasKey(h => h.UserId);
            
            builder.Entity<User>().HasIndex(u => u.NickName).IsUnique();
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            
            InitEntities(builder);
        }

        private static void InitEntities(ModelBuilder builder)
        {
            builder.Entity<User>().Init();
            builder.Entity<Department>().Init();          
            builder.Entity<Role>().Init();
            builder.Entity<UserRole>().Init();
            builder.Entity<SalaryRate>().Init();
            builder.Entity<SalaryRateRequest>().Init();
            builder.Entity<HashedCredential>().Init();
        }
    }
}