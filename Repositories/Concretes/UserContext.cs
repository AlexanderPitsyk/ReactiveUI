using System.Data.Entity;
using ReactiveUIApplication.Models;

namespace ReactiveUIApplication.Repositories
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Credential> Credentials { get; set; }
    }
}