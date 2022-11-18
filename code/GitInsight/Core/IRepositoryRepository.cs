namespace Core;

public interface IRepositoryRepository
{
    RepositoryDTO Find(string repositoryId);
    IReadOnlyCollection<RepositoryDTO> Read();
    (Response response, string repositoryId) Create(RepositoryCreateDTO repository);
    Response Update(RepositoryUpdateDTO repository);
    void Delete(string repositoryId);
    bool LatestCommit(RepositoryUpdateDTO repository);
}