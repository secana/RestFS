using System.IO;
using LightBDD.XUnit2;
using Xunit;

namespace RestFS.Console_Test.Storage
{
    public partial class DeleteFile : FeatureFixture
    {
        private const string                  WorkingPath = "directory5";
        private       string                  _file;
        private       Console.Storage.Storage _storage;

        private void Given_a_initialized_storage()
        {
            Directory.CreateDirectory(WorkingPath);
            _storage = new Console.Storage.Storage(@"./");
        }

        private void Given_a_existent_file()
        {
            _file = Path.Combine(WorkingPath, "sub1", "sub2", "file.name");
            Directory.CreateDirectory(Path.Combine(WorkingPath, "sub1", "sub2"));
            File.WriteAllText(_file, "foobar");
        }

        private void When_DeleteDirectory_is_invoked()
        {
            _storage.DeleteDirectory(_file);
        }

        private void When_DeleteFile_is_invoked()
        {
            _storage.DeleteFile(_file);
        }

        private void Then_file_is_removed()
        {
            Assert.False(File.Exists(_file));
            Assert.True(Directory.Exists(Path.Combine(WorkingPath, "sub1", "sub2")));
        }

        private void Given_a_existent_directory()
        {
            _file = Path.Combine(WorkingPath, "sub3");
            Directory.CreateDirectory(_file);
        }

        private void Then_directory_is_removed()
        {
            Assert.False(Directory.Exists(_file));
        }
    }
}