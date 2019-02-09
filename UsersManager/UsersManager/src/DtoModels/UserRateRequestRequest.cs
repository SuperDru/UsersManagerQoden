using System;

namespace UsersManager.DtoModels
{
    public class UserRateRequestRequest
    {
        public int UserId { get; set; }
        
        public int Rate { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public string Reasons { get; set; }
    }
}