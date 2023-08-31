using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Aws.Services.Tests.Services;

public class ProductCreateServicesTests
{
    private readonly IProductRepository _ProductRepository;
    private readonly IValidator<ProductDto> _validator;
    private readonly IMapper _mapper;
    private readonly ProductCreateServices _ProductCreateServices;

    public ProductCreateServicesTests()
    {
        _ProductRepository = Mock.Of<IProductRepository>();
        _validator = Mock.Of<IValidator<ProductDto>>();
        _mapper = Mock.Of<IMapper>();

        _ProductCreateServices = new ProductCreateServices(_ProductRepository, _validator, _mapper);
    }

    [Fact]
    public async Task ItShouldCreateProduct()
    {
        var ProductDto = new ProductDto(0, "", Guid.NewGuid());
        var validationResult = new ValidationResult(new List<ValidationFailure>());
        var Product = new Product(0, "", Guid.NewGuid());

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(ProductDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        Mock.Get(_mapper)
            .Setup(m => m.Map<Product>(ProductDto))
            .Returns(Product);

        Mock.Get(_ProductRepository)
            .Setup(repository => repository.AddAsync(Product, CancellationToken.None))
            .ReturnsAsync(true);

        var result = await _ProductCreateServices.Execute(ProductDto, CancellationToken.None);

        Assert.True(result);

        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(ProductDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<Product>(ProductDto), Times.Once);
        Mock.Get(_ProductRepository).Verify(repository => repository.AddAsync(Product, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ItShouldNotCreateProduct()
    {
        var ProductDto = new ProductDto(-100, "", Guid.Empty);
        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("PropertyName", "Error message")
        };

        var validationResult = new ValidationResult(validationErrors);

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(ProductDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _ProductCreateServices.Execute(ProductDto, CancellationToken.None));

        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(ProductDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<Product>(ProductDto), Times.Never);
        Mock.Get(_ProductRepository).Verify(repository => repository.AddAsync(It.IsAny<Product>(), CancellationToken.None), Times.Never);
    }
}
