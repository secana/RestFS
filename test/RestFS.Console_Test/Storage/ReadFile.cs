using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class ReadFile
    {
        [Scenario]
        public void Read_file()
        {
            Runner.RunScenario(
                _ => Given_a_initialized_storage(),
                _ => Given_a_existent_file(),
                _ => When_ReadFile_is_invoked_Async().GetAwaiter().GetResult(),
                _ => Then_correct_file_content_is_read());
        }
    }
}