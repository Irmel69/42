using Contracts.DTOs;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BL.Services;

public sealed class UserService(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager) : IUserService
{
    public async Task<AuthenticationResult> RegisterAsync(string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            return AuthenticationResult.Failure(["Username and password are required."]);
        }

        var user = new IdentityUser { UserName = userName.Trim() };
        var result = await userManager.CreateAsync(user, password);

        return result.Succeeded
            ? AuthenticationResult.Success()
            : AuthenticationResult.Failure(result.Errors.Select(error => error.Description));
    }

    public async Task<AuthenticationResult> LoginAsync(string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            return AuthenticationResult.Failure(["Username and password are required."]);
        }

        var result = await signInManager.PasswordSignInAsync(
            userName.Trim(),
            password,
            isPersistent: true,
            lockoutOnFailure: true);

        return result.Succeeded
            ? AuthenticationResult.Success()
            : AuthenticationResult.Failure(["Invalid username or password."]);
    }

    public Task LogoutAsync() => signInManager.SignOutAsync();
}