using Aws.Services.Dtos;
using Aws.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateAsync([FromServices] IProductCreateServices productCreateServices, [FromBody] ProductDto dto)
    {
        var created = await productCreateServices.Execute(dto);

        if (created) return Created("/Product/GetAll", null);
        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromServices] IProductUpdateServices productUpdateServices, [FromBody] ProductDto dto)
    {
        var updated = await productUpdateServices.Execute(dto);

        if (updated) return Ok();
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromServices] IProductDeleteServices productDeleteServices, [FromBody] Guid productId)
    {
        var updated = await productDeleteServices.Execute(productId);

        if (updated) return Ok();
        return BadRequest();
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync([FromServices] IProductGetAllServices productGetAllServices)
    {
        var products = await productGetAllServices.Execute();
        return Ok(products);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetFromIdAsync([FromServices] IProductGetByIdServices productGetByIdServices, [FromQuery] Guid productId)
    {
        var product = await productGetByIdServices.Execute(productId);
        return Ok(product);
    }
}
