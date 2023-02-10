using System.Threading.Tasks;
using XamarinEcreo.Models;

namespace XamarinEcreo.Services
{
    public interface IAdminstratorService
    {
        Task<UserPersonalInfoModel> GetUserInfoAsync(string id);
        Task<UserPersonalInfoModel> GetUserPasswordAsync(string id);
        Task<UserGetModel> GetUserRoleAsync(string id);
    }
}