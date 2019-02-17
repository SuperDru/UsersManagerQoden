using System.Net.NetworkInformation;
using System.Security.Policy;

namespace UsersManager
{
    public static class ErrorMessages
    {
        public static string CredentialsMsg = "Invalid username or password";
        public static string IncorrectDateUpdateMsg = "Incorrect date update";
        public static string NoManagerAttachmentMsg = "Manager hasn't been attached to user";
        
        public static string NoUserWithIdMsg(int id) => $"No user with id '{id}'";
        public static string NoUserWithNicknameMsg(string nickname) => $"No user with nickname '{nickname}'";
        public static string UserWithNicknameExistsMsg(string nickname) => $"User with nickname '{nickname}' already exists";
        public static string UserWithEmailExistsMsg(string email) => $"User with email '{email}' already exists";
    }
}