namespace GitInsight;

public class GitInsight : IGitInsight
{
    private readonly IRepositoryRepository _repository;
    private readonly ICommitRepository _commit;
    private readonly DBContext? _context;

    public GitInsight(DBContext context)
    {
        var ctx = context;  
        _repository = new RepostitoryRepository(ctx);
        _commit = new CommitRepository(ctx);
    }

    /*
    public GitInsight()
    {
    
        var connection = new SqliteConnection("DataSource=GitInsight.db");
        connection.Open();

        var builder = new DbContextOptionsBuilder<DBContext>();
        builder.UseSqlite(connection);
        var context = new DBContext(builder.Options);
        context.Database.EnsureCreated();
        context.SaveChanges();
        _context = context;
        _commit = new CommitRepository(_context);
        _repository = new RepostitoryRepository(_context);
    
    }
    */

    public GitInsight()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<DBContext>();
        builder.UseSqlite(connection);
        var context = new DBContext(builder.Options);
        context.Database.EnsureCreated();
        context.SaveChanges();

        _context = context;
        _repository = new RepostitoryRepository(_context);
        _commit = new CommitRepository(_context);
    }
    

    public async Task AddRepository(LibGit2Sharp.Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        var LatestCommit = repository.Head.Tip.GetHashCode();

        if(await _repository.LatestCommit(new RepositoryUpdateDTO(repositoryId, repository.Info.Path, repository.Head.RemoteName, LatestCommit)))
        {
            Console.WriteLine("Repository already exists in database");
        }
        else 
        {
            var repo = await _repository.CreateAsync(new RepositoryCreateDTO(repositoryId, repository.Info.Path, repository.Head.RemoteName, LatestCommit));
            if(repo.response == Response.Conflict)
            {
                await _repository.UpdateAsync(new RepositoryUpdateDTO(repositoryId, repository.Info.Path, repository.Head.RemoteName, LatestCommit));
            }
            await AddCommits(repository);
        }
    }
    
    public async Task AddCommits(LibGit2Sharp.Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        var commits = repository.Commits;

        foreach(var commit in commits)
        {
            await _commit.CreateAsync(new CommitCreateDTO(repositoryId, commit.Sha, commit.Author.Name, commit.Author.When.DateTime));
        }
    }

    public async Task<List<(int commitFrequency, DateTime commitDate)>> GetCommitsPerDayAsync(LibGit2Sharp.Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        return await _commit.GetCommitsPerDayAsync(repositoryId);
    }

    public async Task<IReadOnlyDictionary<string, List<(int commitFrequency, DateTime Commitdate)>>> GetCommitsPerAuthorAsync(LibGit2Sharp.Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        return await _commit.GetCommitsPerAuthorAsync(repositoryId);
    }
    
    public void GetMostUsedFile( LibGit2Sharp.Repository repository)
    {
       
    }
}