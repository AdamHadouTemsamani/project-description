namespace Data;

public class RepostitoryRepository : IRepositoryRepository
{
    private readonly DBContext _context;
    private int _id = 0;

    public RepostitoryRepository(DBContext context)
    {
        _context = context;
    }

    public int Create(RepositoryCreateDTO repository)
    {
        var search = _context.Repositories.Where(x => x.Name.Equals(repository.Name)).FirstOrDefault();

        if(search is null)
        {
            var repo = new DBRepository(_id++, repository.Path, repository.Name, null!);
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

    public void addAuthor(int repoID, AuthorDTO author)
    {
        if(author != null)
        {
            var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repoID)).FirstOrDefault();
            var auth = new DBAuthor(author.Id, author.Name);

            if(searchRepo != null)
            {
                if(!searchRepo.Authors.Contains(auth))
                {
                    searchRepo.Authors.Add(auth);
                }
            }
        }
    }

    public void addCommit(int repoID, CommitDTO commit, ObjectId latestCommit)
    {
        if(commit != null)
        {
            var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repoID)).FirstOrDefault();
            var com = new DBCommit(commit.Id, commit.Date);

            if(searchRepo != null)
            {
                if(!searchRepo.Commits.Contains(com))
                {
                    searchRepo.Commits.Add(com);
                    searchRepo.LatestCommit = latestCommit;
                }
            }
        }
    }

    public ObjectId getLatestCommit(int repoId)
    {
        var searchRepo = _context.Repositories.Where(x => x.Id.Equals(repoId)).FirstOrDefault();
        return searchRepo?.LatestCommit!;
    }
}