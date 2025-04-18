using Microsoft.AspNetCore.Mvc;

namespace Triangle.Task.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, List<TriangleResult> results)
    {
        _logger = logger;
        results = new List<TriangleResult>();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }




    [HttpPost]
    public IActionResult CalculateTriangle(Triangle triangle)
    {

        if (triangle == null)
        {
            return BadRequest("Triangle data is required.");
        }
        if (triangle.SideA <= 0 || triangle.SideB <= 0 || triangle.SideC <= 0)
        {
            return BadRequest("All sides must be greater than zero.");
        }

        if(triangle.SideA + triangle.SideB <= triangle.SideC ||
           triangle.SideA + triangle.SideC <= triangle.SideB ||
           triangle.SideB + triangle.SideC <= triangle.SideA)
        {
            return BadRequest("The provided sides do not form a valid triangle.");
        }
        decimal semiPerimeter = (triangle.SideA + triangle.SideB + triangle.SideC) / 2;
        decimal area = (decimal)Math.Sqrt((double)(semiPerimeter * (semiPerimeter - triangle.SideA) * (semiPerimeter - triangle.SideB) * (semiPerimeter - triangle.SideC)));
        decimal perimeter = triangle.SideA + triangle.SideB + triangle.SideC;
        var result = new TriangleResult
        {
            Area = (decimal)area,
            Perimeter = perimeter
        };
        return Ok(result);
    }

}



public class Triangle
{
    public decimal SideA { get; set; }
    public decimal SideB { get; set; }
    public decimal SideC { get; set; }
}

public class TriangleResult
{
    public decimal Area { get; set; }
    public decimal Perimeter { get; set; }
}