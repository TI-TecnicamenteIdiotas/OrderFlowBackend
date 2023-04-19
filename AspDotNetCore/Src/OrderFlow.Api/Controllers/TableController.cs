using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> GetAllPaginated()
    {
        var tables = await _tableService.GetAllPaginated();
        if (!tables.Any())
            return NoContent();

        return Ok(tables);
    }

    [HttpGet("{tableId:guid}")]
    public async Task<ActionResult> GetTableById([FromRoute] Guid tableId)
    {
        var table = await _tableService.GetTableById(tableId);
        if (table is null)
            return NotFound();

        return Ok(table);
    }

    private readonly record struct AddTableResponseWrapper(Guid TableId);

    [HttpPost]
    public async Task<ActionResult> AddTable([FromBody] PostTable requestBody)
    {
        var tableId = await _tableService.AddTable(requestBody);
        if (tableId is null)
            return Problem();

        return Created(string.Empty, new AddTableResponseWrapper
        {
            TableId = tableId.Value
        });
    }

    [HttpDelete("{tableId:guid}")]
    public async Task<ActionResult> DeleteTableById([FromRoute] Guid tableId)
    {
        var wasDeleted = await _tableService.DeleteTableById(tableId);
        if (!wasDeleted)
            return Problem();

        return Ok();
    }

    [HttpPut("{tableId:guid}")]
    public async Task<ActionResult> UpdateTable([FromRoute] Guid tableId, [FromBody] PutTable requestBody)
    {
        var wasUpdated = await _tableService.UpdateTableById(tableId, requestBody);
        if (!wasUpdated)
            return Problem();

        return Ok();
    }
}