using Microsoft.AspNetCore.Mvc;
using single_api.Models;

namespace single_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
 
    private readonly ILogger<MoviesController> _logger;
    private readonly IMemoryLoad<Movie> _memoryLoad;

    public MoviesController(ILogger<MoviesController> logger,
    IMemoryLoad<Movie> memoryLoad)
    {
        _logger = logger;
        _memoryLoad = memoryLoad;

    }

    [HttpGet(Name = "Movies")]
    public IEnumerable<Movie> Get()
    {
        return _memoryLoad.Movies();
    }
}
