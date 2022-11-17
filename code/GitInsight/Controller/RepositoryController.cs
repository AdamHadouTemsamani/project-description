namespace GitInsight;


[ApiController]
public class RepositoryController : ControllerBase
{
    private readonly IGitInsight _gitInsight;    
    public RepositoryController(IGitInsight gitInsight)
    {
        _gitInsight = gitInsight;
    }
    
    [HttpGet]
    [Route("{username}/{repository}")]
    public IEnumerable<(int commitFrequency, DateTime commitDate)> PullRepository(string username, string repository)
    {
        var path = CloneRepository.GetDirectory(repository);
        var repo = CloneRepository.CreateRepository(username, repository);
        
        _gitInsight.AddRepository(repo);
        return _gitInsight.GetCommitsPerDay(repo);
    }
    
}