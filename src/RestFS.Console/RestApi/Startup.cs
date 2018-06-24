using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using RestFS.Console.Storage;

namespace RestFS.Console.RestApi
{
    internal class Startup
    {
        private readonly ILogger  _logger;
        private readonly IStorage _storage;

        public Startup(ILogger logger, IStorage storage)
        {
            _storage = storage;
            _logger  = logger;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(
                opt => opt.Bootstrapper = new Bootstrapper(
                    _logger,
                    _storage
                )));
        }
    }
}