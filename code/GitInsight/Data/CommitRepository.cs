namespace Data;

public class CommitRepository : ICommitRepository
{
    private readonly DBContext _context;

    public CommitRepository(DBContext context)
    {
        _context = context;
    }

    public int Create(CommitCreateDTO commit)
    {
        /* Check if the item already exists */
        var search = _context.Commits.Where(x => x.Date.Equals(commit.Date)).FirstOrDefault();

        /* If not add it to the database */
        if(search is null) 
        {
            var com = new DBCommit { Date = commit.Date, Author = commit.Author,  BelongsTo = commit.BelongsTo };
            _context.Commits.Add(com); 
            _context.SaveChanges();
            return com.Id;
        }
        return search.Id;
    }

    public CommitDTO Find(int commitId)
    {
        var com = from c in _context.Commits
                   where c.Id == commitId
                   select new CommitDTO(c.Id, c.Date, c.Author, c.BelongsTo);
        return com.FirstOrDefault()!;
    }

    public IReadOnlyCollection<CommitDTO> Read()
    {
        var commits = from c in _context.Commits
                      select new CommitDTO(c.Id, c.Date, c.Author, c.BelongsTo);
        return commits.ToList();
    }

    public void Update(CommitUpdateDTO commit)
    {
        var com = _context.Commits.Find(commit.Id);
        if(com is not null)
        {
            com.Id = commit.Id;
            com.Date = commit.Date;
            com.Author = commit.Author;
            com.BelongsTo = commit.BelongsTo;
            _context.SaveChanges();
        }
        
    }


    public void Delete(int commitId)
    {
        var com = _context.Commits.Find(commitId);
        if(com is not null)
        {
            _context.Commits.Remove(com);
            _context.SaveChanges();
        }
    }
}