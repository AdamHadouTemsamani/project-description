namespace Data;

public class RepostitoryRepository : IRepositoryRepository
{
    private readonly DBContext _context;

    public RepostitoryRepository(DBContext context)
    {
        _context = context;
    }

    public (Response response, string repositoryId) Create(RepositoryCreateDTO repository)
    {
        var search = _context.Repositories.Where(x => x.Id.Equals(repository.Id)).FirstOrDefault();
        Response response;
        if(search is null)
        {
            var repo = new DBRepository 
            { 
                Id = repository.Id, 
                Path = repository.Path, 
                Name = repository.Name, 
                LatestCommit = repository.LatestCommit 
            };
            _context.Repositories.Add(repo);
            _context.SaveChanges(); 
            response = Response.Created;
            return (response, repo.Id);
        }

        response = Response.Conflict;
        return (response, search.Id);
    }

    public RepositoryDTO Find(string repositoryId)
    {
        var repo = from c in _context.Repositories
                   where c.Id == repositoryId
                   select new RepositoryDTO(c.Id, c.Path, c.Name, c.LatestCommit);
        return repo.FirstOrDefault()!;
    }

    public IReadOnlyCollection<RepositoryDTO> Read()
    {
        var repositories = from c in _context.Repositories
                           select new RepositoryDTO(c.Id, c.Path, c.Name, c.LatestCommit!);
        return repositories.ToList();   
    }

    public Response Update(RepositoryUpdateDTO repository)
    {
        var search = _context.Repositories.Where(x => x.Id.Equals(repository.Id)).FirstOrDefault();
        if(search is not null)
        {
            search.Path = repository.Path;
            search.Name = repository.Name;
            search.LatestCommit = repository.LatestCommit;
            _context.SaveChanges();
            return Response.Updated;
        } 
        else if(_context.Repositories.Where(x => !x.Id.Equals(repository.Id)) != null)
        {
            return Response.Conflict;
        }
        return Response.NotFound;
    }

    public void Delete(string repositoryId)
    {
        var search = _context.Repositories.Find(repositoryId);

        if(search is not null)
        { 
            _context.Repositories.Remove(search);
            _context.SaveChanges();
        }
    }


    public bool LatestCommit(RepositoryUpdateDTO repository)
    {
        var search = _context.Repositories.Where(x => x.Id.Equals(repository.Id)).FirstOrDefault();
        if(search is not null)
        {
            if(search.LatestCommit == repository.LatestCommit)
            {
                return true;
            }
        }
        return false;
    }
}