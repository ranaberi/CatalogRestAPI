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
        var controller = new ItemsControllerTests(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);

    }
}