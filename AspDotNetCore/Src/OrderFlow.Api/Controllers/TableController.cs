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

    [HttpGet]
    public async Task<IActionResult> GetAllPaginated()
    {
        var tables = await _tableService.GetAllPaginated();
        if (!tables.Any())
            return NoContent();

        return Ok(tables);
    }

    [HttpGet("{tableId:guid}")]
    public async Task<IActionResult> GetTableById([FromRoute] Guid tableId)
    {
        var table = await _tableService.GetTableById(tableId);
        if (table is null)
            return NotFound();

        return Ok(table);
    }

    private readonly record struct AddTableResponseWrapper(Guid TableId);

    [HttpPost]
    public async Task<IActionResult> AddTable([FromBody] PostTable requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var tableId = await _tableService.AddTable(requestBody);
        if (tableId is null)
            return Problem();

        return Created(string.Empty, new AddTableResponseWrapper
        {
            TableId = tableId.Value
        });
    }

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