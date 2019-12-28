using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Nancy;

namespace RestFS.Console_Test.RestApi
{
    public partial class CreateFile
    {
        [Scenario]
        public void Create_a_new_file_without_content()
        {
            Runner.RunScenario(
                _ => Given_a_file("not/existing/file.ext", new byte[0]),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_put_on_file_is_invoked(false),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_file_is_created_on_storage());
        }

        [Scenario]
        public void Create_a_new_file_with_content()
        {
            Runner.RunScenario(
                _ => Given_a_file("not/existing/file.ext", new byte[0]),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_put_on_file_is_invoked(false),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent),
                _ => Then_file_is_created_on_storage());
        }

        [Scenario]
        public void Create_an_existent_file_with_overwrite_false()
        {
            Runner.RunScenario(
                _ => Given_a_file("existing/file.ext", new byte[] {0x22, 0x33}),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_put_on_file_is_invoked(false),
                _ => Then_status_code_is_returned(HttpStatusCode.Conflict));
        }

        [Scenario]
        public void Create_an_existent_file_with_overwrite_true()
        {
            Runner.RunScenario(
                _ => Given_a_file("existing/file.ext", new byte[] {0x22, 0x33}),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_put_on_file_is_invoked(true),
                _ => Then_status_code_is_returned(HttpStatusCode.NoContent));
        }

        [Scenario]
        public void Wrong_query_parameter_set()
        {
            Runner.RunScenario(
                _ => Given_a_file(null, new byte[] {0x22, 0x33}),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_put_on_file_is_invoked(true),
                _ => Then_status_code_is_returned(HttpStatusCode.BadRequest));
        }
    }
}