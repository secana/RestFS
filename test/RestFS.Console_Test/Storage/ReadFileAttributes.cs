using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace RestFS.Console_Test.Storage
{
    public partial class ReadFileAttributes
    {
        [Scenario]
        public void Read_file_attributes()
        {
            Runner.RunScenario(
                Given_a_initialized_storage,
                Given_a_existent_storage,
                When_ReadFileAttributes_is_invoked,
                Then_correct_attributes_are_returned);
        }
    }
}