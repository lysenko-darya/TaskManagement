namespace TaskManagement.Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException() : base("Entity is not found") { }
    public NotFoundException(string entity, long id) : base($"{entity} with Id:'{id}' is not found") { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception inner) : base(message, inner) { }
}