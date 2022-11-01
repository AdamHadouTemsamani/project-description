namespace Core;

public record CommitDTO(int Id, DateTime Date);

public record CommitCreateDTO(DateTime Date);

public record CommitUpdateDTO(int Id, DateTime Date);