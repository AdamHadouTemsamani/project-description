namespace Core;

public record RepositoryDTO(int Id, string Path, string Name, ObjectId LatestCommit);

public record RepositoryCreateDTO([StringLength(120)]string Path,[StringLength(50)]string Name);

public record RepositoryUpdateDTO(int Id, [StringLength(120)]string Path,[StringLength(50)]string Name);