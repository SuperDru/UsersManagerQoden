using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManager.Database.Models
{
    public class Role
    {   
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}