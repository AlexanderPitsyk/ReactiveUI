using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using ReactiveUIApplication.Common;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.Repositories;

namespace ReactiveUIApplication.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private const string SegmentName = "Login";

        private readonly IUserRepository _userRepository;

        private PasswordBox _password;

        private User _user;

        private string _userName;

        public LoginViewModel(IScreen screen, IUserRepository userRepository) : base(SegmentName, screen)
        {
            _userRepository = userRepository;
            var canLogin = this.WhenAny(m => m.UserName, m => m.Password, (user, password) => user.Value.IsValid());

            Login = ReactiveCommand.CreateAsyncTask(canLogin, _ => _userRepository.Login(UserName, Password.Password));
            Login.ObserveOn(RxApp.MainThreadScheduler).Subscribe(user =>
            {
                User = user;
                HostScreen.Router.Navigate.Execute(new UserViewModel(HostScreen, user));
            });
            Login.ThrownExceptions.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e => MessageBox.Show(e.Message));
        }

        public ReactiveCommand<User> Login { get; protected set; }

        public User User
        {
            get { return _user; }
            set { this.RaiseAndSetIfChanged(ref _user, value); }
        }

        public string UserName
        {
            get { return _userName; }
            set { this.RaiseAndSetIfChanged(ref _userName, value); }
        }

        public PasswordBox Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }
    }
}