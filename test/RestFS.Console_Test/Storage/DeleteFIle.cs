using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class DeleteFile
    {
        [Scenario]
        public void Delete_existent_file()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_existent_file(),
                _ => When_DeleteFile_is_invoked(),
                _ => Then_file_is_removed());
        }

        [Scenario]
        public void Delete_existent_directory()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_existent_directory(),
                _ => When_DeleteDirectory_is_invoked(),
                _ => Then_directory_is_removed());
        }
    }
}