using EcreoLibraryStandard;
using MvvmHelpers;
using System.Threading.Tasks;
using XamarinEcreo.Models;

namespace XamarinEcreo.Services
{
    public interface IUserDbService
    {
        Task AddUser(string firstname, string lastname, bool admin, Role role, string email, string phone, string address, string password);
        Task<User> GetItemAsync(string id);
        Task<UserGetModel> GetUserModelAsync(string id);
        Task<UserModel> GetUserAsync(string id);
        Task<ObservableRangeCollection<UserGetModel>> GetUsersAsync();
        Task RemoveUser(string id);
        Task<User> UpdateUserAsync(User user);
        Task<UserGetModel> UpdateUserModelAsync(UserGetModel user);
        Task<UserModel> UpdateUserModelAsync(UserModel user);
        Task<UserGetModel> UpdateUserAbsenceAsync(UserGetModel user);
    }
}