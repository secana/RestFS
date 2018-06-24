namespace RestFS.Console.RestApi.Module
{
    public class QueryParameter
    {
        public string File      { get; set; }
        public string Directory { get; set; }
        public bool   Overwrite { get; set; } = false;
    }
}