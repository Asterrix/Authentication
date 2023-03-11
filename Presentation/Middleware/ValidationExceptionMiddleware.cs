using Application.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace Presentation.Middleware;

public class ValidationExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ValidationExceptionMiddleware> _logger;

    public ValidationExceptionMiddleware(ILogger<ValidationExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var problemDetails = new ErrorModel
            {
                Type = "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.1",
                Title = "Validation Error",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = "Validation error occurred"
            };

            foreach (var error in e.Errors)
            {
                problemDetails.ErrorList.Add(error.ErrorMessage);
            }

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, jsonSettings));
        }
    }
}