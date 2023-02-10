using System.Security.Claims;
using System.Threading.Tasks;

namespace XamarinEcreo.Services
{
    public interface IAuthenticationService
    {
        ClaimsPrincipal GetPrincipalFromToken(string token);
        Task<ClaimsPrincipal> SignIn(string username, string password);
    }
}