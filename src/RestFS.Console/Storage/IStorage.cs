using System.Threading.Tasks;

namespace RestFS.Console.Storage
{
    public interface IStorage
    {
        bool FileExists(string file);
        bool DirectoryExists(string directory);
        bool IsDirectory(string file);

        Task<byte[]> ReadFileAsync(string file);
        FileAttributes ReadFileAttributes(string file);
        FileAttributes ReadDirAttributes(string directory);
        Task<DirectoryList> ListDirectoryAsync(string directory);

        Task WriteFileAsync(string file, byte[] content);
        void CreateDirectory(string directory);
        void DeleteFile(string file);
        void DeleteDirectory(string directory);
    }
}