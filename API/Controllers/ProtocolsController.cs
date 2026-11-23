using Contracts.DTOs;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/protocols")]
public sealed class ProtocolsController(IProtocolService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProtocolEntryDTO>>> GetAll() => Ok(await service.GetAllProtocolEntries());

    [HttpGet("{protocolId}")]
    public async Task<ActionResult<ProtocolEntryDTO>> Get(string protocolId)
    {
        var entry = await service.GetProtocolEntry(protocolId);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProtocolRequest request)
    {
        return await service.CreateNewProtocolEntry(request.TaskID, request.Note)
            ? NoContent()
            : BadRequest("TaskID and Note are required.");
    }

    [HttpPut("{protocolId}")]
    public async Task<IActionResult> Update(string protocolId, UpdateProtocolRequest request)
    {
        return await service.UpdateProtocolEntry(protocolId, request.Note)
            ? NoContent()
            : request.Note is null ? BadRequest("Note is required.") : NotFound();
    }

    [HttpDelete("{protocolId}")]
    public async Task<IActionResult> Delete(string protocolId) =>
        await service.DeleteProtocolEntry(protocolId) ? NoContent() : NotFound();
}

public sealed record CreateProtocolRequest(string TaskID, string Note);
public sealed record UpdateProtocolRequest(string Note);
