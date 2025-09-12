using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Settings;
using TaskManagement.Domain.Abstractions;

namespace TaskManagement.Infrastructure.Services;

internal class AuthenticationService(IOptions<AuthSettings> settings, IHttpContextAccessor httpContextAccessor) : IAuthenticationService
{
    private readonly AuthSettings _settings = settings.Value;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public string GenerateNewToken(string userName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: new List<Claim>([
                new Claim(ClaimTypes.NameIdentifier, userName)
            ]),
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMin),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string? GetSubjectFromUser()
    {
        return _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
    }
}
