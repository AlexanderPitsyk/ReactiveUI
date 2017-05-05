using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.ViewModels;

namespace ReactiveUIApplication.Views
{
    public partial class UserView : IViewFor<UserViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof (UserViewModel),
                typeof (UserView),
                new PropertyMetadata(null));

        public UserView()
        {
            InitializeComponent();
            this.Bind(ViewModel, vm => vm.Name, v => v.UserName.Text);
            this.OneWayBind(ViewModel, vm => vm.Header, v => v.Header.Text);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (UserViewModel) value; }
        }

        public UserViewModel ViewModel
        {
            get { return (UserViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}