namespace Data;

public class CommitRepository : ICommitRepository
{
    private readonly DBContext _context;
    public CommitRepository(DBContext context)
    {
        _context = context;
    }

    public (Response response, string commitId) Create(CommitCreateDTO commit)
    {
        var search = _context.Commits.Where(x => x.RepositoryId.Equals(commit.repositoryId) && x.Id.Equals(commit.Id)).FirstOrDefault();
        if(search is null) 
        {
            var com = new DBCommit 
            { 
                RepositoryId = commit.repositoryId, 
                Id = commit.Id,
                Author = commit.Author,
                Date = commit.Date
            };
            _context.Commits.Add(com); 
            _context.SaveChanges();
            return (Response.Created, com.Id);
        }
        return (Response.Conflict, search.Id);
    }

    public CommitDTO Find(string commitId)
    {
        var com = from c in _context.Commits
                   where c.Id == commitId
                   select new CommitDTO(c.RepositoryId, c.Id, c.Author, c.Date);
        return com.FirstOrDefault()!;
    }

    public IReadOnlyCollection<CommitDTO> Read()
    {
        var commits = from c in _context.Commits
                      select new CommitDTO(c.RepositoryId, c.Id, c.Author, c.Date);
        return commits.ToList();
    }

    public Response Update(CommitUpdateDTO commit)
    {
       var search = _context.Commits.Where(x => x.RepositoryId.Equals(commit.repositoryId) && x.Id.Equals(commit.Id)).FirstOrDefault();
        if(search is not null)
        {
            search.Author = commit.Author;
            search.Date = commit.Date;
            _context.SaveChanges();
            return Response.Updated;
        } 
        else if(_context.Commits.Where(x => !x.RepositoryId.Equals(commit.repositoryId) && !x.Id.Equals(commit.Id)) != null)
        {
            return Response.Conflict;
        }
        return Response.NotFound;
    }

    public void Delete(string commitId)
    {
        var com = _context.Commits.Find(commitId);
        if(com is not null)
        {
            _context.Commits.Remove(com);
            _context.SaveChanges();
        }
    }

    public IReadOnlyCollection<CommitDTO> GetAllCommits()
    {
        return _context.Commits.Select(x => new CommitDTO(x.RepositoryId, x.Id, x.Author, x.Date)).ToList();
    }

    public IEnumerable<(int commitCount, DateTime commitDate)> GetCommitsPerDay(string repositoryId)
    {
        return _context.Commits.ToList().GroupBy(x => x.Date).Select(x => (x.Count(), x.Key));
    }

    public IReadOnlyDictionary<string, IEnumerable<(int CommitFrequency, DateTime commitDate)>> GetCommitsPerAuthor(string repositoryId)
    {
        var commits = _context.Commits.ToList();
        var authors = commits.Select(x => x.Author).Distinct();
        var dictionary = new Dictionary<string, IEnumerable<(int CommitFrequency, DateTime commitDate)>>();

        foreach(var author in authors)
        {
            var commit = commits.Where(x => x.Author == author).GroupBy(x => x.Date).Select(x => (x.Count(), x.Key));
            dictionary.Add(author, commit);
        }
        return dictionary;
    }
}