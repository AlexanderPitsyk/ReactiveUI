using System.ComponentModel.DataAnnotations;

namespace ReactiveUIApplication.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}