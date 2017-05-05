using ReactiveUIApplication.Models;

namespace ReactiveUIApplication.Repositories
{
    public class ToDoRepository : RepositoryBase<ToDoItem>
    {
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context) : base(context)
        {
            _context = context;
        }

        public override void Update(ToDoItem toDoItem)
        {
            var entity = _context.ToDoItemList.Find(toDoItem.ToDoItemId);
            _context.Entry(entity).CurrentValues.SetValues(toDoItem);
            _context.SaveChanges();
        }

        public override void Remove(ToDoItem item)
        {
            var itemDelete = GetEntityById(item.ToDoItemId);
            base.Remove(itemDelete);
        }
    }
}