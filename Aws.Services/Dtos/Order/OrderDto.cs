namespace Aws.Services.Dtos;

public record OrderDto(Guid UserId, List<OrderItemDto> Items);