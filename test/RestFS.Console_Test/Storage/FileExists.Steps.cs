using System.IO;
using LightBDD.XUnit2;
using Xunit;

namespace RestFS.Console_Test.Storage
{
    public partial class FileExists : FeatureFixture
    {
        private const string                  WorkingPath = "testDirectory1";
        private       bool                    _exists;
        private       string                  _file;
        private       Console.Storage.Storage _storage;

        private void Given_a_initialized_storage()
        {
            Directory.CreateDirectory(WorkingPath);
            _storage = new Console.Storage.Storage(@"./");
        }

        private void Given_a_existent_file()
        {
            _file = Path.Combine(WorkingPath, "file.exists");
            File.Create(_file);
        }

        private void Given_a_non_existing_file()
        {
            _file = Path.Combine(WorkingPath, "file_not.exists");
        }

        private void When_FileExists_is_invoked()
        {
            _exists = _storage.FileExists(_file);
        }

        private void Then_FileExists_returns_true()
        {
            Assert.True(_exists);
        }

        private void Then_FileExists_returns_false()
        {
            Assert.False(_exists);
        }
    }
}