using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class DeleteFile : FeatureFixture
    {
        private const string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       Browser         _browser;
        private       string          _file;
        private       BrowserResponse _result;
        private       Mock<IStorage>  _storage;

        private void Given_a_file(string file)
        {
            _file = file;
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

        private void When_delete_on_file_is_invoked()
        {
            _result = _browser.Delete(Route, with =>
            {
                with.Query("file", _file);
                with.HttpRequest();
            }).GetAwaiter().GetResult();
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _result.StatusCode);
        }

        private void Then_the_file_is_deleted()
        {
            _storage.Verify(s => s.DeleteFile(_file), Times.Once);
        }
    }
}