using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class FileExists
    {
        [Scenario]
        public void Exists_existent_file()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_existent_file(),
                _ => When_FileExists_is_invoked(),
                _ => Then_FileExists_returns_true());
        }

        [Scenario]
        public void Exists_nonexistent_file()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_non_existing_file(),
                _ => When_FileExists_is_invoked(),
                _ => Then_FileExists_returns_false());
        }
    }
}