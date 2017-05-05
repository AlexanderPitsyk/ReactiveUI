using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.Common;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.Repositories;

namespace ReactiveUIApplication.ViewModels
{
    public class MenuViewModel : ReactiveObject
    {
        private readonly IUserRepository _userRepository;

        private MenuOptionViewModel _selectedOption;

        private User _user;

        public MenuViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            //Create Navigate Menu
            Menu = new ReactiveList<MenuOptionViewModel>();
            var canLoadMenu = this.WhenAny(m => m.User, user => user.Value != null);
            LoadMenu = ReactiveCommand.CreateAsyncTask(canLoadMenu, _ => _userRepository.GetMenuByUser(User));
            LoadMenu.ObserveOn(RxApp.MainThreadScheduler).Subscribe(menu =>
            {
                Menu.Clear();
                menu.ForEach(option => Menu.Add(new MenuOptionViewModel(option)));
            });

            LoadMenu.ThrownExceptions.Subscribe(ex =>
            {
                Menu.Clear();
                MessageBox.Show(ex.Message);
            });
            this.WhenAnyValue(m => m.User).InvokeCommand(this, vm => vm.LoadMenu);
        }

        public ReactiveCommand<IList<Menu>> LoadMenu { get; protected set; }

        public ReactiveList<MenuOptionViewModel> Menu { get; protected set; }

        public User User
        {
            get { return _user; }
            set { this.RaiseAndSetIfChanged(ref _user, value); }
        }

        public MenuOptionViewModel SelectedOption
        {
            get { return _selectedOption; }
            set { this.RaiseAndSetIfChanged(ref _selectedOption, value); }
        }

        public string GetResult(string firstValue, string secondValue)
        {
            return (int.Parse(firstValue) + int.Parse(secondValue)).ToString();
        }
    }
}