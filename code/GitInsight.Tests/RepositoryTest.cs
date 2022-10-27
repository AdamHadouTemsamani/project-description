using LibGit2Sharp;
using GitInsight;
using System.Text.RegularExpressions;

namespace GitInsight.Tests;

public class RepositoryTest
{
    private string _fullpath;
    private Repository _repository;
   

    public RepositoryTest()
    {
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName;
        string[] extract = Regex.Split(path, "bin");
        _fullpath = extract[0] + "Testrepo.git";
        _repository = new Repository(_fullpath);
    }

    [Fact]
    public void Check_If_Repository_Exists_Should_Return_True()
    {
        //Arrange & Act
        var input = Repository.IsValid(_fullpath);

        //Assert
        input.Should().Be(true);

    }
    
    [Fact]
    public void Commit_Grouped_By_Frequency()
    {  
        string answer = "1 2011-04-14\r\n2 2010-05-25\r\n2 2010-05-24\r\n1 2010-05-11 \r\n 1 2010-05-08";
        StringWriter sw = new StringWriter();

        Console.SetOut(sw);
        Program.Main(new string[] {_fullpath, "--frequency"});

        sw.ToString().Replace(" ","").TrimEnd().Should().Be(answer.Replace(" ",""));
    }

    [Fact]
    public void Commit_Grouped_By_Author_and_Date()
    {
        string answer = "gor \r\n\t 1 2011-04-14 \r\n Scott Chacon \r\n\t 2 2010-05-25 \r\n\t 2 2010-05-24 \r\n\t 1 2010-05-11 \r\n\t 1 2010-05-08";
        StringWriter sw = new StringWriter();

        Console.SetOut(sw);
        Program.Main(new string[] {_fullpath, "--author"});

        sw.ToString().Replace(" ", "").TrimEnd().Should().Be(answer.Replace(" ", ""));
    }
}