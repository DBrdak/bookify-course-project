using System.Security.Claims;
using Bookify.Application.Abstractions.Authentication;

namespace Bookify.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    public string IdentityId { get; }
}