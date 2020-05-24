using System.Threading.Tasks;
using Authorization.Entities.Entities;
using Authorization.Models;

namespace Authorization.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> LoginAsync(string userName, string password);

        Task<User> RegisterUser(Login loginDetails);
    }
}