using System;
using System.IO;
using LightBDD.XUnit2;
using Xunit;

namespace RestFS.Console_Test.Config
{
    public partial class Config : FeatureFixture
    {
        private string[]              _args;
        private Console.Config.Config _config;

        private void Given_an_appsettings_file()
        {
            // Delete environment variable, else the second run will fail.
            Environment.SetEnvironmentVariable("LoggerName", null);
            Environment.SetEnvironmentVariable("RootDirectory", null);
            Environment.SetEnvironmentVariable("Uri", null);
            _args = new string[0];

            if (!File.Exists("appsettings.json"))
                throw new Exception("appsettings.json not found for config test.");
        }

        private void Given_environment_variables()
        {
            Environment.SetEnvironmentVariable("LoggerName", "ETestLogger");
            Environment.SetEnvironmentVariable("RootDirectory", "/env/root/dir");
            Environment.SetEnvironmentVariable("Uri", "http://env.test.host:8080");
            _args = new string[0];
        }

        private void Given_commandline_arguments()
        {
            _args = new[]
            {
                "LoggerName=CTestLogger",
                "RootDirectory=/cmd/root/dir",
                "Uri=http://cmd.test.host:8080"
            };
        }

        private void When_new_config_is_created()
        {
            _config = new Console.Config.Config(_args);
        }

        private void Then_the_right_config_values_are_used(
            string logger,
            string rootDir,
            string uri)
        {
            Assert.Equal(logger, _config.LoggerName);
            Assert.Equal(rootDir, _config.RootDirectory);
            Assert.Equal(uri, _config.Uri);
        }
    }
}