using System;
using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class ReadFileInfo : FeatureFixture
    {
        private const string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       Browser         _browser;
        private       FileAttributes  _expectedAttr;
        private       string          _file;
        private       BrowserResponse _result;
        private       Mock<IStorage>  _storage;

        private void Given_a_file(string file)
        {
            _file         = file;
            _expectedAttr = new FileAttributes("file.ext", 10, new DateTime(2018, 03, 08, 12, 13, 14), false);
        }

        private void Given_a_storage(bool containsFile)
        {
            _storage = new Mock<IStorage>();
            _storage.Setup(s => s.ReadFileAttributes(_file)).Returns(_expectedAttr);
            _storage.Setup(s => s.FileExists(_file)).Returns(containsFile);
        }

        private void Given_a_fake_browser()
        {
            _browser = Helper.CreateBrowser(_storage.Object);
        }

        private void When_Head_on_file_is_invoked()
        {
            _result = _browser.Head(Route, with =>
            {
                with.Query("file", _file);
                with.HttpRequest();
            }).GetAwaiter().GetResult();
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _result.StatusCode);
        }

        private void Then_expected_file_attributes_are_returned()
        {
            Assert.Equal(_expectedAttr.Name, _result.Headers["Name"]);
        }
    }
}