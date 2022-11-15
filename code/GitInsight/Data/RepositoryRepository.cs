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

        if(search is null)
        {
            var repo = new DBRepository(repository.Path, repository.Name, repository.LatestCommit);
            _context.Repositories.Add(repo);
            _context.SaveChanges();
            return repo.Id;
        }
        return search.Id;
    }

    public RepositoryDTO Find(string repositoryName)
    {
        var repo = from c in _context.Repositories
                   where c.Name == repositoryName
                   select new RepositoryDTO(c.Id, c.Path, c.Name, c.LatestCommit!);
        return repo.FirstOrDefault()!;
    }

    public IReadOnlyCollection<RepositoryDTO> Read()
    {
        var repositories = from c in _context.Repositories
                           select new RepositoryDTO(c.Id, c.Path, c.Name,c.LatestCommit!);
        return repositories.ToList();   
    }

    public async Task<IReadOnlyCollection<RepositoryDTO>> ReadAll()
    {
        var repos = await _context.Repositories.Select(x => new RepositoryDTO(x.Id, x.Path, x.Name, x.LatestCommit!)).ToListAsync();
        return repos!;
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

    public void AddAuthor(int repoID, AuthorDTO author)
    {
        if(author != null)
        {
            var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repoID)).FirstOrDefault();
            var auth = new DBAuthor(author.Name);

            if(searchRepo != null)
            {
                if(!searchRepo.Authors.Contains(auth))
                {
                    searchRepo.Authors.Add(auth);
                    _context.SaveChanges();
                }
            }
        }
    }

    public void AddCommit(int repoID, CommitDTO commit)
    {
        if(commit != null)
        {
            var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repoID)).FirstOrDefault();
            var com = new DBCommit(commit.Date);

            if(searchRepo != null)
            {
                if(!searchRepo.Commits.Contains(com))
                {
                    searchRepo.Commits.Add(com);
                    _context.SaveChanges();
                }
            }
        }
    }

    public int LatestCommit(string name)
    {
        var searchRepo = _context.Repositories.Where(x => x.Name.Equals(name)).FirstOrDefault();
        return searchRepo!.LatestCommit;
    }
}