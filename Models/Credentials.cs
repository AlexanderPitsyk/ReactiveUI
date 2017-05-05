using System.ComponentModel.DataAnnotations;

namespace ReactiveUIApplication.Models
{
    public class Credential
    {
        public Credential(User user, string password)
        {
            User = user;
            Password = password;
        }

        [Key]
        public int UserId { get; set; }

        public string Password { get; set; }

        public virtual User User { get; set; }
    }
}