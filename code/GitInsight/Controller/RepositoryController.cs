namespace GitInsight;

[Route("{username}/{repository}")]
[ApiController]
public class RepositoryController : ControllerBase
{

    private readonly IRepositoryRepository _repositories;
    private readonly IAuthorRepository _authors;
    private readonly ICommitRepository _commits;


    
    public RepositoryController(IRepositoryRepository repositories, IAuthorRepository authors, ICommitRepository commits)
    {
        _repositories = repositories;
        _authors = authors;
        _commits = commits;
    }
    
    [HttpGet]
    public RepositoryDTO PullRepository(string username, string repository)
    {
        var path = GitInsight.GetDirectory(repository);
        var repo = GitInsight.CreateRepository(username, repository);
        
        var repositoryId = _repositories.Create(new RepositoryCreateDTO(repo.Info.Path, repo.Head.RemoteName, repo.Head.Tip.GetHashCode()));
        foreach(Commit c in repo.Commits)
        {   
            var authorId = _authors.Create(new AuthorCreateDTO(c.Author.Name));
            var commitId = _commits.Create(new CommitCreateDTO(c.GetHashCode(),
                            c.Author.When.DateTime, 
                            new DBAuthor { Name = c.Author.Name }, 
                            new DBRepository { Path = repo.Info.Path, Name = repo.Head.RemoteName, LatestCommit = repo.Head.Tip.GetHashCode()}));

            _authors.AddCommit(authorId, new CommitCreateDTO(c.GetHashCode(), c.Author.When.Date, new DBAuthor { Name = c.Author.Name }, new DBRepository { Path = repo.Info.Path, Name = repo.Head.RemoteName, LatestCommit = repo.Head.Tip.GetHashCode() }));
            _repositories.AddCommit(commitId, new CommitCreateDTO(c.GetHashCode(), c.Author.When.Date, new DBAuthor { Name = c.Author.Name }, new DBRepository { Path = repo.Info.Path, Name = repo.Head.RemoteName, LatestCommit = repo.Head.Tip.GetHashCode()}));
        }
        
        //Check directory exists, if it does unzip and use it
        var repoPath = path + ".zip";
         //Use repository and run database

        //When it is done using the repository it is zipped
        
        ZipFile.CreateFromDirectory(path, repoPath);
        DeleteDirectory.DeleteFolder(path);

        return _repositories.Find(repositoryId);
    }
    
}