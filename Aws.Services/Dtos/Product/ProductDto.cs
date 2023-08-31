namespace Aws.Services.Dtos;

public record ProductDto(double Value, string Name, Guid UserId,Guid? Id = default);