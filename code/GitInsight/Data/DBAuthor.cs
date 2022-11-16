namespace Data;

[Index(nameof(Name), IsUnique = true)]
public class DBAuthor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<DBCommit> Commits { get; set; } = null!;

}