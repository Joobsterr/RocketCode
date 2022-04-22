using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class User
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastLogin { get; set; }

        public User(string username, string email, string password, DateTime creationDate, DateTime lastLogin)
        {
            Username = username;
            Email = email;
            Password = password;
            CreationDate = creationDate;
            LastLogin = lastLogin;
        }
    }
}
