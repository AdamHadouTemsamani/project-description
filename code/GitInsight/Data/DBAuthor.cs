namespace Data;
public class DBAuthor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<DBCommit> Commits { get; set; }

    public DBAuthor(string name)
    {
        Name = name;
        Commits = new List<DBCommit>();
    }
}