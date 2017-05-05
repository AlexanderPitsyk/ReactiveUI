using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.Common;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.Repositories;

namespace ReactiveUIApplication.ViewModels
{
    public class ToDoCreateViewModel : ViewModelBase
    {
        private const string Complite = "ToDo was Saved";

        private const string SegmentName = "Create";

        private const string Warning = "To fill necessary field: Name, Description, Proirity";

        private readonly IRepository<ToDoItem> _toDoToDoRepository;

        public ToDoCreateViewModel(IScreen screen, IRepository<ToDoItem> toDoRepository) : base(SegmentName, screen)
        {
            ToDoItem = new ToDoItem {Created = DateTime.Now, DueDate = DateTime.Now};
            _toDoToDoRepository = toDoRepository;

            SetSubmitReactiveCommand();
        }

        public ReactiveCommand<bool> Submit { get; private set; }

        public ToDoItem ToDoItem { get; }

        private void SetSubmitReactiveCommand()
        {
            Submit = ReactiveCommand.CreateAsyncTask(_ => Task.Run(() =>
            {
                if (ToDoItem.PriorityId == -1 || ToDoItem.Name.IsInvalid() || ToDoItem.Description.IsInvalid())
                {
                    return false;
                }

                _toDoToDoRepository.Insert(ToDoItem);
                return true;
            }));
            Submit.ThrownExceptions.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e => MessageBox.Show(e.Message));
            Submit.Subscribe(param => { MessageBox.Show(!param ? Warning : Complite); });
        }
    }
}