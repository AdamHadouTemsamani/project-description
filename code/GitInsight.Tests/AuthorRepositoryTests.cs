using Core;
using LibGit2Sharp;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GitInsight.Tests;

public class AuthorRepositoryTests : IDisposable
{
    private readonly DBContext _context;
    private readonly AuthorRepository _repository;

    public AuthorRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<DBContext>();
        builder.UseSqlite(connection);
        var context = new DBContext(builder.Options);
        context.Database.EnsureCreated();

        var author = new DBAuthor(1, "Bastjan");
        var author2 = new DBAuthor(2, "Karl");

        context.Authors.Add(author);
        context.Authors.Add(author2);
        context.SaveChanges();

        _context = context;
        _repository = new AuthorRepository(_context);
    }

    [Fact]
    public void Create_returns_author_Id_3()
    {
        var author = new AuthorCreateDTO("Meow");

        var actual = _repository.Create(author);
        
        actual.Should().Be(3);
    }

    [Fact]
    public void Find_returns_AuthorDTO_given_Id_2()
    {
        var actual = _repository.Find(2);

        actual.Should()
            .BeEquivalentTo(new AuthorDTO(2, "Karl"));
    }

    [Fact]
    public void Delete_returns_null_given_author_Id_2()
    {
        _repository.Delete(2);

        var actual = _repository.Find(2);

        actual.Should().Be(null);
    }

    [Fact]
    public void Read_returns_authors()
    {
        var actual = _repository.Read();

        var expected = new List<AuthorDTO>
        {
            new (1, "Bastjan"),
            new (2, "Karl")
        };

        actual.Should()
            .BeEquivalentTo(expected);
    }

    [Fact]
    public void addCommit_adds_new_commit()
    { 
        _repository.addCommit("Bastjan", new CommitDTO(1, new DateTime(09, 21, 2000)));
        
        
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
}
