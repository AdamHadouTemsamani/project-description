namespace Core;

public interface ICommitRepository
{
    int Create(CommitCreateDTO commit);
    CommitDTO Find(int commitId);
    IReadOnlyCollection<CommitDTO> Read();
    void Update(CommitUpdateDTO author);
    void Delete(int commitId);
}