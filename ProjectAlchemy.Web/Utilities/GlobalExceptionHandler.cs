using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectAlchemy.Core.Exceptions;

namespace ProjectAlchemy.Web.Utilities;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);
        ProblemDetails problemDetails;
        
        switch (exception)
        {
            case NotFoundException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Detail = exception.Message
                };
                break;
            case InvalidArgumentException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = exception.Message
                };
                break;
            case NotAuthorizedException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Detail = exception.Message
                };
                break;
            case AlreadyExistsException:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Detail = "Already exists"
                };
                break;
            default:
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "Something went wrong."
                };
                break;
        }
        
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsync(problemDetails.Detail, cancellationToken: cancellationToken);
        return true;
    }
}
