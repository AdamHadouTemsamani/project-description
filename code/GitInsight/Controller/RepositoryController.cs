namespace GitInsight;

public class RepositoryController : ControllerBase
{
    private readonly ILogger<RepositoryController> _logger;

    public RepositoryController(ILogger<RepositoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Insight")]
    
}