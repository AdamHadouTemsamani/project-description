using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GitInsight.Tests;
public class CommitRepositoryTest : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DBContext _context;
    private readonly CommitRepository _repository;

    public CommitRepositoryTest () 
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var builder = new DbContextOptionsBuilder<DBContext>();
        builder.UseSqlite(_connection);
        var context = new DBContext(builder.Options);
        context.Database.EnsureCreated();
    }

    [Fact]
    public void Create_given_Commit_returns_Created_with_Commit()
    {
        
    }

    public void Dispose () 
    {
        _context.Dispose();
        _connection.Dispose();
    }
}