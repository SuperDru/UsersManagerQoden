namespace UsersManager.Database.Models
{
    public class HashedCredentials
    {
        public int UserId { get; set; }
        
        public byte[] Salt { get; set; }

        public string HashedPassword { get; set; }
    }
}