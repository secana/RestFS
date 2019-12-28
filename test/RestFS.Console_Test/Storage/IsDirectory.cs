using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class IsDirectory
    {
        [Scenario]
        public void Is_directory()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_directory(),
                _ => When_IsDirectory_is_invoked(),
                _ => Then_IsDirectory_returns_true());
        }

        [Scenario]
        public void Is_no_directory()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_no_directory(),
                _ => When_IsDirectory_is_invoked(),
                _ => Then_IsDirectory_returns_false());
        }
    }
}