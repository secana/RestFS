using System;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Nancy;
using RestFS.Console.Storage;

[assembly: LightBddScope]

namespace RestFS.Console_Test.RestApi
{
    public partial class ReadFile
    {
        [Scenario]
        public void Read_an_existent_file()
        {
            Runner.RunScenario(
                _ => Given_a_file("existing/file.ext",
                    new byte[] {0x1, 0x2},
                    new FileAttributes("existing/file.ext", 2, new DateTime(2018, 06, 12, 21, 11, 10), false)),
                _ => Given_a_storage(true),
                _ => Given_a_fake_browser(),
                _ => When_get_on_file_is_invoked(),
                _ => Then_file_attribures_are_in_the_header(),
                _ => Then_status_code_is_returned(HttpStatusCode.OK),
                _ => Then_expected_file_is_returned());
        }

        [Scenario]
        public void Read_a_nonexistent_file()
        {
            Runner.RunScenario(
                _ => Given_a_file("not/existing/file.ext", null, null),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_get_on_file_is_invoked(),
                _ => Then_status_code_is_returned(HttpStatusCode.NotFound)
            );
        }

        [Scenario]
        public void Wrong_query_parameter_set()
        {
            Runner.RunScenario(
                _ => Given_a_file(null, null, null),
                _ => Given_a_storage(false),
                _ => Given_a_fake_browser(),
                _ => When_get_on_file_is_invoked_with_wrong_parameters(),
                _ => Then_status_code_is_returned(HttpStatusCode.BadRequest)
            );
        }
    }
}