using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;

namespace Aws.Services.Services;

public class ProductCreateServices : IProductCreateServices
{
    private readonly IProductRepository _repository;
    private readonly IValidator<ProductDto> _validator;
    private readonly IMapper _mapper;

    public ProductCreateServices(IProductRepository repository, IValidator<ProductDto> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<bool> Execute(ProductDto parameter, CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(parameter);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }

        var Product = _mapper.Map<Product>(parameter);
        return await _repository.AddAsync(Product, cancellationToken);
    }
}