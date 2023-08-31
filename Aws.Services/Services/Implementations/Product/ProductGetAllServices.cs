using Domain.Entities;
using Domain.Repositories;

namespace Aws.Services.Services;

public class ProductGetAllServices : IProductGetAllServices
{
    private readonly IProductRepository _repository;

    public ProductGetAllServices(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> Execute(CancellationToken cancellationToken = default)
    {
        return (List<Product>)await _repository.GetAllAsync(cancellationToken);
    }
}