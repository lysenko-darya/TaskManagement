namespace TaskManagement.Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException() : base("Entity not found") { }
    public NotFoundException(string entity, long id) : base($"{entity} with id:{id} not found") { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception inner) : base(message, inner) { }
}