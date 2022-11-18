namespace GitInsight;

public class GitFork
{
    /*Use user secrets + netwtonsoft.json to dynamically deserialize your json */
    public Dictionary<int, List<Fork>> RootObject { get; set; } = null!;
    public class Fork 
    {
        public int Id { get; set; }
        public string Owner { get; set; } = null!;
    }

    public class Owner 
    {
        public string Login { get; set; } = null!;
        public int Id { get; set; } 
    }
}