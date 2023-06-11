using System.Net;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs;
using NimbleFlow.Contracts.DTOs.Tables;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TableController : ControllerBase
{
    private const int MaxAccountableLength = 256;
    private readonly TableService _tableService;
    private readonly HubService? _hubService;

    public TableController(TableService tableService, HubService? hubService)
    {
        _tableService = tableService;
        _hubService = hubService;
    }

    /// <summary>Creates a table</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="409">Conflict</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(TableDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTable([FromBody] CreateTableDto requestBody)
    {
        if (requestBody.Accountable.Length > MaxAccountableLength)
            return BadRequest($"{nameof(requestBody.Accountable)} must be under {MaxAccountableLength + 1} characters");

        var (responseStatus, response) = await _tableService.Create(requestBody);
        switch (responseStatus)
        {
            case HttpStatusCode.Created when response is not null:
            {
                if (_hubService is not null)
                    await _hubService.PublishTableCreatedAsync(response);
                return Created(string.Empty, response);
            }
            case HttpStatusCode.Conflict:
                return Conflict();
            default:
                return Problem();
        }
    }

    /// <summary>Gets all tables paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedDto<TableDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTablesPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false
    )
    {
        var (totalAmount, tables) = await _tableService.GetAllPaginated(page, limit, includeDeleted);
        if (totalAmount == 0)
            return NoContent();

        var response = new PaginatedDto<TableDto>(totalAmount, tables);
        return Ok(response);
    }

    /// <summary>Gets a table by id</summary>
    /// <param name="tableId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{tableId:guid}")]
    [ProducesResponseType(typeof(TableDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTableById([FromRoute] Guid tableId)
    {
        var response = await _tableService.GetById(tableId);
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
    /// <response code="409">Conflict</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{tableId:guid}")]
    public async Task<IActionResult> UpdateTableById([FromRoute] Guid tableId, [FromBody] UpdateTableDto requestBody)
    {
        if (requestBody.Accountable?.Length > MaxAccountableLength)
            return BadRequest($"{nameof(requestBody.Accountable)} must be under {MaxAccountableLength + 1} characters");

        var responseStatus = await _tableService.UpdateTableById(tableId, requestBody);
        return responseStatus switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotModified => StatusCode((int)HttpStatusCode.NotModified),
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.Conflict => Conflict(),
            _ => Problem()
        };
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
        return responseStatus switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotFound => NotFound(),
            _ => Problem()
        };
    }
}