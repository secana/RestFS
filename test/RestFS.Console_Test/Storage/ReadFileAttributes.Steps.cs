using System.IO;
using LightBDD.XUnit2;
using Xunit;
using FileAttributes = RestFS.Console.Storage.FileAttributes;

namespace RestFS.Console_Test.Storage
{
    public partial class ReadFileAttributes : FeatureFixture
    {
        private const string                  WorkingPath = "directory6";
        private       string                  _file;
        private       FileAttributes          _fileAttributes;
        private       Console.Storage.Storage _storage;

        private void Given_a_initialized_storage()
        {
            Directory.CreateDirectory(WorkingPath);
            _storage = new Console.Storage.Storage(@"./");
        }

        private void Given_a_existent_storage()
        {
            _file = Path.Combine(WorkingPath, "file.exists");
            File.WriteAllBytes(_file, new byte[] {0x11, 0x22});
        }

        private void When_ReadFileAttributes_is_invoked()
        {
            _fileAttributes = _storage.ReadFileAttributes(_file);
        }

        private void Then_correct_attributes_are_returned()
        {
            Assert.Equal(2, _fileAttributes.Size);
            Assert.Equal("file.exists", _fileAttributes.Name);
        }
    }
}