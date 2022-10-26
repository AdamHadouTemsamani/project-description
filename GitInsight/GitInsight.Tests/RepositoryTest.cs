using LibGit2Sharp;
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
    public void Check_If_Correct_Path_Is_Returned()
    {
        _fullpath.Should().Be("C:\\Users\\Adamh\\OneDrive\\Documents\\GitHub\\project-description\\GitInsight\\GitInsight.Tests\\Testrepo.git");
    }


    [Fact]
    public void Check_If_Repository_Exists_Should_Return_True()
    {
        //Arrange & Act
        var input = Repository.IsValid(_fullpath);

        //Assert
        input.Should().Be(true);

    }

   
}