using LibGit2Sharp;
using System.Linq;
using System;

public static class Program 
{
    private static SqliteConnection? _connection;
    private static DbContextOptionsBuilder<DBContext>? _builder;
    private static DBContext? _context;

    private static CommitFrequency? _frequency;

    public static void Main(string[] args)
    {
        /*
        var builder = WebApplication.CreateBuilder(args);

        //Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        var app = builder.Build();
        
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
        */
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
        _connection = new SqliteConnection("Data Source=hello.db");
        _connection.Open();

        _builder = new DbContextOptionsBuilder<DBContext>();
        _builder.UseSqlite(_connection);

        _context = new DBContext(_builder.Options);
        _context.Database.EnsureCreated();
    }

    private static void DBClose()
    {
        _connection!.Close();
    }
    

    private static void DBRepoSetup(string[] args)
    {
        var authRepo = new AuthorRepository(_context!);
        var commitRepo = new CommitRepository(_context!);
        var repoRepo = new RepostitoryRepository(_context!);
        var repo = new Repository(Repository.IsValid(args.FirstOrDefault("")) ? args[0] : Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName);
        var commitHash = repo.Head.Tip.Id; 

        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName;
        string[] extract = Regex.Split(path, "bin");
        var fullpath = extract[0] + "Repositories\\";
        
        var search = from c in _context?.Repositories
                     where c.LatestCommit == commitHash
                     select c.LatestCommit;
        search.First();

        if(search is null)
        {
            var repoID = repoRepo.Create(new RepositoryCreateDTO(repo.Info.Path, Path.GetFileName(Directory.GetParent(repo.Info.Path)!.Parent!.FullName)));
            repo.Commits.ToList().ForEach(x => {
            var authID = authRepo.Create(new AuthorCreateDTO(x.Author.Name));
            var comID = commitRepo.Create(new CommitCreateDTO(x.Author.When.DateTime));
            authRepo.addCommit(x.Author.Name, commitRepo.Find(comID));
            repoRepo.addAuthor(comID, authRepo.Find(authID));
            repoRepo.addCommit(comID, commitRepo.Find(comID), repo.Head.Tip.Id);
            });
        }
    }
}