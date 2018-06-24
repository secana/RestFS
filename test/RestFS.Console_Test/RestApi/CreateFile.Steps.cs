using System.IO;
using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class CreateFile : FeatureFixture
    {
        private const string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       Browser         _browser;
        private       byte[]          _expectedContent;
        private       string          _file;
        private       BrowserResponse _result;
        private       Mock<IStorage>  _storage;

        private void Given_a_file(string file, byte[] content)
        {
            _file            = file;
            _expectedContent = content;
        }

        private void Given_a_storage(bool containsFile)
        {
            _storage = new Mock<IStorage>();
            _storage.Setup(s => s.FileExists(_file)).Returns(containsFile);
        }

        private void Given_a_fake_browser()
        {
            _browser = Helper.CreateBrowser(_storage.Object);
        }

        private void When_put_on_file_is_invoked(bool overwrite)
        {
            _result = _browser.Put(Route, with =>
            {
                with.Query("file", _file);
                with.Query("overwrite", overwrite.ToString());
                with.HttpRequest();
                with.Body(new MemoryStream(_expectedContent));
            }).GetAwaiter().GetResult();
        }

        private void Then_file_is_created_on_storage()
        {
            _storage.Verify(s => s.WriteFileAsync(_file, _expectedContent), Times.Once);
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _result.StatusCode);
        }
    }
}