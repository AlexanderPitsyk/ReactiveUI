using System.ComponentModel.DataAnnotations;

namespace ReactiveUIApplication.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public User() { }

        public User(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}
