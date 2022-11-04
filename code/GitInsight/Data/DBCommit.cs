namespace Data;

public class DBCommit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public DBCommit(int id, DateTime date)
    {
        Id = id;
        Date = date;
    }
}