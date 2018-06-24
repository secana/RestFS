using System.Threading.Tasks;
using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class ReadFile : FeatureFixture
    {
        public const  string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       byte[]          _actualFile;
        private       FileAttributes  _attributes;
        private       Browser         _browser;
        private       byte[]          _expectedFile;
        private       string          _file;
        private       BrowserResponse _result;
        private       Mock<IStorage>  _storage;

        private void Given_a_file(string file, byte[] content, FileAttributes attributes)
        {
            _expectedFile = content;
            _file         = file;
            _attributes   = attributes;
        }

        private void Given_a_storage(bool containsFile)
        {
            _storage = new Mock<IStorage>();
            _storage.Setup(s => s.ReadFileAsync(_file)).Returns(Task.FromResult(_expectedFile));
            _storage.Setup(s => s.FileExists(_file)).Returns(containsFile);
            _storage.Setup(s => s.ReadFileAttributes(_file)).Returns(_attributes);
        }

        private void Given_a_fake_browser()
        {
            _browser = Helper.CreateBrowser(_storage.Object);
        }

        private void When_get_on_file_is_invoked()
        {
            _result = _browser.Get(Route, with =>
            {
                with.Query("file", _file);
                with.HttpRequest();
            }).GetAwaiter().GetResult();
            _actualFile = new byte[_result.Body.AsStream().Length];
            _result.Body.AsStream().Read(_actualFile, 0, _actualFile.Length);
        }

        private void When_get_on_file_is_invoked_with_wrong_parameters()
        {
            _result = _browser.Get(Route, with =>
            {
                with.Query("file", "b");
                with.Query("directory", "a");
                with.HttpRequest();
            }).GetAwaiter().GetResult();
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _result.StatusCode);
        }

        private void Then_expected_file_is_returned()
        {
            Helper.CompareByteArrays(_expectedFile, _actualFile);
        }

        private void Then_file_attribures_are_in_the_header()
        {
            Helper.CompareFileAttributes(_attributes, _result.ExtractFileAttribures());
        }
    }
}