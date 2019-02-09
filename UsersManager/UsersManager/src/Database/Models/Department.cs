
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManager.Database.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}