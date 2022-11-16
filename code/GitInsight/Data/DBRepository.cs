namespace Data;

[Index(nameof(LatestCommit), IsUnique = true)]
public class DBRepository
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Path { get; set;}
    public string? Name { get; set; }
    public int LatestCommit { get; set; }
    public ICollection<DBCommit> Commits { get; set; } = null!;

}