using System.IO;
using System.Threading.Tasks;
using LightBDD.XUnit2;
using Xunit;

namespace RestFS.Console_Test.Storage
{
    public partial class WriteFile : FeatureFixture
    {
        private const string                  WorkingPath = "directory4";
        private       string                  _file;
        private       byte[]                  _fileContent;
        private       Console.Storage.Storage _storage;

        private void Given_a_initialized_storage()
        {
            Directory.CreateDirectory(WorkingPath);
            _storage = new Console.Storage.Storage(@"./");
        }

        private void Given_a_existent_folder_structure()
        {
            _file        = Path.Combine(WorkingPath, "sub1", "sub2", "sub3", "file.name");
            _fileContent = new byte[] {0x11, 0x22};
        }

        private async Task When_WriteFile_is_invoked_Async()
        {
            await _storage.WriteFileAsync(_file, _fileContent);
        }

        private void Then_file_with_correct_content_is_created()
        {
            Assert.True(File.Exists(_file));
            var content = File.ReadAllBytes(_file);
            Assert.Equal(2, content.Length);
            Assert.Equal(0x11, content[0]);
            Assert.Equal(0x22, content[1]);
        }
    }
}