using System;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.Repositories;
using Splat;

namespace ReactiveUIApplication.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private const string SegmentName = "Shell";

        private User _user;

        public ShellViewModel(IScreen screen) : base(SegmentName, screen)
        {
            MenuViewModel = Locator.Current.GetService<MenuViewModel>();
            LoginViewModel = Locator.Current.GetService<LoginViewModel>();
            SetInterfaceByUser();
            SetInterface();
            HostScreen.Router.Navigate.Execute(LoginViewModel);
            this.WhenAnyValue(vm => vm.LoginViewModel.User).Subscribe(user => User = user);
        }

        public User User
        {
            get { return _user; }
            set { this.RaiseAndSetIfChanged(ref _user, value); }
        }

        public MenuViewModel MenuViewModel { get; }

        public LoginViewModel LoginViewModel { get; }

        private void SetInterfaceByUser()
        {
            this.WhenAnyValue(vm => vm.User).Subscribe(user => MenuViewModel.User = user);
        }

        private void SetInterface()
        {
            this.WhenAny(vm => vm.MenuViewModel.SelectedOption, mvm => mvm.Value)
                .Where(e => e != null)
                .Subscribe(e =>
                {
                    if (e.Model.Option.ToString() == HostScreen.Router.NavigationStack.Last().UrlPathSegment)
                    {
                        return;
                    }

                    switch (e.Model.Option)
                    {
                        case MenuOption.Login:
                            HostScreen.Router.Navigate.Execute(LoginViewModel);
                            break;
                        case MenuOption.User:
                            HostScreen.Router.Navigate.Execute(
                                new UserViewModel(Locator.Current.GetService<IScreen>(), User));
                            break;
                        case MenuOption.List:
                            HostScreen.Router.Navigate.Execute(new ToDoListViewModel(
                                Locator.Current.GetService<IScreen>(),
                                Locator.Current.GetService<IRepository<ToDoItem>>()));
                            break;
                        case MenuOption.Create:
                            HostScreen.Router.Navigate.Execute(Locator.Current.GetService<ToDoCreateViewModel>());
                            break;
                    }
                });
        }
    }
}