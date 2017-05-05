using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.ViewModels;

namespace ReactiveUIApplication.Views
{
    public partial class MenuView : IViewFor<MenuViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof (MenuViewModel),
                typeof (MenuView),
                new PropertyMetadata(null));

        public MenuView()
        {
            InitializeComponent();
            this.OneWayBind(ViewModel, vm => vm.Menu, v => v.SideMenu.ItemsSource);
            this.Bind(ViewModel, vm => vm.SelectedOption, v => v.SideMenu.SelectedValue);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MenuViewModel) value; }
        }

        public MenuViewModel ViewModel
        {
            get { return (MenuViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}