namespace Core;

public interface IRepositoryRepository
{
    Task<(RepositoryDTO, Response)> FindAsync(string repositoryId);
    Task<IReadOnlyCollection<RepositoryDTO>> ReadAsync();
    Task<(Response response, string repositoryId)> CreateAsync(RepositoryCreateDTO repository);
    Task<Response> UpdateAsync(RepositoryUpdateDTO repository);
    Task<Response> DeleteAsync(string repositoryId);
    Task<bool> LatestCommit(RepositoryUpdateDTO repository);
}