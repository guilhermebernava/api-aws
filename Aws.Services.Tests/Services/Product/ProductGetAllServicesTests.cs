using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Aws.Services.Tests.Services;

public class ProductGetAllServicesTests
{
    [Fact]
    public async Task ItShouldGetAllProducts()
    {
        var userId = Guid.NewGuid();
        var expectedProducts = new List<Product>
        {
            new Product(1,"product1",userId),
            new Product(1,"product2",userId)
        };

        var ProductRepository = new Mock<IProductRepository>();
        ProductRepository
            .Setup(repository => repository.GetAllAsync(CancellationToken.None))
            .ReturnsAsync(expectedProducts);

        var ProductGetAllByUserIdServices = new ProductGetAllServices(ProductRepository.Object);

        var Products = await ProductGetAllByUserIdServices.Execute(CancellationToken.None);
        Assert.NotNull(Products);
        Assert.Equal(expectedProducts.Count, Products.Count);
        Assert.True(expectedProducts.All(expectedProduct => Products.Any(Product => Product.Id == expectedProduct.Id)));
        ProductRepository.Verify(repository => repository.GetAllAsync(CancellationToken.None), Times.Once);
    }
}
