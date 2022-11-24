namespace Core;

public record RepositoryDTO(string Id, string Path, string Name, int LatestCommit);

public record RepositoryCreateDTO(string Id, string Path, string Name, int LatestCommit);

public record RepositoryUpdateDTO(string Id, string Path, string Name, int LatestCommit);