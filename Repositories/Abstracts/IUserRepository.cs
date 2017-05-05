using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUIApplication.Models;

namespace ReactiveUIApplication.Repositories
{
    public interface IUserRepository
    {
        Task<User> Login(string userName, string unsecurePassword);

        Task<IList<Menu>> GetMenuByUser(User user);
    }
}