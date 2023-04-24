using Microsoft.AspNetCore.Mvc;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController : ControllerBase
{
    /// <summary>Checks if service is healthy</summary>
    /// <response code="200">Ok</response>
    [HttpGet]
    public Task<IActionResult> GetHealthCheckStatus() => Task.FromResult<IActionResult>(Ok());
}