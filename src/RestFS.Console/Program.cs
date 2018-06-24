using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using RestFS.Console.RestApi;

namespace RestFS.Console
{
    internal static class Program
    {
        private static readonly AutoResetEvent Closing = new AutoResetEvent(false);

        private static void Main(string[] args)
        {
            DI.Register(args);
            var logger = DI.Container.Resolve<ILogger>();
            var config = DI.Container.Resolve<Config.Config>();
            var host   = DI.Container.Resolve<WebHost>();

            logger.LogInformation(LogEvents.ServiceStart, $"Service started on {config.Uri}");
            host.Start();

            System.Console.CancelKeyPress += OnExit;
            Closing.WaitOne();
        }

        private static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Closing.Set();
        }
    }
}