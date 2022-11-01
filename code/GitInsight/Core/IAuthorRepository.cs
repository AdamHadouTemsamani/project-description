namespace Core;

public interface IAuthorRepository
{
    AuthorDTO Find(int authorId);
    IReadOnlyCollection<AuthorDTO> Read();
    int Create(AuthorCreateDTO author);
    void Update(AuthorUpdateDTO author);
    void Delete(int authorId);
}