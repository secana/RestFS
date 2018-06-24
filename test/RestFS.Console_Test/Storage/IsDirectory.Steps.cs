using System.IO;
using LightBDD.XUnit2;
using Xunit;

namespace RestFS.Console_Test.Storage
{
    public partial class IsDirectory : FeatureFixture
    {
        private const string                  WorkingPath = "testDirectory2";
        private       string                  _directory;
        private       bool                    _isDirectory;
        private       Console.Storage.Storage _storage;

        private void Given_a_initialized_storage()
        {
            Directory.CreateDirectory(WorkingPath);
            _storage = new Console.Storage.Storage(@"./");
        }

        private void Given_a_directory()
        {
            _directory = Path.Combine(WorkingPath, "directory1");
            Directory.CreateDirectory(_directory);
        }

        private void Given_no_directory()
        {
            _directory = Path.Combine(WorkingPath, "not_a_dir.file");
            File.Create(_directory);
        }

        private void When_IsDirectory_is_invoked()
        {
            _isDirectory = _storage.IsDirectory(_directory);
        }

        private void Then_IsDirectory_returns_true()
        {
            Assert.True(_isDirectory);
        }

        private void Then_IsDirectory_returns_false()
        {
            Assert.False(_isDirectory);
        }
    }
}