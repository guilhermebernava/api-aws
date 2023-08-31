using Aws.Services.Dtos;
using Aws.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderItemController : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromServices] IOrderItemUpdateServices orderItemUpdateServices, [FromBody] OrderItemDto dto)
    {
        var updated = await orderItemUpdateServices.Execute(dto);

        if (updated) return Ok();
        return BadRequest();
    }
}
