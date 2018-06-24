using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using Nancy;

namespace RestFS.Console_Test.RestApi
{
    public partial class CreateDirectory
    {
        [Scenario]
        public void Create_a_new_directory()
        {
            Runner.RunScenario(
                _ => Given_a_directory("non/existing/dir"),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_put_on_directory_is_invoked(false),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_directory_is_created_on_storage());
        }

        [Scenario]
        public void Create_an_existent_directory_with_overwrite_false()
        {
            Runner.RunScenario(
                _ => Given_a_directory("existing/dir"),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_put_on_directory_is_invoked(false),
                _ => Then_status_code_is_returned(HttpStatusCode.Conflict),
                _ => Then_no_directory_is_created_on_storage());
        }

        [Scenario]
        public void Create_an_existent_directory_with_overwrite_true()
        {
            Runner.RunScenario(
                _ => Given_a_directory("existing/dir"),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_put_on_directory_is_invoked(true),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_directory_is_created_on_storage());
        }
    }
}