using Contracts.DTOs;

namespace Contracts.Interfaces;

public interface IUserService
{
    Task<AuthenticationResult> RegisterAsync(string userName, string password);
    Task<AuthenticationResult> LoginAsync(string userName, string password);
    Task LogoutAsync();
}
