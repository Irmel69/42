using API.Data;
using BL.Services;

namespace API.Tests;

public sealed class BusinessServiceTests
{
    [Fact]
    public async Task TaskTemplateCrud_roundTripsData()
    {
        var service = new TaskTemplateService(new InMemoryTemplateDataAccess());

        Assert.True(await service.CreateNewTaskTemplate("Inspection", "Inspect the equipment."));
        var templates = await service.GetAllTaskTemplates();

        Assert.Single(templates);
        Assert.Equal("Inspection", templates[0].Name);
        Assert.True(await service.UpdateTaskTemplate(templates[0].TaskID, "Updated", "Updated description."));
        Assert.True(await service.DeleteTaskTemplate(templates[0].TaskID));
        Assert.Empty(await service.GetAllTaskTemplates());
    }

    [Fact]
    public async Task ProtocolService_rejectsInvalidInput()
    {
        var service = new ProtocolService(new InMemoryProtocolDataAccess(), new TaskTemplateService(new InMemoryTemplateDataAccess()));

        Assert.False(await service.CreateNewProtocolEntry("", "A note"));
        Assert.False(await service.CreateNewProtocolEntry("task-1", ""));
        Assert.Null(await service.GetProtocolEntry("missing"));
    }
}
