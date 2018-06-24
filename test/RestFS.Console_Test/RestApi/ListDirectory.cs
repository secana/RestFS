using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using Nancy;

namespace RestFS.Console_Test.RestApi
{
    public partial class ListDirectory
    {
        [Scenario]
        public void List_an_existent_directory()
        {
            Runner.RunScenario(
                _ => Given_a_directory("existing/dir"),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_get_on_directory_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.OK),
                _ => Then_expected_directory_listing_is_returned(),
                _ => Then_expected_directory_attributes_are_returned());
        }

        [Scenario]
        public void List_a_nonexistent_directory()
        {
            Runner.RunScenario(
                _ => Given_a_directory("not/existing/dir"),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_get_on_directory_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NotFound));
        }
    }
}