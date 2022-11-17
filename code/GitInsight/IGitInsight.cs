namespace GitInsight;

public interface IGitInsight
{
    public void AddRepository(Repository repository);
    public void AddCommits(Repository repository);
    public IEnumerable<(int commitFrequency, DateTime commitDate)> GetCommitsPerDay(Repository repository);
    public IReadOnlyDictionary<string, IEnumerable<(int commitFrequency, DateTime Commitdate)>> GetCommitsPerAuthor(Repository repository);
}
