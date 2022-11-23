namespace Data;

public class RepostitoryRepository : IRepositoryRepository
{
    private readonly DBContext _context;

    public RepostitoryRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<(Response response, string repositoryId)> CreateAsync(RepositoryCreateDTO repository)
    {
        var search = await _context.Repositories.Where(x => x.Id.Equals(repository.Id)).FirstOrDefaultAsync();
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

    public async Task<(RepositoryDTO, Response)> FindAsync(string repositoryId)
    {
        var search = await _context.Repositories.Where(x => x.Id.Equals(repositoryId)).FirstOrDefaultAsync();
        
        if(search is null)
        {
            return (null, Response.NotFound)!;
        } 
        var repo = new RepositoryDTO(search.Id, search.Path, search.Name, search.LatestCommit!);

        return (repo,Response.Updated);
    }

    public async Task<IReadOnlyCollection<RepositoryDTO>> ReadAsync()
    {
        var repositories = from c in _context.Repositories
                           select new RepositoryDTO(c.Id, c.Path, c.Name, c.LatestCommit!);
        return await repositories.ToListAsync();   
    }

    public async Task<Response> UpdateAsync(RepositoryUpdateDTO repository)
    {
        var search = await _context.Repositories.Where(x => x.Id.Equals(repository.Id)).FirstOrDefaultAsync();
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

    public async Task<Response> DeleteAsync(string repositoryId)
    {
        var search = await _context.Repositories.Where(x => x.Id.Equals(repositoryId)).FirstOrDefaultAsync();
        if(search is not null)
        { 
            _context.Repositories.Remove(search);
            _context.SaveChanges();
            return Response.Deleted;
        }
        return Response.NotFound;
    }


    public async Task<bool> LatestCommit(RepositoryUpdateDTO repository)
    {
        var search = await _context.Repositories.Where(x => x.Id.Equals(repository.Id)).FirstOrDefaultAsync();
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