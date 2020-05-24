namespace OMF.Common.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public byte[] Password { get; set; }

        public byte[] PasswordKey { get; set; }

        public string Address { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
    }
}