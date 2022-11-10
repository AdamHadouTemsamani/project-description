namespace GitInsight;

[ApiController]
[Route("[controller]")]
public class RepositoryController : ControllerBase
{
    private readonly RepostitoryRepository _repoRepository;

    public RepositoryController(RepostitoryRepository repoRepository)
    {
        _repoRepository = repoRepository;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<RepositoryDTO>> GetAllRepositoriesAsync()
    {
        return await _repoRepository.ReadAll();
    }

    /*
    [HttpGet(Name = "{username:string}/{repository:string}")]
    public void GetPullRepository(string username, string repository)
    {
        Uri url = new Uri("https://github.com/" + username + "/" + repository);
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName!;
        string[] extract = Regex.Split(path, "bin");
        var fullpath = extract[0] + "Repositories\\";
        Repository.Clone    (url.ToString(), fullpath);
    }
    */
}