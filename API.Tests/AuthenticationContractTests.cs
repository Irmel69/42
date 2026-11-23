using Contracts.DTOs;

namespace API.Tests;

public sealed class AuthenticationContractTests
{
    [Fact]
    public void Failure_removes_empty_errors()
    {
        var result = AuthenticationResult.Failure(["Invalid credentials.", "", "  "]);

        Assert.False(result.Succeeded);
        Assert.Equal(["Invalid credentials."], result.Errors);
    }

    [Fact]
    public void Success_has_no_errors()
    {
        var result = AuthenticationResult.Success();

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
    }
}
