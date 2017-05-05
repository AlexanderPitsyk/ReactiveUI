using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.ViewModels;

namespace ReactiveUIApplication.Views
{
    public partial class ShellView : IViewFor<ShellViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof (ShellViewModel),
                typeof (ShellView),
                new PropertyMetadata(null));

        public ShellView(ShellViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            this.OneWayBind(ViewModel, vm => vm.MenuViewModel, v => v.Menu.ViewModel);
            this.Bind(ViewModel, vm => vm.HostScreen.Router, v => v.ContentView.Router);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ShellViewModel) value; }
        }

        public ShellViewModel ViewModel
        {
            get { return (ShellViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}