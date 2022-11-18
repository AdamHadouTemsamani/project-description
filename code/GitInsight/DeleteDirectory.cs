namespace GitInsight;

public static class DeleteDirectory
{
    public static void DeleteFolder(string fullpath)
    {
        var directory = new DirectoryInfo(fullpath) { Attributes = FileAttributes.Normal };

        foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
        {
            info.Attributes = FileAttributes.Normal;
        }
        directory.Delete(true);
    }
}