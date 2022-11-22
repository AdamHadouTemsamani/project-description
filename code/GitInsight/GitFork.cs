namespace GitInsight;

public class GitFork
{
    /*Use user secrets + netwtonsoft.json to dynamically deserialize your json */
        public int forkId { get; set; }
        public Owner owner { get; set; } = null!;
    public class Owner 
    {
        public string Login { get; set; } = null!;
        public int Id { get; set; } 
    }
}