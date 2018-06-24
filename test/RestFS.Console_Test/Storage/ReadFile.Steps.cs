using System.IO;
using System.Threading.Tasks;
using LightBDD.XUnit2;
using Xunit;

namespace RestFS.Console_Test.Storage
{
    public partial class ReadFile : FeatureFixture
    {
        private const string                  WorkingPath = "directory7";
        private       byte[]                  _content;
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
            File.WriteAllBytes(_file, new byte[] {0x11, 0x22});
        }

        private async Task When_ReadFile_is_invoked_Async()
        {
            _content = await _storage.ReadFileAsync(_file);
        }

        private void Then_correct_file_content_is_read()
        {
            Assert.Equal(2, _content.Length);
            Assert.Equal(0x11, _content[0]);
            Assert.Equal(0x22, _content[1]);
        }
    }
}