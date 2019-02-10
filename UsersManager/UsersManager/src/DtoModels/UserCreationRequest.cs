using UsersManager.Database.Models;

namespace UsersManager.DtoModels
{
    public class UserCreationRequest
    {
        public User User { get; set; }

        public string Password { get; set; }
    }
}