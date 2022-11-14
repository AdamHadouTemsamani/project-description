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
        Uri url = new Uri("https://github.com/" + username + "/" + repository);
        Console.WriteLine(url);
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName!;
        string[] extract = Regex.Split(path, "bin");
        var fullpath = extract[0] + @"\code\GitInsight\Repositories\" + repository;
        if(!Directory.Exists(fullpath))
        {
            System.IO.Directory.CreateDirectory(fullpath);
        }
        Console.WriteLine(fullpath);
        Repository.Clone(url.ToString(), fullpath);
        
        var repo = new Repository(fullpath);
        var commitHash = repo.Head.Tip.Id.RawId; 

        var latestCommit = _repositories.LatestCommit(repo.Head.RemoteName);


        if(latestCommit != commitHash)
        {
            var repoID = _repositories.Create(new RepositoryCreateDTO(repo.Info.Path, repo.Head.RemoteName, commitHash));
            repo.Commits.ToList().ForEach(x => {
            var authID = _authors.Create(new AuthorCreateDTO(x.Author.Name));
            var comID = _commits.Create(new CommitCreateDTO(x.Author.When.DateTime));
            _authors.AddCommit(x.Author.Name, _commits.Find(comID));
            _repositories.AddAuthor(comID, _authors.Find(authID));
            _repositories.AddCommit(comID, _commits.Find(comID));
            });
        }
        //Check directory exists, if it does unzip and use it
        var repoPath = fullpath + ".zip";
         //Use repository and run database

        //When it is done using the repository it is zipped
        
        ZipFile.CreateFromDirectory(fullpath, repoPath);
        DeleteDirectory.DeleteFolder(fullpath);

        return _repositories.Find(repo.Head.RemoteName  );
    }
    
}