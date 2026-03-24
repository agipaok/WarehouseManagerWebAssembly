using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagerWebAssembly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok("Hallo from API");
    }
}

