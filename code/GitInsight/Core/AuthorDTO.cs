namespace Core;

public record AuthorDTO(int Id, string Name);

public record AuthorCreateDTO([StringLength(50)]string Name);

public record AuthorUpdateDTO(int Id, [StringLength(50)]string Name);