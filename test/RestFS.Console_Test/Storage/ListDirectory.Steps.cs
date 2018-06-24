using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LightBDD.XUnit2;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.Storage
{
    public partial class ListDirectory : FeatureFixture
    {
        private const string                  WorkingPath = "directory3";
        private       string                  _directory;
        private       DirectoryList           _dirList;
        private       string                  _file;
        private       Console.Storage.Storage _storage;

        private void Given_a_initialized_storage()
        {
            Directory.CreateDirectory(WorkingPath);
            _storage = new Console.Storage.Storage(@"./");
        }

        private void Given_a_existent_directory_with_content()
        {
            _directory = WorkingPath;
            _file      = Path.Combine(WorkingPath, "test.file");
            File.Create(_file);
        }

        private async Task When_ListDirectory_is_invoked_Async()
        {
            _dirList = await _storage.ListDirectoryAsync(_directory);
        }


        private void Then_correct_directory_listing_is_returned()
        {
            Assert.True(_dirList.Entries.Count > 0);
            Assert.True(_dirList.Entries.FirstOrDefault(entry => entry.Name == "test.file") != null);
        }
    }
}