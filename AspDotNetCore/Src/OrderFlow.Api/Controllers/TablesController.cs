using Microsoft.AspNetCore.Mvc;
using OrderFlow.Business.DTO.Tables;
using OrderFlow.Business.Interfaces.Services;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/tables")]
public class TablesController : ControllerBase
{
    private readonly ITablesService _service;
    public TablesController(ITablesService tablesService)
    {
        _service = tablesService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Table>>> GetAll()
    {
        var Mesas = await _service.GetAll();
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<bool>> GetTableById([FromQuery] int tableId)
    {
        var table = await _service.GetById(tableId);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Table>> AddPTable([FromBody] PostTable table)
    {
        // var _table = _mapper.Map<Table>(table);
        // var p = await _service.AddTable(_table);
        // return CustomResponse(p);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult<Table>> DeleteTable([FromQuery] int tableId)
    {

        var result = await _service.DeleteTable(tableId);
        return Ok();

    }

    [HttpPut]
    public async Task<ActionResult<Table>> UpdateTable([FromQuery] int tableId, [FromBody] PutTable table)
    {
        // var _table = _mapper.Map<Table>(table);
        // if (tableId != table.Id) _responseService.DivergentId(tableId, _table.Id);
        // if (HasError()) return CustomResponse(_table);
        // var result = await _service.UpdateTable(_table);
        // return CustomResponse(result);

        return Ok();
    }
}