namespace GitInsight;

public interface IGitInsight
{
    public Task AddRepository(Repository repository);
    public Task AddCommits(Repository repository);
    public Task<List<(int commitFrequency, DateTime commitDate)>> GetCommitsPerDayAsync(Repository repository);
    public Task<IReadOnlyDictionary<string, List<(int commitFrequency, DateTime Commitdate)>>> GetCommitsPerAuthorAsync(Repository repository);
}
