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
        var search = _context.Repositories.Where(x => x.LatestCommit.Equals(repository.LatestCommit)).FirstOrDefault();

        if(search is null)
        {
            var repo = new DBRepository 
            { Path = repository.Path, Name = repository.Name, LatestCommit = repository.LatestCommit };
            _context.Repositories.Add(repo);
            _context.SaveChanges(); 
            return repo.Id;
        }
        return search.Id;
    }

    public RepositoryDTO Find(int repositoryId)
    {
        var repo = from c in _context.Repositories
                   where c.Id == repositoryId
                   select new RepositoryDTO(c.Id, c.Path!, c.Name!, c.LatestCommit!);
        return repo.FirstOrDefault()!;
    }

    public IReadOnlyCollection<RepositoryDTO> Read()
    {
        var repositories = from c in _context.Repositories
                           select new RepositoryDTO(c.Id, c.Path!, c.Name!,c.LatestCommit!);
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
            _context.SaveChanges();
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

    
    public void AddCommit(int repositoryId, CommitCreateDTO commit)
    {
        if(commit != null)
        {
            var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repositoryId) ).FirstOrDefault();
            var com = new DBCommit
                { Date = commit.Date, Author = commit.Author, BelongsTo = commit.BelongsTo };

            var searchCommit = _context.Commits.Where(x => x.HashCode.Equals(commit.HashCode)).FirstOrDefault();

            if(searchRepo == null || searchCommit == null)
            {
                _context.Commits.Add(com);
                _context.SaveChanges();
                
            }
        }
    }

    public int LatestCommit(int repositoryId)
    {
        var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repositoryId)).FirstOrDefault();
        return searchRepo!.LatestCommit;
    }
}