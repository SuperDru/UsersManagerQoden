using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UsersManager.DtoModels;

namespace UsersManager.Database.Models
{
    public enum Status
    {
        Pending = 1,
        Accepted = 2,
        Declined = 3
    }
    
    public class SalaryRateRequest
    {
        public string Guid { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rate { get; set; } 
        public DateTime UpdatedAt { get; set; }    
        public string Reasons { get; set; }
        public string PrivateExplanation { get; set; }    
        public string Explanation { get; set; }
        [ForeignKey("Manager")] 
        public int? ManagerId { get; set; }
        public User Manager { get; set; }       
        public Status? Status { get; set; }

    }
}