namespace Core;

public record CommitDTO(int Id, int HashCode, DateTime Date, DBAuthor Author, DBRepository BelongsTo);

public record CommitCreateDTO(int HashCode, DateTime Date, DBAuthor Author, DBRepository BelongsTo);

public record CommitUpdateDTO(int Id, int HashCode, DateTime Date, DBAuthor Author, DBRepository BelongsTo);