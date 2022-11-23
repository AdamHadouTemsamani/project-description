namespace GitInsight;

public interface IGitInsight
{
    public Task AddRepository(Repository repository);
    public Task AddCommits(Repository repository);
    public Task<IEnumerable<(int commitFrequency, DateTime commitDate)>> GetCommitsPerDay(Repository repository);
    public Task<IReadOnlyDictionary<string, IEnumerable<(int commitFrequency, DateTime Commitdate)>>> GetCommitsPerAuthor(Repository repository);
}
