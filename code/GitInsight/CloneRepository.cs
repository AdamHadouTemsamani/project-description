namespace GitInsight;

public static class CloneRepository
{
    public static LibGit2Sharp.Repository CreateRepository(string username, string repository)
    {
        var url = $"https://github.com/{username}/{repository}";
        var path = GetDirectory(repository);
        if(!Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
            LibGit2Sharp.Repository.Clone(url.ToString(), path);
        }
        return new LibGit2Sharp.Repository(path);
    }

    public static string GetDirectory(string repository)
    {
        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.FullName!;
        string[] extract = Regex.Split(path, "bin");
        string[] paths = {extract[0], "code", "GitInsight", "Repositories", repository};
        return Path.Combine(paths);
    }
}