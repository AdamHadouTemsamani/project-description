namespace GitInsight;

public interface IGitInsight
{
    public Task AddRepository(LibGit2Sharp.Repository repository);
    public Task AddCommits(LibGit2Sharp.Repository repository);
    public Task<List<(int commitFrequency, DateTime commitDate)>> GetCommitsPerDayAsync(LibGit2Sharp.Repository repository);
    public Task<IReadOnlyDictionary<string, List<(int commitFrequency, DateTime Commitdate)>>> GetCommitsPerAuthorAsync(LibGit2Sharp.Repository repository);
}
