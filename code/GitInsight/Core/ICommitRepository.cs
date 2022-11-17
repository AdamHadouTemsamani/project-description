namespace Core;

public interface ICommitRepository
{
    (Response response, string commitId) Create(CommitCreateDTO commit);
    CommitDTO Find(string commitId);
    IReadOnlyCollection<CommitDTO> GetAllCommits();
    IEnumerable<(int commitCount, DateTime commitDate)> GetCommitsPerDay(string repositoryId);
    IReadOnlyDictionary<string, IEnumerable<(int CommitFrequency, DateTime commitDate)>> GetCommitsPerAuthor(string repositoryId);
    Response Update(CommitUpdateDTO author);
    void Delete(string commitId);
}