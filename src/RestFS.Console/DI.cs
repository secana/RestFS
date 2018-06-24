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

            Container.Register(new LoggerFactory()
                .AddConsole(LogLevel.Trace)
                .CreateLogger(Container.Resolve<Config.Config>().LoggerName));

            Container.Register<IStorage>(
                new Storage.Storage(Container.Resolve<Config.Config>().RootDirectory));

            Container.Register<WebHost>(new WebHost(
                Container.Resolve<ILogger>(),
                Container.Resolve<IStorage>(),
                Container.Resolve<Config.Config>().Uri));
        }
    }
}