namespace GitInsight;

public class GitInsight : IGitInsight
{
    private readonly IRepositoryRepository? _repository;
    private readonly ICommitRepository? _commit;
    private readonly DBContext? _context;

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

    public GitInsight(bool isMemoryDate)
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

    public void AddRepository(Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        var LatestCommit = repository.Head.Tip.GetHashCode();

        if(_repository!.LatestCommit(new RepositoryUpdateDTO(repositoryId, repository.Info.Path, repository.Head.RemoteName, LatestCommit)))
        {
            var repo = _repository.Create(new RepositoryCreateDTO(repositoryId, repository.Info.Path, repository.Head.RemoteName, LatestCommit));
            if(repo.response == Response.Conflict)
            {
                _repository.Update(new RepositoryUpdateDTO(repositoryId, repository.Info.Path, repository.Head.RemoteName, LatestCommit));
            }
            AddCommits(repository);
        }
    }
    
    public void AddCommits(Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        var commits = repository.Commits;

        foreach(var commit in commits)
        {
            _commit!.Create(new CommitCreateDTO(repositoryId, commit.Sha, commit.Author.Name, commit.Author.When.DateTime));
        }
    }

    public IEnumerable<(int commitFrequency, DateTime commitDate)> GetCommitsPerDay(Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        return _commit.GetCommitsPerDay(repositoryId);
    }

    public IReadOnlyDictionary<string, IEnumerable<(int commitFrequency, DateTime Commitdate)>> GetCommitsPerAuthor(Repository repository)
    {
        var repositoryId = repository.Commits.ToList()[0].Sha;
        return _commit.GetCommitsPerAuthor(repositoryId);
    }

}