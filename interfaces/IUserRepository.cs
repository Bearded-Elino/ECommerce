using System.Threading.Tasks;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);

        public Task<User> Login(LoginViewModel loginViewModel);

    }
}