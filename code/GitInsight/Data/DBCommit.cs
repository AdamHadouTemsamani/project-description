namespace Data;

public class DBCommit
{
    public string RepositoryId { get; set; } = null!;
    public string Id { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime Date { get; set; }
}