using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagerWebAssembly.Data;
using WarehouseManagerWebAssembly.Models;

[ApiController]
[Route("api/[controller]")]
public class CsvController : ControllerBase
{
   private readonly AppDbContext _db;
    public CsvController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Δεν επιλέχθηκε αρχείο");

        using var reader = new StreamReader(file.OpenReadStream());
        var content = await reader.ReadToEndAsync();
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        var products = new List<Product>();

        foreach (var line in lines.Skip(1))
        {
            var cols = line.Split(',');
            if (cols.Length < 6 ) continue;
            
            products.Add(new Product
            {
                Name = cols[0].Trim(),
                SKU = cols[1].Trim(),
                Category = cols[2].Trim(),
                Price = decimal.Parse(cols[3].Trim()),
                Stock = int.Parse(cols[4].Trim()),
                MinStock = int.Parse(cols[5].Trim())
                
            });
        }

        await _db.Products.AddRangeAsync();
        await _db.SaveChangesAsync();

        return Ok(new { message = $"Αποθηκεύτηκαν {products.Count} προϊόντα", data = products });

    }
}

