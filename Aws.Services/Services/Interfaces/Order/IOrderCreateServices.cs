using Aws.Services.Dtos;

namespace Aws.Services.Services;

public interface IOrderCreateServices : IServices<bool, OrderDto>
{
}
