using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.ViewModels;

namespace ReactiveUIApplication.Views
{
    public partial class ToDoListView : IViewFor<ToDoListViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof (ToDoListViewModel),
                typeof (ToDoListView),
                new PropertyMetadata(null));

        public ToDoListView()
        {
            InitializeComponent();
            this.OneWayBind(ViewModel, vm => vm.ItemList, v => v.DataGrid.ItemsSource);
            this.BindCommand(ViewModel, vm => vm.Submit, v => v.Submit);
            this.BindCommand(ViewModel, vm => vm.Delete, v => v.Delete);

            this.WhenAnyValue(e => e.DataGrid.SelectedValue).BindTo(this, e => e.ViewModel.SelectedToDoItem);
            this.Bind(ViewModel, vm => vm.SelectedToDoItem, v => v.DataGrid.SelectedValue);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ToDoListViewModel) value; }
        }

        public ToDoListViewModel ViewModel
        {
            get { return (ToDoListViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}