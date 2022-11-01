namespace Core;

public interface ICommitRepository
{
    int Create(CommitCreateDTO commit);
    CommitDTO Find(int commitId);
    IReadOnlyCollection<CommitDTO> Read();
    void Update(CommitUpdateDTO commit);
    void Delete(int commitId);
}