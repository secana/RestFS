using System;
using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class DirectoryInfo : FeatureFixture
    {
        private const string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       Browser         _browser;
        private       string          _directory;
        private       FileAttributes  _expectedAttr;
        private       BrowserResponse _response;
        private       Mock<IStorage>  _storage;

        private void Given_a_directory(string directory)
        {
            _directory    = directory;
            _expectedAttr = new FileAttributes(directory, 1, new DateTime(2018, 06, 16, 12, 13, 14), true);
        }

        private void Given_a_storage(bool containsDir)
        {
            _storage = new Mock<IStorage>();
            _storage.Setup(s => s.DirectoryExists(_directory)).Returns(containsDir);
            _storage.Setup(s => s.IsDirectory(_directory)).Returns(true);
            _storage.Setup(s => s.ReadDirAttributes(_directory)).Returns(_expectedAttr);
        }

        private void Given_a_fake_browser()
        {
            _browser = Helper.CreateBrowser(_storage.Object);
        }

        private void When_head_on_directory_is_invoked()
        {
            _response = _browser.Head(Route, with =>
            {
                with.Query("directory", _directory);
                with.HttpRequest();
            }).GetAwaiter().GetResult();
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _response.StatusCode);
        }

        private void Then_expected_directory_attributes_are_returned()
        {
            Assert.Equal(_expectedAttr.Name, _response.Headers["Name"]);
        }
    }
}