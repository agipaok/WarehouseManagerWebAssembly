using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CsvController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Δεν επιλέχθηκε αρχείο.");

        using var reader = new StreamReader(file.OpenReadStream());
        var content = await  reader.ReadToEndAsync();
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var results = new List<string>();
        foreach (var line in lines.Skip(1))
        {
            results.Add(line.Trim());
        }
        
        return  Ok(new { message = $"Διαβάστηκαν {results.Count} γραμμές", data = results});
      
    }
}