namespace Data;

public class RepostitoryRepository : IRepositoryRepository
{
    private readonly DBContext _context;

    public RepostitoryRepository(DBContext context)
    {
        _context = context;
    }

    public int Create(RepositoryCreateDTO repository)
    {
        var search = _context.Repositories.Where(x => x.Name.Equals(repository.Name)).FirstOrDefault();
        var repo = new DBRepository(repository.Path,repository.Name);

        if(search is null)
        {
            _context.Repositories.Add(repo);
            _context.SaveChanges();
        }
        return repo.Id;
    }

    public RepositoryDTO Find(int repositoryId)
    {
        var repo = from c in _context.Repositories
                   where c.Id == repositoryId
                   select new RepositoryDTO(c.Id, c.Path, c.Name);
        return repo.FirstOrDefault()!;
    }

    public IReadOnlyCollection<RepositoryDTO> Read()
    {
        var repositories = from c in _context.Repositories
                           select new RepositoryDTO(c.Id, c.Path, c.Name);
        return repositories.ToList();   
    }

    public void Update(RepositoryUpdateDTO repository)
    {
        var repo = _context.Repositories.Find(repository.Id);
        if(repo is not null)
        {
            repo.Id = repository.Id;
            repo.Path = repository.Path;
            repo.Name = repository.Name;
        }
    }

    public void Delete(int repositoryId)
    {
        var repo = _context.Repositories.Find(repositoryId);
        if(repo is not null)
        {
            _context.Repositories.Remove(repo);
            _context.SaveChanges();
        }
    }
}