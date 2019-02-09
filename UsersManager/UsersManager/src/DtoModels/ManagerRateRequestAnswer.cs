using System;
using UsersManager.Database.Models;

namespace UsersManager.DtoModels
{
    public class ManagerRateRequestAnswer
    {
        public string Guid { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public User User { get; set; }
        
        public int Rate { get; set; }
        
        public string Reasons { get; set; }

        public string PrivateExplanation { get; set; }
        
        public string Explanation { get; set; }
        
        public Status Status { get; set; }
    }
}