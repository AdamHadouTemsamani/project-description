namespace Core;

public record CommitDTO(string repositoryId, string Id, string Author, DateTime Date);

public record CommitCreateDTO(string repositoryId, string Id, string Author, DateTime Date);

public record CommitUpdateDTO(string repositoryId, string Id, string Author, DateTime Date); 