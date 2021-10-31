using Microsoft.Extensions.Logging;
using Nancy.TinyIoc;
using RestFS.Console.RestApi;
using RestFS.Console.Storage;

namespace RestFS.Console
{
    public static class DI
    {
        public static TinyIoCContainer Container { get; }

        static DI()
        {
            Container = TinyIoCContainer.Current;
        }

        public static void Register(string[] args)
        {
            Container.Register(new Config.Config(args));

            var logConfig = new Microsoft.Extensions.Logging.Console.ConsoleLoggerOptions();

            Container.Register(
                LoggerFactory.Create(builder => 
                builder.AddConsole().
                SetMinimumLevel(LogLevel.Trace)));
            
            Container.Register(Container.Resolve<ILoggerFactory>().CreateLogger(Container.Resolve<Config.Config>().LoggerName));

            Container.Register<IStorage>(
                new Storage.Storage(Container.Resolve<Config.Config>().RootDirectory));

            Container.Register(new WebHost(
                Container.Resolve<ILogger>(),
                Container.Resolve<IStorage>(),
                Container.Resolve<Config.Config>().Uri));
        }
    }
}