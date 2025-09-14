using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger(typeof(ExceptionMiddleware));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e, statusCode: HttpStatusCode.NotFound);
        }
        catch (AccessDeniedException e)
        {
            await HandleExceptionAsync(context, e, statusCode: HttpStatusCode.Forbidden);
        }
        catch (DomainException e)
        {
            await HandleExceptionAsync(context, e);
        }
        catch (DbUpdateConcurrencyException e)
        {
            await HandleExceptionAsync(context, e, "Не удалось сохранить данные.");
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e, "Что-то пошло не так. Пожалуйста, попробуйте еще раз или свяжитесь с поддержкой.");
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception e, string? message = null, string? details = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        var logMessage = e switch
        {
            DomainException be => $"Message : {be.Message ?? string.Empty}.",
            Exception => e.Message,
        };
        _logger.LogError(e, logMessage);

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsJsonAsync(new ExceptionResult()
        {
            Message = message ?? e.Message,
            DevMessage = e.Message,
            StackTrace = e.StackTrace,
            Details = details
        });
    }

    private readonly record struct ExceptionResult
    {
        public required string Message { get; init; }
        public required string? DevMessage { get; init; }
        public required string? StackTrace { get; init; }
        public required string? Details { get; init; }
    }
}