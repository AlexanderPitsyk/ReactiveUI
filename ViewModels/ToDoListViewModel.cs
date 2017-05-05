using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.Common;
using ReactiveUIApplication.Enums;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.Repositories;

namespace ReactiveUIApplication.ViewModels
{
    public class ToDoListViewModel : ViewModelBase
    {
        private const string SegmentName = "List";

        private readonly IRepository<ToDoItem> _toDoToDoRepository;

        private ToDoItem _selectedToDoItem;

        public ToDoListViewModel(IScreen screen, IRepository<ToDoItem> toDoRepository) : base(SegmentName, screen)
        {
            ItemList = new ObservableCollection<ToDoItem>();
            _toDoToDoRepository = toDoRepository;

            SetSubmitReactiveCommand();
            SetDeleteReactiveCommand();
            GetToDoList(ItemList);
        }

        public ToDoItem SelectedToDoItem
        {
            get { return _selectedToDoItem; }
            set { this.RaiseAndSetIfChanged(ref _selectedToDoItem, value); }
        }

        public ObservableCollection<ToDoItem> ItemList { get; }

        public ReactiveCommand<bool> Submit { get; private set; }

        public ReactiveCommand<bool> Delete { get; private set; }

        private void GetToDoList(ObservableCollection<ToDoItem> toDoItemList)
        {
            _toDoToDoRepository.GetEntityList().ForEach(toDoItemList.Add);
            SelectedToDoItem = ItemList.FirstOrDefault();
            SetPriorityOptionList(toDoItemList);
        }

        private void SetPriorityOptionList(ObservableCollection<ToDoItem> toDoItemList)
        {
            var priorityOptionList = new ObservableCollection<PriorityOption>();
            Enum.GetValues(typeof (PriorityOption)).ForEach<PriorityOption>(priorityOptionList.Add);
            toDoItemList.ForEach(x => x.Collection = priorityOptionList);
        }

        private void SetDeleteReactiveCommand()
        {
            this.WhenAny(vm => vm.SelectedToDoItem, item => item != null);
            Delete = ReactiveCommand.CreateAsyncTask(
                this.WhenAny(vm => vm.SelectedToDoItem, item => item != null),
                _ => Task.Run(() =>
                {
                    //avoid NullRefferenceException
                    if (SelectedToDoItem == null)
                    {
                        return false;
                    }

                    _toDoToDoRepository.Remove(SelectedToDoItem);
                    return true;
                }));
            Delete.ThrownExceptions.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e => MessageBox.Show(e.Message));
            Delete.Subscribe(e =>
            {
                if (!e)
                {
                    return;
                }

                //Reload items
                ItemList.Clear();
                GetToDoList(ItemList);
                SelectedToDoItem = ItemList.FirstOrDefault();
            });
        }

        private void SetSubmitReactiveCommand()
        {
            Submit = ReactiveCommand.CreateAsyncTask(_ => Task.Run(() =>
            {
                ItemList.ForEach(SubmitToDoItem);
                return true;
            }));
            Submit.ThrownExceptions.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e => MessageBox.Show(e.Message));
        }

        private void SubmitToDoItem(ToDoItem item)
        {
            _toDoToDoRepository.Update(item);
        }
    }
}