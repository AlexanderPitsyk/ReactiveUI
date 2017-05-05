using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUIApplication.Common;
using ReactiveUIApplication.Models;

namespace ReactiveUIApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> Login(string userName, string unsecurePassword)
        {
            if (userName.IsInvalid())
            {
                throw new ArgumentException("userName");
            }

            if (unsecurePassword.IsInvalid())
            {
                throw new ArgumentException("Incorect pasword");
            }

            var result = _userContext.Users.Where(e => e.Name == userName)
                .Join(
                    _userContext.Credentials.Where(e => e.Password == unsecurePassword),
                    user => user.Id,
                    credential => credential.User.Id,
                    (user, credential) => user)
                .FirstOrDefault();

            if (result == null)
            {
                throw new Exception("Not found.");
            }

            return await Task.FromResult(result);
        }

        public async Task<IList<Menu>> GetMenuByUser(User user)
        {
            if (user == null)
            {
                return new List<Menu> {new Menu(MenuOption.Login)};
            }

            return await Task.FromResult(new List<Menu>
            {
                new Menu(MenuOption.Login),
                new Menu(MenuOption.User),
                new Menu(MenuOption.List),
                new Menu(MenuOption.Create)
            });
        }
    }
}