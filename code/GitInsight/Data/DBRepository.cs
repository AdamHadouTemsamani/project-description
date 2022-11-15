namespace Data;

public class DBRepository
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Path { get; set;}
    public string Name { get; set; }
    public int LatestCommit { get; set; }
    public ICollection<DBAuthor> Authors { get; set; }
    public ICollection<DBCommit> Commits { get; set; }

    public DBRepository(string path, string name, int latestCommit)
    {
        Path = path;
        Name = name;
        LatestCommit = latestCommit;
        Authors = new List<DBAuthor>();
        Commits = new List<DBCommit>();
    }
}