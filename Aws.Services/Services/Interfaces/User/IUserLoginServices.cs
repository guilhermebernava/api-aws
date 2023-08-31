using Aws.Services.Dtos;

namespace Aws.Services.Services;

public interface IUserLoginServices : IServices<string,LoginDto>
{
}
