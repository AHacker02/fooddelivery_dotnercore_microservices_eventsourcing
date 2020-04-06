using System;

namespace OMF.Common.Models
{
    public class User
    {
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string Name { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
    }
}
