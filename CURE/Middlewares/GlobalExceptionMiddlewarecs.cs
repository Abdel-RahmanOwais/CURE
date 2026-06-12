using CURE.API.Models;
using CURE.Application.Exceptions;
using System.Text.Json;

namespace CURE.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.StatusCode = 404;
                response.Message = exception.Message;
                break;

            case BadRequestException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = 400;
                response.Message = exception.Message;
                break;

            case UnauthorizedException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.StatusCode = 401;
                response.Message = exception.Message;
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = 500;
                response.Message = "An unexpected error occurred.";
                response.Details = exception.Message;
                break;
        }

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}