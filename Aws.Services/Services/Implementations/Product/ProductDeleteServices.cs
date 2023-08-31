using Domain.Repositories;

namespace Aws.Services.Services;

public class ProductDeleteServices : IProductDeleteServices
{
    private readonly IProductRepository _repository;

    public ProductDeleteServices(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Execute(Guid ProductId, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteByIdAsync(ProductId, cancellationToken);
    }
}