using Contracts.DTOs;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/task-templates")]
public sealed class TaskTemplatesController(ITaskTemplateService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskTemplateDTO>>> GetAll() => Ok(await service.GetAllTaskTemplates());

    [HttpGet("{taskId}")]
    public async Task<ActionResult<TaskTemplateDTO>> Get(string taskId)
    {
        var template = await service.GetTaskTemplate(taskId);
        return template is null ? NotFound() : Ok(template);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskTemplateRequest request)
    {
        return await service.CreateNewTaskTemplate(request.Name, request.Description)
            ? NoContent()
            : BadRequest("Name and Description are required.");
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> Update(string taskId, UpdateTaskTemplateRequest request)
    {
        return await service.UpdateTaskTemplate(taskId, request.Name, request.Description)
            ? NoContent()
            : string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Description)
                ? BadRequest("Name and Description are required.")
                : NotFound();
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> Delete(string taskId) =>
        await service.DeleteTaskTemplate(taskId) ? NoContent() : NotFound();
}

public sealed record CreateTaskTemplateRequest(string Name, string Description);
public sealed record UpdateTaskTemplateRequest(string Name, string Description);
