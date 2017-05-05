using ReactiveUI;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.ViewModels;
using ReactiveUIApplication.Views;
using Splat;

namespace ReactiveUIApplication.Repositories
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public AppBootstrapper()
        {
            //TestData
            TestData.TestData.SetTestData();
            // View "StackTrace"
            Router = new RoutingState();
            // This is our main window host
            Locator.CurrentMutable.RegisterLazySingleton(() => this, typeof (IScreen));
            // Contexts
            Locator.CurrentMutable.Register(() => new UserContext(), typeof (UserContext));
            Locator.CurrentMutable.Register(() => new ToDoContext(), typeof (ToDoContext));
            // Repositories
            Locator.CurrentMutable.Register(() =>
                new UserRepository(Locator.Current.GetService<UserContext>()),
                typeof (IUserRepository));
            Locator.CurrentMutable.Register(() =>
                new ToDoRepository(Locator.Current.GetService<ToDoContext>()),
                typeof (IRepository<ToDoItem>));
            // ViewModels
            Locator.CurrentMutable.RegisterLazySingleton(() => new ShellViewModel(
                Locator.Current.GetService<IScreen>()),
                typeof (ShellViewModel));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ToDoListViewModel(
                Locator.Current.GetService<IScreen>(),
                Locator.Current.GetService<IRepository<ToDoItem>>()),
                typeof (ToDoListViewModel));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ToDoCreateViewModel(
                Locator.Current.GetService<IScreen>(),
                Locator.Current.GetService<IRepository<ToDoItem>>()),
                typeof (ToDoCreateViewModel));
            Locator.CurrentMutable.RegisterLazySingleton(() => new MenuViewModel(
                Locator.Current.GetService<IUserRepository>()),
                typeof (MenuViewModel));
            Locator.CurrentMutable.RegisterLazySingleton(() => new LoginViewModel(
                Locator.Current.GetService<IScreen>(),
                Locator.Current.GetService<IUserRepository>()),
                typeof (LoginViewModel));
            // Views
            Locator.CurrentMutable.RegisterLazySingleton(() => new ShellView(
                Locator.Current.GetService<ShellViewModel>()),
                typeof (IViewFor<ShellViewModel>));
            Locator.CurrentMutable.Register(() => new MenuView(), typeof (IViewFor<MenuViewModel>));
            Locator.CurrentMutable.Register(() => new ToDoListView(), typeof (IViewFor<ToDoListViewModel>));
            Locator.CurrentMutable.Register(() => new ToDoCreateView(), typeof (IViewFor<ToDoCreateViewModel>));
            Locator.CurrentMutable.Register(() => new LoginView(), typeof (IViewFor<LoginViewModel>));
            Locator.CurrentMutable.Register(() => new UserView(), typeof (IViewFor<UserViewModel>));
        }

        public RoutingState Router { get; }
    }
}