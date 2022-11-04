namespace Data;

public class AuthorRepository : IAuthorRepository
{
    private readonly DBContext _context;
    private int _id = 0;

    public AuthorRepository(DBContext context)
    {
        _context = context;
    }

    public int Create(AuthorCreateDTO author)
    {
        /* Check if the item already exists */
        var search = _context.Authors.Where(x => x.Name.Equals(author.Name)).FirstOrDefault();

        /* If not add it to the database */
        if(search is null) 
        {
            var auth = new DBAuthor(_id++,author.Name);
            _context.Authors.Add(auth);
            _context.SaveChanges();
            return auth.Id;
        }
        return search.Id;
    }

    public AuthorDTO Find(int authorId)
    {
        var auth = from c in _context.Authors
                   where c.Id == authorId
                   select new AuthorDTO(c.Id, c.Name!);
        return auth.FirstOrDefault()!;
    }

    public IReadOnlyCollection<AuthorDTO> Read()
    {
        var authors = from c in _context.Authors
                      select new AuthorDTO(c.Id, c.Name!);
        return authors.ToList();
    }

    public void Update(AuthorUpdateDTO author)
    {
        var auth = _context.Authors.Find(author.Id);
        if(auth is not null)
        {
            auth.Id = author.Id;
            auth.Name = author.Name;
        }
    }

    public void Delete(int authorId)
    {
        var auth = _context.Authors.Find(authorId);
        if(auth is not null)
        {
            _context.Authors.Remove(auth);
            _context.SaveChanges();
        }
    }

    public void addCommit(string authorName, CommitDTO commit)
    {
        var searchAuth = _context.Authors.Where(x => x.Name.Equals(authorName)).FirstOrDefault();
        if(searchAuth != null)
        {
            searchAuth.Commits.Add(new DBCommit(commit.Id,commit.Date));
        }
    }
}