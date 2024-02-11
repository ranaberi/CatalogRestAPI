using Moq;
using Microsoft.Extensions.Logging;
using CatalogApi.Controllers;
using CatalogApi.Repositories;
using CatalogApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;
namespace CatalogUnitTests;

public class ItemsControllerTests
{
    [Fact]
    public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
    {
        //Arrange
        var repositoryStub = new Mock<IItemsRepository>();
        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

        var loggerStub = new Mock<ILogger<ItemsController>>();
        var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);

    }
}