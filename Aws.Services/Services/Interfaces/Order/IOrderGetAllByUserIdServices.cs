using Domain.Entities;

namespace Aws.Services.Services;

public interface IOrderGetAllByUserIdServices : IServices<IList<Order>, Guid>
{
}
