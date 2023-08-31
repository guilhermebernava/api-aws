namespace Aws.Services.Dtos;
public record UserDto(string Email, string Password,string? FullName = default,Guid? Id = default);