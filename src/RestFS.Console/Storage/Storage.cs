using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestFS.Console.Storage
{
    public class Storage : IStorage
    {
        private readonly string _rootDir;

        public Storage(string rootDirectory)
        {
            _rootDir = Path.GetFullPath(rootDirectory);
        }

        public bool FileExists(string file)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            return File.Exists(fullPath);
        }

        public bool DirectoryExists(string directory)
        {
            var fullPath = SecurePathCombine(_rootDir, directory);
            return Directory.Exists(fullPath);
        }

        public bool IsDirectory(string file)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            return File.GetAttributes(fullPath).HasFlag(System.IO.FileAttributes.Directory);
        }

        public async Task<byte[]> ReadFileAsync(string file)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            return await File.ReadAllBytesAsync(fullPath);
        }

        public async Task<DirectoryList> ListDirectoryAsync(string directory)
        {
            var fullPath   = SecurePathCombine(_rootDir, directory);
            var dirListing = new DirectoryList();
            var files      = Directory.GetFiles(fullPath);
            var dirs       = Directory.GetDirectories(fullPath);
             
            await Task.Run(() => dirListing.Entries.AddRange(GetFileAttributes(files)));
            await Task.Run(() => dirListing.Entries.AddRange(GetDirAttributes(dirs)));

            return dirListing;
        }

        public async Task WriteFileAsync(string file, byte[] content)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            var dir = Path.GetDirectoryName(fullPath);
            if (dir is null) { throw new ArgumentNullException(nameof(dir)); }
            Directory.CreateDirectory(dir);
            await File.WriteAllBytesAsync(fullPath, content);
        }

        public void CreateDirectory(string directory)
        {
            var fullPath = SecurePathCombine(_rootDir, directory);
            Directory.CreateDirectory(fullPath);
        }

        public void DeleteFile(string file)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            File.Delete(fullPath);
        }

        public void DeleteDirectory(string directory)
        {
            var fullPath = SecurePathCombine(_rootDir, directory);
            Directory.Delete(fullPath, true);
        }

        public FileAttributes ReadFileAttributes(string file)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            var fi       = new FileInfo(fullPath);

            return new FileAttributes(
                fi.Name,
                fi.Length,
                fi.LastWriteTime,
                fi.Attributes.HasFlag(System.IO.FileAttributes.Directory));
        }

        public FileAttributes ReadDirAttributes(string file)
        {
            var fullPath = SecurePathCombine(_rootDir, file);
            var fi       = new DirectoryInfo(fullPath);

            return new FileAttributes(
                fi.Name,
                1,
                fi.LastWriteTime,
                fi.Attributes.HasFlag(System.IO.FileAttributes.Directory));
        }

        private List<FileAttributes> GetFileAttributes(string[] dirs)
        {
            return dirs.Select(ReadFileAttributes).ToList();
        }

        private List<FileAttributes> GetDirAttributes(string[] dirs)
        {
            return dirs.Select(ReadDirAttributes).ToList();
        }

        private string SecurePathCombine(params string[] values)
        {
            var path          = Path.Combine(values);
            var canonicalPath = Path.GetFullPath(path);

            // Prevent directory traversal by checking if the requested 
            // canonical path starts with the root directory.
            if (!canonicalPath.StartsWith(_rootDir))
                throw new UnauthorizedAccessException();

            return path;
        }
    }
}