using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Swagger.Services;
using Nancy.TinyIoc;
using RestFS.Console.Storage;
using Swagger.ObjectModel;

namespace RestFS.Console.RestApi
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly ILogger  _logger;
        private readonly IStorage _storage;

        public Bootstrapper(ILogger logger, IStorage storage)
        {
            _storage = storage;
            _logger  = logger;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            SwaggerMetadataProvider.SetInfo(
                "RestFs",                                 //Name
                "v1",                                     //Version
                "REST API abstraction for a file system", //Description
                new Contact {Url = "https://github.com/secana/RestFS"} //Contact Info
            );

            container.Register(_logger);
            container.Register(_storage);
            base.ApplicationStartup(container, pipelines);
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Routing(explicitHeadRouting: true);
            base.Configure(environment);
        }
    }
}