namespace Contracts.DTOs;

public sealed class AuthenticationResult
{
    public bool Succeeded { get; init; }
    public IReadOnlyList<string> Errors { get; init; } = [];

    public static AuthenticationResult Success() => new() { Succeeded = true };

    public static AuthenticationResult Failure(IEnumerable<string> errors) => new()
    {
        Errors = errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToArray()
    };
}