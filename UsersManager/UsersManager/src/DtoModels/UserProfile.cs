using System;
using JetBrains.Annotations;
using UsersManager.Database.Models;

namespace UsersManager.DtoModels
{
    public class UserProfile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string NickName { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}