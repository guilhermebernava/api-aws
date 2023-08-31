using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;

namespace Aws.Services.Profiles;

public class ProductProfiles : Profile
{
    public ProductProfiles()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}
