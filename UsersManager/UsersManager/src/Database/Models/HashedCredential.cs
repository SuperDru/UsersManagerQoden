namespace UsersManager.Database.Models
{
    public class HashedCredential
    {
        public int UserId { get; set; }
        
        public byte[] Salt { get; set; }

        public string HashedPassword { get; set; }
    }
}