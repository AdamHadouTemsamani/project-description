namespace GitInsight;

[ApiController]
public class RepositoryController : ControllerBase
{

    

    //[HttpGet(Name = "{username}/{repository}")]
    [Route("{username}/{repository}")]
    public void GetPullRepository(string username, string repository)
    {
        Uri url = new Uri("https://github.com/" + username + "/" + repository);
        Console.WriteLine(url);
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName!;
        string[] extract = Regex.Split(path, "bin");
        var fullpath = extract[0] + @"\code\GitInsight\Repositories\" + repository;
        if(!Directory.Exists(fullpath))
        {
            System.IO.Directory.CreateDirectory(fullpath);
        }
        Console.WriteLine(fullpath);
        Repository.Clone(url.ToString(), fullpath);
        
        //Use repo


        //Check directory exists, if it does unzip and use it


        //When it is done using the repository it is zipped
        var repoPath = fullpath + ".zip";
        ZipFile.CreateFromDirectory(fullpath, repoPath);
        DeleteDirectory.DeleteFolder(fullpath);
    }
    
}