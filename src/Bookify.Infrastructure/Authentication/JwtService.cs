using System.Net.Http.Json;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Abstractions;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    public async Task<Result<string>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        return null;
    }
}