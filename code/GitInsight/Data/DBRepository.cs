namespace Data;

public class DBRepository
{
    public int Id { get; set; }
    public string Path { get; set;}
    public string Name { get; set; }
    public ICollection<DBAuthor> Authors { get; set; }
    public ICollection<DBCommit> Commits { get; set; }

    public DBRepository(string path, string name)
    {
        Path = path;
        Name = name;
        Authors = new List<DBAuthor>();
        Commits = new List<DBCommit>();
    }
}