using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class ListDirectory
    {
        [Scenario]
        public void List_directory()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_existent_directory_with_content(),
                _ => When_ListDirectory_is_invoked_Async().GetAwaiter().GetResult(),
                _ => Then_correct_directory_listing_is_returned());
        }
    }
}