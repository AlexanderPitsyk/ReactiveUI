using System.Data.Entity;
using ReactiveUIApplication.Models;

namespace ReactiveUIApplication.Repositories
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItemList { get; set; }
    }
}