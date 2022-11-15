namespace Data;

public class DBCommit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DBAuthor Author { get; set; } = null!;
    public DBRepository BelongsTo { get; set; } = null!;

    
}