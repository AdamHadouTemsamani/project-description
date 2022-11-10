namespace GitInsight;

[ApiController]
[Route("[controller]")]
public class RepositoryController : ControllerBase
{
    private readonly ILogger<RepositoryController> _logger;
    private readonly RepostitoryRepository _repoRepository;

    public RepositoryController(ILogger<RepositoryController> logger, RepostitoryRepository repoRepository)
    {
        _logger = logger;
        _repoRepository = repoRepository;

    }

    [HttpGet(Name = "")]
    public async Task<IReadOnlyCollection<RepositoryDTO>> GetAllRepositoriesAsync()
    {
        var repo = await _repoRepository.ReadAll();
        return repo;
    }

    
    [HttpGet(Name = "{username:string}/{repository:string}")]
    public void GetPullRepository(string username, string repository)
    {
        Uri url = new Uri("https://github.com/" + username + "/" + repository);
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName!;
        string[] extract = Regex.Split(path, "bin");
        var fullpath = extract[0] + "Repositories\\";
        Repository.Clone(url.ToString(), fullpath);
    }

}