namespace Data;

public class DBRepository
{
    public string Id { get; set; } = null!; 
    public string Path { get; set;} = null!;
    public string Name { get; set; } = null!;
    public int LatestCommit { get; set; }

}