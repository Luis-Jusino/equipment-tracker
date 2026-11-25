using EquipmentTracker.Api.Controllers;
using EquipmentTracker.Api.Data;
using EquipmentTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit.Abstractions;

namespace EquipmentTracker.Tests;

public class EquipmentControllerTests
{
    private static EquipmentController CreateController() =>
        new(new InMemoryEquipmentRepository());

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenItemDoesNotExist()
    {
        var result = await CreateController().GetById(99);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ThenGetById_ReturnsTheItem()
    {
        var controller = CreateController();
        await controller.Create(new Equipment { Name = "Radar Unit", SerialNumber = "RU-1001"});

        var result = await controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var item = Assert.IsType<Equipment>(ok.Value);
        Assert.Equal("Radar Unit", item.Name);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        var result = await CreateController().Update(1, new Equipment { Id = 2, Name = "X", SerialNumber = "Y"});
        Assert.IsType<BadRequestObjectResult>(result);

    }

    [Fact]
    public async Task Delete_RemovesTheItem()
    {
        var controller = CreateController();
        await controller.Create(new Equipment {Name = "A", SerialNumber = "B"});

        var deleteResult = await controller.Delete(1);
        var getResult = await controller.GetById(1);

        Assert.IsType<NoContentResult>(deleteResult);
        Assert.IsType<NotFoundResult>(getResult.Result);

    }

    [Fact]
    public async Task GetAll_FiltersByStatus()
    {
        var controller = CreateController();
        await controller.Create(new Equipment { Name = "A", SerialNumber = "1", Status = EquipmentStatus.NeedsMaintenance});
        await controller.Create(new Equipment { Name = "B", SerialNumber = "2"});

        var result = await controller.GetAll(EquipmentStatus.NeedsMaintenance);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var items = Assert.IsAssignableFrom<IEnumerable<Equipment>>(ok.Value);
        Assert.Single(items);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenItemExists()
    {
        var controller = CreateController();
        await controller.Create(new Equipment { Name = "RF Unit", SerialNumber = "3"});
        
        var result = await controller.Update(1, new Equipment { Id = 1, Name = "RF Unit", SerialNumber = "3" , Status = EquipmentStatus.OutOfService});
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenItemDoesNotExits()
    {
        var result = await CreateController().Update(1, new Equipment {Id = 1, Name = "PS Computer", SerialNumber = "3", Status = EquipmentStatus.OutOfService});
        Assert.IsType<NotFoundResult>(result);
    }
}