namespace Core;

public interface IAuthorRepository
{
    AuthorDTO Find(int authorId);
    IReadOnlyCollection<AuthorDTO> Read();
    int Create(AuthorCreateDTO author);
    void Update(AuthorUpdateDTO author);
    void AddCommit(int authorId, CommitCreateDTO commit);
    void Delete(int authorId);
}