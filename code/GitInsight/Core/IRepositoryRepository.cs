namespace Core;

public interface IRepositoryRepository
{
    RepositoryDTO Find(string repositoryName);
    Task<IReadOnlyCollection<RepositoryDTO>> ReadAll();
    IReadOnlyCollection<RepositoryDTO> Read();
    int Create(RepositoryCreateDTO repository);
    void Update(RepositoryUpdateDTO repository);
    void Delete(int repositoryId);
    void AddAuthor(int authorID, AuthorDTO author);
    void AddCommit(int commitID, CommitDTO commit);
    byte[] LatestCommit(string Name);
}