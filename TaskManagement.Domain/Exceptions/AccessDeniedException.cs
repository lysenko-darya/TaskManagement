namespace TaskManagement.Domain.Exceptions;

public class AccessDeniedException : DomainException
{
    public AccessDeniedException() : base("Access denied") { }
    public AccessDeniedException(string message) : base(message) { }
    public AccessDeniedException(string message, Exception inner) : base(message, inner) { }
}