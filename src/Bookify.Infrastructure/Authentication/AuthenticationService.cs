using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Users;
using System.Net.Http.Json;

namespace Bookify.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        return null;
    }
}