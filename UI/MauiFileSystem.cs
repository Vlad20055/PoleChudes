using Infrastructure;

namespace UI;

public class MauiFileSystem : IFileSystemPath
{
    public string GetAppDataDirectory() => FileSystem.AppDataDirectory;
}