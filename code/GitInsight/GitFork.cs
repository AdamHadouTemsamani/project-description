namespace GitInsight;

public class GitFork
{
    /* Seem's like we don't eed this class anyway. But in case we need to extract information from JSON */
    
        public int forkId { get; set; }
        public Owner owner { get; set; } = null!;
    public class Owner 
    {
        public string Login { get; set; } = null!;
        public int Id { get; set; } 
    }
}