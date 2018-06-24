using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Moq;
using Nancy.Testing;
using RestFS.Console.RestApi;
using RestFS.Console.Storage;
using Xunit;

namespace RestFS.Console_Test.RestApi
{
    public static class Helper
    {
        public static Browser CreateBrowser(IStorage storage)
        {
            var logger       = new Mock<ILogger>();
            var bootstrapper = new Bootstrapper(logger.Object, storage);
            var browser      = new Browser(bootstrapper);

            return browser;
        }

        public static FileAttributes ExtractFileAttribures(this BrowserResponse response)
        {
            var dict = response.Headers;
            return new FileAttributes(
                dict["Name"],
                long.Parse(dict["Size"]),
                DateTime.ParseExact(dict["LastWriteTime"], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                bool.Parse(dict["IsDir"]));
        }

        public static void CompareFileAttributes(FileAttributes expected, FileAttributes actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.IsDir, actual.IsDir);
            Assert.Equal(expected.LastWriteTime, actual.LastWriteTime);
            Assert.Equal(expected.Size, actual.Size);
        }

        public static void CompareByteArrays(byte[] expected, byte[] actual)
        {
            if (expected.Length != actual.Length)
                throw new ArgumentException("Arrays are expected to be of equal size.");

            for (var i = 0; i < expected.Length; i++)
                Assert.True(expected[i] == actual[i],
                    $"Expected: '{expected[i]}', Actual: '{actual[i]}' at offset {i}."
                );
        }
    }
}