using Aws.Services.Dtos;
using Aws.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginAsync([FromServices] IUserLoginServices loginServices,[FromBody] LoginDto dto)
    {
        var token = await loginServices.Execute(dto);

        if(token != null) return Ok(token);
        return Unauthorized();
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateAsync([FromServices] IUserCreateServices userCreateServices, [FromBody] UserDto dto)
    {
        var created = await userCreateServices.Execute(dto);

        if (created) return Created("/User/Login", null);
        return BadRequest();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromServices] IUserUpdateServices userUpdateServices, [FromBody] UserDto dto)
    {
        var updated = await userUpdateServices.Execute(dto);

        if (updated) return Ok();
        return BadRequest();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromServices] IUserDeleteServices userDeleteServices, [FromBody] Guid userId)
    {
        var updated = await userDeleteServices.Execute(userId);

        if (updated) return Ok();
        return BadRequest();
    }
}
