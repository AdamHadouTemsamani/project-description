namespace GitInsight;

public class CommitFrequency
{
    public void PrintCommitGroupedByFrequency(DBContext context)
    {
        PrintGitCommitsGroupedByDate(context!.Commits.ToList());
    }

    public void PrintCommitGroupedByDateAndAuthor(DBContext context)
    {
        context!.Authors.ToList().ForEach(x => {
            Console.WriteLine(x.Name);
            PrintGitCommitsGroupedByDate(x.Commits.ToList(),true);
        });
    }

    public void PrintGitCommitsGroupedByDate(IList<DBCommit> group, bool indentation = false)
    {
        group.ToList().GroupBy(d => d.Date.ToString("yyyy-MM-dd")).ToList().ForEach(c => Console.WriteLine((indentation?"\t":"") + c.Count() + " " + c.Key));
    }
}