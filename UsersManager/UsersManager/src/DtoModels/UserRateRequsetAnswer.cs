using System;
using UsersManager.Database.Models;

namespace UsersManager.DtoModels
{
    public class UserRateRequestAnswer
    {
        public int Rate { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public string Reasons { get; set; }
        
        public string Explanation { get; set; }
        
        public Status Status { get; set; }
    }
}