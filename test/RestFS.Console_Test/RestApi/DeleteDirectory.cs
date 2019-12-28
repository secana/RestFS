using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Nancy;

namespace RestFS.Console_Test.RestApi
{
    public partial class DeleteDirectory
    {
        [Scenario]
        public void Delete_an_existent_directory()
        {
            Runner.RunScenario(
                _ => Given_a_directory("existing/dir"),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_delete_on_directory_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_the_directory_is_deleted());
        }

        [Scenario]
        public void Delete_a_nonexistent_directory()
        {
            Runner.RunScenario(
                _ => Given_a_directory("not/existing/dir"),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_delete_on_directory_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NotFound));
        }
    }
}