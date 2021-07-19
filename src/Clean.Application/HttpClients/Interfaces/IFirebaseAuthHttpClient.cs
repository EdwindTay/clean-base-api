using System.Threading.Tasks;
using Clean.Core.Dto.Auth;

namespace Clean.Application.HttpClients.Interfaces
{
    public interface IFirebaseAuthHttpClient
    {
        Task<TokenDto> LoginAsync(string username, string password);
    }
}
