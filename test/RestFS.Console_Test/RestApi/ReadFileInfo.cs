using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Nancy;

namespace RestFS.Console_Test.RestApi
{
    public partial class ReadFileInfo
    {
        [Scenario]
        public void Read_an_existent_file_information()
        {
            Runner.RunScenario(
                _ => Given_a_file("existing/file.ext"),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_Head_on_file_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_expected_file_attributes_are_returned()
            );
        }

        [Scenario]
        public void Read_a_nonexistent_file_information()
        {
            Runner.RunScenario(
                _ => Given_a_file("not/existing/file.ext"),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_Head_on_file_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NotFound)
            );
        }

        [Scenario]
        public void Wrong_query_parameter_set()
        {
            Runner.RunScenario(
                _ => Given_a_file(null),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_Head_on_file_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.BadRequest)
            );
        }
    }
}