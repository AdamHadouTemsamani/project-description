namespace Data;

public class DBCommit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public DBCommit(DateTime date)
    {
        Date = date;
    }
}