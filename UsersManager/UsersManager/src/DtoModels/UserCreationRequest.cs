using System;
using UsersManager.Database.Models;

namespace UsersManager.DtoModels
{
    public class UserCreationRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime InvitedAt { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public string Password { get; set; }
    }
}