using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using Infra.Exceptions;
using Moq;

namespace Aws.Services.Tests.Services;

public class ProductGetByIdServicesTests
{
    [Fact]
    public async Task ItShouldGetProduct()
    {
        var product =
                   new Product(1, "product1", Guid.NewGuid());

        var ProductRepository = new Mock<IProductRepository>();
        ProductRepository
            .Setup(repository => repository.GetByIdAsync(product.Id, CancellationToken.None))
            .ReturnsAsync(product);

        var ProductGetAllByUserIdServices = new ProductGetByIdServices(ProductRepository.Object);

        var result = await ProductGetAllByUserIdServices.Execute(product.Id, CancellationToken.None);
        Assert.NotNull(result);
        Assert.Equal(result.Id, product.Id);
    }

    [Fact]
    public async Task ItShouldThrowsRepositoryException()
    {
        var product =
                   new Product(1, "product1", Guid.NewGuid());

        var ProductRepository = new Mock<IProductRepository>();
        ProductRepository
            .Setup(repository => repository.GetByIdAsync(product.Id, CancellationToken.None))
            .Throws(new RepositoryException("Not found"));

        var ProductGetAllByUserIdServices = new ProductGetByIdServices(ProductRepository.Object);

       await Assert.ThrowsAsync<RepositoryException>(async () => await ProductGetAllByUserIdServices.Execute(product.Id, CancellationToken.None));
    }
}
