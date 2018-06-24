using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.ModelBinding;
using RestFS.Console.Storage;

namespace RestFS.Console.RestApi.Module
{
    public sealed partial class FileSystemModule : NancyModule
    {
        private const    string   ApiVersion = "/api/v1";
        private const    string   Route      = ApiVersion + "/fs";
        private const    string   Docu       = ApiVersion + "/api-docs";
        private readonly ILogger  _logger;
        private readonly IStorage _storage;

        public FileSystemModule(ILogger logger, IStorage storage)
        {
            _logger  = logger;
            _storage = storage;

            Get("/", _ => Response.AsRedirect(Docu));

            Get(Docu, _ => View["index.html"]);

            Get(Route, async param =>
            {
                var query = this.Bind<QueryParameter>();

                if (!string.IsNullOrEmpty(query.File) && string.IsNullOrEmpty(query.Directory))
                    return await ReadFileAsync(query.File);

                if (string.IsNullOrEmpty(query.File))
                    return await ListDirectoryAsync(query.Directory ?? "");

                return new Response().StatusCode = HttpStatusCode.BadRequest;
            }, null, "Read");

            Delete(Route, param =>
            {
                var query = this.Bind<QueryParameter>();

                if (!string.IsNullOrEmpty(query.File) && string.IsNullOrEmpty(query.Directory))
                    return DeleteFile(query.File);

                if (string.IsNullOrEmpty(query.File) && !string.IsNullOrEmpty(query.Directory))
                    return DeleteDirectory(query.Directory);

                return new Response().StatusCode = HttpStatusCode.BadRequest;
            }, null, "Delete");

            Head(Route, param =>
            {
                var query = this.Bind<QueryParameter>();

                if (!string.IsNullOrEmpty(query.File) && string.IsNullOrEmpty(query.Directory))
                    return ReadFileInfo(query.File);

                if (string.IsNullOrEmpty(query.File) && !string.IsNullOrEmpty(query.Directory))
                    return ReadDirectoryInfo(query.Directory);

                return new Response().StatusCode = HttpStatusCode.BadRequest;
            }, null, "Info");

            Put(Route, async param =>
                {
                    var query   = this.Bind<QueryParameter>();
                    var length  = (int) Context.Request.Body.Length;
                    var content = new byte[length];
                    Context.Request.Body.Read(content, 0, length);

                    if (!string.IsNullOrEmpty(query.File) && string.IsNullOrEmpty(query.Directory))
                        return await WriteFileAsync(query.File, content, query.Overwrite);

                    if (string.IsNullOrEmpty(query.File) && !string.IsNullOrEmpty(query.Directory))
                        return CreateDirectory(query.Directory, query.Overwrite);

                    return new Response().StatusCode = HttpStatusCode.BadRequest;
                },
                null,
                "Write"
            );
        }
    }
}