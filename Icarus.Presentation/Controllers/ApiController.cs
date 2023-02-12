using Icarus.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Icarus.Presentation.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    protected ApiController(ISender sender)
    {
        _sender = sender;
    }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
            BadRequest(
            CreateProblemDetails(
                "Validation error",
                StatusCodes.Status400BadRequest,
                result.Error,
                validationResult.Errors)),
            _ =>
            BadRequest(
            CreateProblemDetails(
                "Validation error",
                StatusCodes.Status400BadRequest,
                result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
    {
        return new() { Title = title, Type = error.Code, Status = status, Detail = error.Message, Extensions = { { nameof(errors), errors } } };
    }
}
