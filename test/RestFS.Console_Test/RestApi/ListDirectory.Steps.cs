using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class ListDirectory : FeatureFixture
    {
        private const string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       DirectoryList   _acutalDirList;
        private       Browser         _browser;
        private       string          _directory;
        private       FileAttributes  _expectedAttr;
        private       DirectoryList   _expectedDirList;
        private       BrowserResponse _response;
        private       Mock<IStorage>  _storage;

        private void Given_a_directory(string directory)
        {
            _directory       = directory;
            _expectedDirList = CreateExpectedDirList();
            _expectedAttr    = new FileAttributes(directory, 1, new DateTime(2018, 06, 16, 12, 13, 14), true);
        }

        private void Given_a_storage(bool containsDir)
        {
            _storage = new Mock<IStorage>();
            _storage.Setup(s => s.DirectoryExists(_directory)).Returns(containsDir);
            _storage.Setup(s => s.IsDirectory(_directory)).Returns(true);
            _storage.Setup(s => s.ListDirectoryAsync(_directory)).Returns(Task.FromResult(_expectedDirList));
            _storage.Setup(s => s.ReadDirAttributes(_directory)).Returns(_expectedAttr);
        }

        private void Given_a_fake_browser()
        {
            _browser = Helper.CreateBrowser(_storage.Object);
        }

        private void When_get_on_directory_is_invoked()
        {
            _response = _browser.Get(Route, with =>
            {
                with.Query("directory", _directory);
                with.HttpRequest();
                with.Header("Accept", "application/json");
            }).GetAwaiter().GetResult();
            _acutalDirList = JsonConvert.DeserializeObject<DirectoryList>(_response.Body.AsString());
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _response.StatusCode);
        }

        private void Then_expected_directory_listing_is_returned()
        {
            Assert.Equal(_expectedDirList.Entries.Count, _acutalDirList.Entries.Count);
            Assert.Equal(_expectedDirList.Entries[0].Name, _acutalDirList.Entries[0].Name);
            Assert.Equal(_expectedDirList.Entries[1].Name, _acutalDirList.Entries[1].Name);
        }

        private DirectoryList CreateExpectedDirList()
        {
            return new DirectoryList
            {
                Entries = new List<FileAttributes>
                {
                    new FileAttributes("file1.ext", 10, new DateTime(2018, 03, 08, 12, 13, 14), false),
                    new FileAttributes("dir1", 1, new DateTime(2018, 03, 08, 13, 14, 15), true)
                }
            };
        }

        private void Then_expected_directory_attributes_are_returned()
        {
            Assert.Equal(_expectedAttr.Name, _response.Headers["Name"]);
        }
    }
}