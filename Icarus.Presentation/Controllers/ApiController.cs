using MediatR;
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
}
