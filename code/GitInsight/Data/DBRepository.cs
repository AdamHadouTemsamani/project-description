namespace Data;

public class DBRepository
{
    public int Id { get; set; }
    public string Path { get; set;}
    public string Name { get; set; }
    public byte[] LatestCommit { get; set; }
    public ICollection<DBAuthor> Authors { get; set; }
    public ICollection<DBCommit> Commits { get; set; }

    public DBRepository(int id, string path, string name, byte[] latestCommit)
    {
        Id = id;
        Path = path;
        Name = name;
        LatestCommit = latestCommit;
        Authors = new List<DBAuthor>();
        Commits = new List<DBCommit>();
    }
}