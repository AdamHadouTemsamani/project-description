using LibGit2Sharp;
using System.Linq;
using System;


public static class Program 
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults( webBuilder => webBuilder.UseStartup<Startup>());
    }

       /*
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DBContext>(o => o.UseSqlite("Data Source=GitInsight.db"));
        builder.Services.AddTransient<IRepositoryRepository, RepostitoryRepository>();
        builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
        builder.Services.AddTransient<ICommitRepository, CommitRepository>();

        //Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        var app = builder.Build();
        
        

        app.Run();
        
        
        DBSetup();
        DBRepoSetup(args);

        foreach(var arg in args)
        {
            switch(arg)
            {
                case "--frequency":

                    _frequency?.PrintCommitGroupedByFrequency(_context!);
                    break;

                case "--author":
                    _frequency?.PrintCommitGroupedByDateAndAuthor(_context!);
                    break;
            }
          
        }

        DBClose();
    }

    private static void DBSetup()
    {
        
        _builder = new DbContextOptionsBuilder<DBContext>();
        _builder.UseSqlite("Data Source=GitInsight.db");

        _context = new DBContext(_builder.Options);
        _context.Database.Migrate();
    }

    

    private static void DBRepoSetup(string[] args)
    {
        var authRepo = new AuthorRepository(_context!);
        var commitRepo = new CommitRepository(_context!);
        var repoRepo = new RepostitoryRepository(_context!);
        var repo = new Repository(Repository.IsValid(args.FirstOrDefault("")) ? args[0] : Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName);
        var commitHash = repo.Head.Tip.Id.RawId; 
        
        var search = from c in _context?.Repositories
                     where c.LatestCommit == commitHash
                     select c.LatestCommit;
        
        if(search.FirstOrDefault() is null)
        {
            var repoID = repoRepo.Create(new RepositoryCreateDTO(repo.Info.Path, repo.Head.RemoteName, commitHash));
            repo.Commits.ToList().ForEach(x => {
            var authID = authRepo.Create(new AuthorCreateDTO(x.Author.Name));
            var comID = commitRepo.Create(new CommitCreateDTO(x.Author.When.DateTime));
            authRepo.addCommit(x.Author.Name, commitRepo.Find(comID));
            repoRepo.addAuthor(comID, authRepo.Find(authID));
            repoRepo.addCommit(comID, commitRepo.Find(comID));
            });
        }
    }
    */
}