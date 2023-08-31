using Domain.Entities;
using Domain.Repositories;

namespace Aws.Services.Services;

public class ProductGetByIdServices : IProductGetByIdServices
{
    private readonly IProductRepository _repository;

    public ProductGetByIdServices(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Execute(Guid parameter, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(parameter,cancellationToken);
    }
}