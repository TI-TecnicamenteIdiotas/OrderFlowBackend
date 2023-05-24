using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs.Tables;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TableController : ControllerBase
{
    private readonly TableService _tableService;

    public TableController(TableService tableService)
    {
        _tableService = tableService;
    }

    /// <summary>Creates a table</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(TableDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTable([FromBody] CreateTableDto requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var response = await _tableService.CreateTable(requestBody);
        if (response is null)
            return Problem();

        return Created(string.Empty, response);
    }

    /// <summary>Gets all tables paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(TableDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTablesPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false
    )
    {
        var response = await _tableService.GetAllTablesPaginated(page, limit, includeDeleted);
        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    /// <summary>Gets a table by id</summary>
    /// <param name="tableId"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{tableId:guid}")]
    [ProducesResponseType(typeof(TableWithRelationsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTableById([FromRoute] Guid tableId, [FromQuery] bool includeDeleted = false)
    {
        var response = await _tableService.GetTableWithRelationsById(tableId, includeDeleted);
        if (response is null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>Updates a table by id</summary>
    /// <param name="tableId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="304">Not Modified</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{tableId:guid}")]
    public async Task<IActionResult> UpdateTableById([FromRoute] Guid tableId, [FromBody] UpdateTableDto requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var (responseStatus, response) = await _tableService.UpdateTableById(tableId, requestBody);
        return StatusCode((int)responseStatus, response);
    }

    /// <summary>Deletes a table by id</summary>
    /// <param name="tableId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{tableId:guid}")]
    public async Task<IActionResult> DeleteTableById([FromRoute] Guid tableId)
    {
        var responseStatus = await _tableService.DeleteEntityById(tableId);
        return StatusCode((int)responseStatus);
    }
}