﻿using LightBDD.XUnit2;
using Moq;
using Nancy;
using Nancy.Testing;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public partial class CreateDirectory : FeatureFixture
    {
        private const string          ApiVersion = "/api/v1";
        private const string          Route      = ApiVersion + "/fs";
        private       Browser         _browser;
        private       string          _directory;
        private       BrowserResponse _result;
        private       Mock<IStorage>  _storage;

        private void Given_a_directory(string directory)
        {
            _directory = directory;
        }

        private void Given_a_storage(bool directoryExists)
        {
            _storage = new Mock<IStorage>();
            _storage.Setup(s => s.DirectoryExists(_directory)).Returns(directoryExists);
        }


        private void Given_a_fake_browser()
        {
            _browser = Helper.CreateBrowser(_storage.Object);
        }

        private void When_put_on_directory_is_invoked(bool overwrite)
        {
            _result = _browser.Put(Route, with =>
            {
                with.Query("directory", _directory);
                with.Query("overwrite", overwrite.ToString());
                with.HttpRequest();
            }).GetAwaiter().GetResult();
        }

        private void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(statusCode, _result.StatusCode);
        }

        private void Then_directory_is_created_on_storage()
        {
            _storage.Verify(s => s.CreateDirectory(_directory), Times.Once);
        }

        private void Then_no_directory_is_created_on_storage()
        {
            _storage.Verify(s => s.CreateDirectory(_directory), Times.Never);
        }
    }
}