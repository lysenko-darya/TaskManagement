using System.Security.Claims;

namespace TaskManagement.Domain.Abstractions;
public interface IAuthenticationService
{
    string GenerateNewToken(string userName);
    string? GetSubjectFromUser();
}
