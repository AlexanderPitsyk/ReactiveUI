using System;
using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.Enums;
using ReactiveUIApplication.ViewModels;

namespace ReactiveUIApplication.Views
{
    public partial class ToDoCreateView : IViewFor<ToDoCreateViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof (ToDoCreateViewModel),
                typeof (ToDoCreateView),
                new PropertyMetadata(null));

        public ToDoCreateView()
        {
            InitializeComponent();

            this.Bind(ViewModel, vm => vm.ToDoItem.Name, v => v.Name.Text);
            this.Bind(ViewModel, vm => vm.ToDoItem.Description, v => v.Description.Text);

            Priority.ItemsSource = Enum.GetValues(typeof (PriorityOption));
            this.WhenAnyValue(e => e.Priority.SelectedIndex).BindTo(this, e => e.ViewModel.ToDoItem.PriorityId);
            this.Bind(ViewModel, vm => vm.ToDoItem.PriorityId, v => v.Priority.SelectedIndex);

            this.Bind(ViewModel, vm => vm.ToDoItem.DueDate, v => v.DueDate.SelectedDate);
            this.BindCommand(ViewModel, vm => vm.Submit, v => v.Submit);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ToDoCreateViewModel) value; }
        }

        public ToDoCreateViewModel ViewModel
        {
            get { return (ToDoCreateViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}