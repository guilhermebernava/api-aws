using Aws.Services.Services;
using Domain.Repositories;
using Moq;

namespace Aws.Services.Tests.Services;

public class ProductDeleteServicesTests
{
    [Fact]
    public async Task ItShouldDeleteProduct()
    {
        var ProductId = Guid.NewGuid();
        var ProductRepository = new Mock<IProductRepository>();
        ProductRepository
            .Setup(repository => repository.DeleteByIdAsync(ProductId, CancellationToken.None))
            .ReturnsAsync(true);

        var ProductDeleteServices = new ProductDeleteServices(ProductRepository.Object);

        var result = await ProductDeleteServices.Execute(ProductId, CancellationToken.None);

        Assert.True(result);
        ProductRepository.Verify(repository => repository.DeleteByIdAsync(ProductId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ItShouldNotDeleteProduct()
    {
        var ProductId = Guid.NewGuid();
        var ProductRepository = new Mock<IProductRepository>();
        ProductRepository
            .Setup(repository => repository.DeleteByIdAsync(ProductId, CancellationToken.None))
            .ReturnsAsync(false);

        var ProductDeleteServices = new ProductDeleteServices(ProductRepository.Object);

        var result = await ProductDeleteServices.Execute(ProductId, CancellationToken.None);

        Assert.False(result);
        ProductRepository.Verify(repository => repository.DeleteByIdAsync(ProductId, CancellationToken.None), Times.Once);
    }
}
