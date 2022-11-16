namespace Core;

public interface IRepositoryRepository
{
    RepositoryDTO Find(int repositoryId);
    IReadOnlyCollection<RepositoryDTO> Read();
    int Create(RepositoryCreateDTO repository);
    void Update(RepositoryUpdateDTO repository);
    void Delete(int repositoryId);
    void AddCommit(int repositoryId, CommitCreateDTO commit);
    int LatestCommit(int repositoryId);
}