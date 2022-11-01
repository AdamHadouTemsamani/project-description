namespace Data;

public class AuthorRepository : IAuthorRepository
{
    private readonly DBContext _context;

    public AuthorRepository(DBContext context)
    {
        _context = context;
    }

    public int Create(AuthorCreateDTO author)
    {
        var search = _context.Authors.Where(x => x.Name.Equals(author.Name))
                             .FirstOrDefault();
                    
    }
}