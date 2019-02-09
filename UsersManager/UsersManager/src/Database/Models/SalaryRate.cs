using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UsersManager.Database.Models
{
    public class SalaryRate
    {
        public int Rate { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}