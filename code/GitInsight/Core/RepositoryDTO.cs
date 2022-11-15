namespace Core;

public record RepositoryDTO(int Id, string Path, string Name, int LatestCommit);

public record RepositoryCreateDTO(string Path, string Name, int LatestCommit);

public record RepositoryUpdateDTO(int Id, string Path, string Name);