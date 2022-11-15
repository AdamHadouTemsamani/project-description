namespace Core;

public interface IRepositoryRepository
{
    RepositoryDTO Find(string repositoryName);
    IReadOnlyCollection<RepositoryDTO> Read();
    int Create(RepositoryCreateDTO repository);
    void Update(RepositoryUpdateDTO repository);
    void Delete(int repositoryId);
    void AddCommit(int repoID, CommitDTO commit);
    int LatestCommit(string Name);
}