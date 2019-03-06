using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManager.Database.Models
{
    public class HashedCredential
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public byte[] Salt { get; set; }

        public string HashedPassword { get; set; }
    }
}