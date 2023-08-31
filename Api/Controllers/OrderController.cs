using Aws.Services.Dtos;
using Aws.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromServices] IOrderCreateServices orderCreateServices, [FromBody] OrderDto dto)
    {
        var created = await orderCreateServices.Execute(dto);

        if (!created) return BadRequest();
        return Created("/Order/GetAllByUserId", null);
    }

    [HttpGet]
    [Route("GetAllByUserId")]
    public async Task<IActionResult> GetAllAsync([FromServices] IOrderGetAllByUserIdServices orderGetAllByUserIdServices, [FromQuery] Guid userId)
    {
        var orders = await orderGetAllByUserIdServices.Execute(userId);
        return Ok(orders);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync([FromServices] IOrderGetByIdServices orderGetByIdServices, [FromQuery] Guid orderId)
    {
        var order = await orderGetByIdServices.Execute(orderId);
        return Ok(order);
    }
}
