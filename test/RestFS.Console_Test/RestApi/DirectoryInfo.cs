using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using Nancy;

namespace RestFS.Console_Test.RestApi
{
    public partial class DirectoryInfo
    {
        [Scenario]
        public void Read_an_existent_directory_information()
        {
            Runner.RunScenario(
                _ => Given_a_directory("existing/dir"),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_head_on_directory_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_expected_directory_attributes_are_returned());
        }

        [Scenario]
        public void Read_a_nonexistent_directory_information()
        {
            Runner.RunScenario(
                _ => Given_a_directory("not/existing/dir"),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_head_on_directory_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NotFound));
        }
    }
}