namespace Core;

public record CommitDTO(int Id, DateTime Date, DBAuthor Author, DBRepository BelongsTo);

public record CommitCreateDTO(DateTime Date, DBAuthor Author, DBRepository BelongsTo);

public record CommitUpdateDTO(int Id, DateTime Date, DBAuthor Author, DBRepository BelongsTo);