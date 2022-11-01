namespace Data;

public class DBCommit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public DBCommit(DateTime date)
    {
        Date = date;
    }
}