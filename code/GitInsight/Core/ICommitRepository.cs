namespace Core;

public interface ICommitRepository
{
    Task<(Response response, string commitId)> CreateAsync(CommitCreateDTO commit);
    Task<(CommitDTO, Response)> FindAsync(string commitId);
    Task<IReadOnlyCollection<CommitDTO>> GetAllCommitsAsync();
    Task<List<(int commitCount, DateTime commitDate)>> GetCommitsPerDayAsync(string repositoryId);
    Task<IReadOnlyDictionary<string, List<(int CommitFrequency, DateTime commitDate)>>> GetCommitsPerAuthorAsync(string repositoryId);
    Task<Response> UpdateAsync(CommitUpdateDTO author);
    Task<Response> DeleteAsync(string commitId);
}