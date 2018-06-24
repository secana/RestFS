using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Config
{
    public partial class Config
    {
        [Scenario]
        public void Appsettings_config_given()
        {
            Runner.RunScenario(
                _ => Given_an_appsettings_file(),
                _ => When_new_config_is_created(),
                _ => Then_the_right_config_values_are_used(
                    "TestLogger",
                    "/test/root/dir",
                    "http://test.host:8080"));
        }

        [Scenario]
        public void Appsettings_and_environment_config_given()
        {
            Runner.RunScenario(
                _ => Given_an_appsettings_file(),
                _ => Given_environment_variables(),
                _ => When_new_config_is_created(),
                _ => Then_the_right_config_values_are_used(
                    "ETestLogger",
                    "/env/root/dir",
                    "http://env.test.host:8080"));
        }

        [Scenario]
        public void Appsettings_and_environment_and_commandline_config_given()
        {
            Runner.RunScenario(
                _ => Given_an_appsettings_file(),
                _ => Given_environment_variables(),
                _ => Given_commandline_arguments(),
                _ => When_new_config_is_created(),
                _ => Then_the_right_config_values_are_used(
                    "CTestLogger",
                    "/cmd/root/dir",
                    "http://cmd.test.host:8080"));
        }
    }
}