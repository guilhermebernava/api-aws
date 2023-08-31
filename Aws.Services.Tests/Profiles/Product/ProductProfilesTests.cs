using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Profiles;
using Domain.Entities;
using Domain.Utils;

namespace Aws.Services.Tests.Profiles;

public class ProductProfilesTests
{
    private readonly IMapper _mapper;

    public ProductProfilesTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductProfiles>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void ItShouldMapAllValues()
    {
        var ProductDto = new ProductDto(10, "password", Guid.NewGuid(), Guid.NewGuid());
        var Product = _mapper.Map<Product>(ProductDto);

        Assert.Equal(ProductDto.Name, Product.Name);
        Assert.Equal(ProductDto.Value, Product.Value);
        Assert.Equal(ProductDto.UserId,Product.UserId);
        Assert.Equal(ProductDto.Id, Product.Id);
    }
}
