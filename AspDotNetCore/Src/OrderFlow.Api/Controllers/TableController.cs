using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Helpers;
using OrderFlow.Contracts.DTOs.Tables;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    /// <summary>Gets all tables paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetTable[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTablesPaginated([FromQuery] int page = 0, [FromQuery] int limit = 12)
    {
        var tables = await _tableService.GetAllTablesPaginated();
        if (!tables.Any())
            return NoContent();

        return Ok(tables);
    }

    /// <summary>Gets a table by id</summary>
    /// <param name="tableId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{tableId:guid}")]
    public async Task<IActionResult> GetTableById([FromRoute] Guid tableId)
    {
        var table = await _tableService.GetTableById(tableId);
        if (table is null)
            return NotFound();

        return Ok(table);
    }

    private readonly record struct AddTableResponseWrapper(Guid TableId);

    /// <summary>Creates a category</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(AddTableResponseWrapper), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTable([FromBody] PostTable requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var tableId = await _tableService.CreateTable(requestBody);
        if (tableId is null)
            return Problem();

        return Created(string.Empty, new AddTableResponseWrapper
        {
            TableId = tableId.Value
        });
    }

    /// <summary>Deletes a table by id</summary>
    /// <param name="tableId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{tableId:guid}")]
    public async Task<IActionResult> DeleteTableById([FromRoute] Guid tableId)
    {
        var tableExists = await _tableService.GetTableById(tableId);
        if (tableExists is null)
            return NotFound();

        var wasTableDeleted = await _tableService.DeleteTableById(tableId);
        if (!wasTableDeleted)
            return Problem();

        return Ok();
    }

    /// <summary>Updates a table by id</summary>
    /// <param name="tableId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{tableId:guid}")]
    public async Task<IActionResult> UpdateTableById([FromRoute] Guid tableId, [FromBody] PutTable requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var tableExists = await _tableService.GetTableById(tableId);
        if (tableExists is null)
            return NotFound();

        var wasTableUpdated = await _tableService.UpdateTableById(tableId, requestBody);
        if (!wasTableUpdated)
            return Problem();

        return Ok();
    }
}