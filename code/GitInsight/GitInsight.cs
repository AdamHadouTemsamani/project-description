namespace GitInsight;

public static class GitInsight
{
    public static Repository CreateRepository(string username, string repository)
    {
        Uri url = new Uri("https://github.com/" + username + "/" + repository);
        var path = GetDirectory(repository);
        if(!Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        Repository.Clone(url.ToString(), path);
        return new Repository(path);
    }

    public static string GetDirectory(string repository)
    {
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName!;
        string[] extract = Regex.Split(path, "bin");
        return extract[0] + @"\code\GitInsight\Repositories\" + repository;
    }

    
}