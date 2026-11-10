using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CargoTrack.Application.Exceptions;

namespace CargoTrack.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger=logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException=> (StatusCodes.Status404NotFound, "Kargo bulunamadı"),
             ConflictException => (StatusCodes.Status409Conflict, "İşlem yapılamadı"),
            _ => (StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu")
        };

        if(statusCode==StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Beklenmeyen hata: {Path}",httpContext.Request.Path);

        
        httpContext.Response.StatusCode=statusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status=statusCode,
            Title=title,
            Detail=statusCode==StatusCodes.Status500InternalServerError ? null : exception.Message}, cancellationToken);

        return true;

        











    }















}