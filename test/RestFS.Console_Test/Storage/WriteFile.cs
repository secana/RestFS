using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class WriteFile
    {
        [Scenario]
        public void Write_file()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_existent_folder_structure(),
                _ => When_WriteFile_is_invoked_Async().GetAwaiter().GetResult(),
                _ => Then_file_with_correct_content_is_created());
        }
    }
}