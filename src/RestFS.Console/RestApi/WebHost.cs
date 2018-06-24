using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RestFS.Console.Storage;

namespace RestFS.Console.RestApi
{
    public class WebHost : IDisposable
    {
        private readonly ILogger  _logger;
        private readonly IStorage _storage;
        private readonly string   _url;
        private          IWebHost _webHost;

        public WebHost(
            ILogger logger,
            IStorage storage,
            string url)
        {
            _url     = url;
            _logger  = logger;
            _storage = storage;
        }

        public void Dispose()
        {
            _webHost?.Dispose();
        }

        private IWebHost BuildWebHost(params string[] urls)
        {
            var startup = new Startup(
                _logger,
                _storage
            );

            return new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls(urls)
                .Configure(startup.Configure)
                .Build();
        }

        public void Start()
        {
            if (_webHost == null)
                _webHost = BuildWebHost(_url);
            _webHost.Start();
        }
    }
}