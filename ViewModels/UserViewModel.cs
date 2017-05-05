using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.Common;
using ReactiveUIApplication.Models;

namespace ReactiveUIApplication.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private const string SegmentName = "User";

        private string _message;

        private string _name;

        private User _user;

        public UserViewModel(IScreen screen, User user) : base(SegmentName, screen)
        {
            this.WhenAnyValue(e => e.User).Where(e => e != null).Subscribe(u => { Name = u.Name; });
            User = user;

            this.WhenAnyValue(e => e.Name).Subscribe(name => User.Name = name);

            UserError.RegisterHandler(async error =>
            {
                // Don't block app
                await Task.Delay(1);
                var message = new StringBuilder();
                var hasRecoveryOptions = error.ErrorCauseOrResolution.IsValid();
                if (hasRecoveryOptions)
                    message.AppendLine(error.ErrorCauseOrResolution);
                message.AppendLine(error.ErrorMessage);
                var result = MessageBox.Show(message.ToString(), "Alert!",
                    hasRecoveryOptions ? MessageBoxButton.YesNo : MessageBoxButton.OK);

                return hasRecoveryOptions && result == MessageBoxResult.Yes
                    ? RecoveryOptionResult.RetryOperation
                    : RecoveryOptionResult.CancelOperation;
            });
        }

        public User User
        {
            get { return _user; }
            set { this.RaiseAndSetIfChanged(ref _user, value); }
        }

        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        public string Header => $"User: {Name}";

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }
    }
}