using LibGit2Sharp;
using System.Linq;
using System;

public static class Program 
{
    private static SqliteConnection? _connection;
    private static DbContextOptionsBuilder<DBContext>? _builder;
    private static DBContext? _context;
    

    public static void Main(string[] args)
    {
        builder.Services.AddHttpClient();
        DBSetup();
        DBRepoSetup(args);

        foreach(var arg in args)
        {
            switch(arg)
            {
                case "--frequency":
                    PrintCommitGroupedByFrequency();
                    break;

                case "--author":
                    PrintCommitGroupedByDateAndAuthor();
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

        var repoID = repoRepo.Create(new RepositoryCreateDTO(repo.Info.Path, Path.GetFileName(Directory.GetParent(repo.Info.Path)!.Parent!.FullName)));
        repo.Commits.ToList().ForEach(x => {
            var authID = authRepo.Create(new AuthorCreateDTO(x.Author.Name));
            var comID = commitRepo.Create(new CommitCreateDTO(x.Author.When.DateTime));
            authRepo.addCommit(x.Author.Name, commitRepo.Find(comID));
            repoRepo.addAuthor(comID, authRepo.Find(authID));
            repoRepo.addCommit(comID, commitRepo.Find(comID));
        });
    }

    private static void PrintCommitGroupedByFrequency()
    {
        PrintGitCommitsGroupedByDate(_context!.Commits.ToList());
    }

    private static void PrintCommitGroupedByDateAndAuthor()
    {
        _context!.Authors.ToList().ForEach(x => {
            Console.WriteLine(x.Name);
            PrintGitCommitsGroupedByDate(x.Commits.ToList(),true);
        });
    }

    private static void PrintGitCommitsGroupedByDate(IList<DBCommit> group, bool indentation = false)
    {
        group.ToList().GroupBy(d => d.Date.ToString("yyyy-MM-dd")).ToList().ForEach(c => Console.WriteLine((indentation?"\t":"") + c.Count() + " " + c.Key));
    }
}