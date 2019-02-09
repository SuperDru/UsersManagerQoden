using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsersManager.DtoModels;

namespace UsersManager.Database.Models
{
    public class User
    {   
        public int Id { get; set; }

        public string Guid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime InvitedAt { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        
        
        public User Manager { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<SalaryRate> SalaryRates { get; set; }
    }
}