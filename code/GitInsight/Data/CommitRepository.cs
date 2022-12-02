namespace Data;

public class CommitRepository : ICommitRepository
{
    private readonly DBContext _context;
    public CommitRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<(Response response, string commitId)> CreateAsync(CommitCreateDTO commit)
    {
        var search = await _context.Commits.Where(x => x.RepositoryId.Equals(commit.repositoryId) && x.Id.Equals(commit.Id)).FirstOrDefaultAsync();
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

    public async Task<(CommitDTO, Response)> FindAsync(string commitId)
    {
        var search = await _context.Commits.Where(x => x.Id.Equals(commitId)).FirstOrDefaultAsync();
        
        if(search is null)
        {
            return (null, Response.NotFound)!;
        } 
        var com = new CommitDTO(search.RepositoryId, search.Id, search.Author, search.Date);
        return (com, Response.Updated);
    }

    public async Task<IReadOnlyCollection<CommitDTO>> ReadAsync()
    {
        var commits = from c in _context.Commits
                      select new CommitDTO(c.RepositoryId, c.Id, c.Author, c.Date);
        return await commits.ToListAsync();
    }

    public async Task<Response> UpdateAsync(CommitUpdateDTO commit)
    {
       var search = await _context.Commits.Where(x => x.RepositoryId.Equals(commit.repositoryId) && x.Id.Equals(commit.Id)).FirstOrDefaultAsync();
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

    public async Task<Response> DeleteAsync(string commitId)
    {
        var search = await _context.Commits.Where(x => x.Id.Equals(commitId)).FirstOrDefaultAsync();
        if(search is not null)
        {
            _context.Commits.Remove(search);
            _context.SaveChanges();
            return Response.Deleted;
        }
        return Response.NotFound;
    }

    public async Task<IReadOnlyCollection<CommitDTO>> GetAllCommitsAsync()
    {
        return await _context.Commits.Select(x => new CommitDTO(x.RepositoryId, x.Id, x.Author, x.Date)).ToListAsync();
    }

    public async Task<List<(int commitCount, DateTime commitDate)>> GetCommitsPerDayAsync(string repositoryId)
    {
        var commitList = await _context.Commits.ToListAsync();
        return commitList.GroupBy(x => x.Date.Date).Select(g => (g.Count(), g.Key)).ToList();
    }

    public async Task<IReadOnlyDictionary<string, List<(int CommitFrequency, DateTime commitDate)>>> GetCommitsPerAuthorAsync(string repositoryId)
    {
        var commits = await _context.Commits.ToListAsync();
        var authors = commits.Select(x => x.Author).Distinct();
        var dictionary = new Dictionary<string, List<(int CommitFrequency, DateTime commitDate)>>();

        foreach(var author in authors)
        {
            var commit = commits.Where(x => x.Author == author).GroupBy(d => d.Date.Date).Select(g => (g.Count(), g.Key)).ToList();
            dictionary.Add(author, new List<(int, DateTime)> {(3,DateTime.MaxValue)});
        }
        return dictionary;
    }
}