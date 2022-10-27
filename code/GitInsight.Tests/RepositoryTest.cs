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
        string filePath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName + "\\frequency.txt";
        string mainOut = File.ReadAllText(filePath).Replace("\r","");
        StringWriter sw = new StringWriter();

        Console.SetOut(sw);
        Program.Main(new string[] {_fullpath, "--frequency"});

        sw.ToString().Replace("\r", "").TrimEnd().Should().Be(mainOut);
    }

    [Fact]
    public void Commit_Grouped_By_Author_and_Date()
    {
        string filePath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName + "\\author.txt";
        string mainOut = File.ReadAllText(filePath).Replace(" ","").Replace("\r\n","");
        StringWriter sw = new StringWriter();

        Console.SetOut(sw);
        Program.Main(new string[] {_fullpath, "--author"});

        sw.ToString().Replace("\r\n", "").Replace("\t", "").Replace(" ","").TrimEnd().Should().Be(mainOut);
    }
}