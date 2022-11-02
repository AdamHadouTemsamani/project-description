using LibGit2Sharp;
using System.Linq;
using System;

public static class Program 
{
    public static void Main(string[] args)
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var builder = new DbContextOptionsBuilder<DBContext>();
        builder.UseSqlite(connection);

        var context = new DBContext(builder.Options); 
        context.Database.EnsureCreated();
        /*context.AddRange(new Tag("Teddybear"){Id = 1}, 
                         new Tag("Balloon"){Id = 2});*/
        context.SaveChanges();

        var authRepo = new AuthorRepository(context);
        var commitRepo = new CommitRepository(context);
        var repo = Repository.IsValid(args.FirstOrDefault("")) ? args[0] : Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName;

        

        foreach(var arg in args)
        {
            switch(arg)
            {
                case "--frequency":
                    PrintCommitGroupedByFrequency(rep);
                    break;

                case "--author":
                    PrintCommitGroupedByDateAndAuthor(rep);
                    break;
            }
          
        }
    }

    private static void PrintCommitGroupedByFrequency(Repository com){
        PrintGitCommitsGroupedByDate(com.Commits.ToList());
    }

    private static void PrintCommitGroupedByDateAndAuthor(Repository com){
        com.Commits.GroupBy(x => x.Author.Name).ToList().ForEach(x => {
            Console.WriteLine(x.Key);
            PrintGitCommitsGroupedByDate(x.ToList(),true);
        });
    }

    private static void PrintGitCommitsGroupedByDate(IList<Commit> group, bool indentation = false){
        group.ToList().GroupBy(d => d.Author.When.ToString("yyyy-MM-dd")).ToList().ForEach(c => Console.WriteLine((indentation?"\t":"") + c.Count() + " " + c.Key));
    }
}





