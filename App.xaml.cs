using System.Windows;
using ReactiveUI;
using ReactiveUIApplication.Repositories;
using ReactiveUIApplication.ViewModels;
using ReactiveUIApplication.Views;
using Splat;

namespace ReactiveUIApplication
{
    public partial class App
    {
        public static AppBootstrapper Bootstrapper;
        public static ShellView ShellView;

        public App()
        {
            Bootstrapper = new AppBootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShellView = (ShellView) Locator.Current.GetService<IViewFor<ShellViewModel>>();
            ShellView.Show();
        }
    }
}