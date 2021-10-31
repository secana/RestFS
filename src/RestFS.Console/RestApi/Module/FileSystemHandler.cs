using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nancy;
using Newtonsoft.Json;

namespace RestFS.Console.RestApi.Module
{
    public sealed partial class FileSystemModule
    {
        private Response ReadFileInfo(string file)
        {
            if (!_storage.FileExists(file))
            {
                _logger.LogWarning(LogEvents.NotFound, $"File not found: {file}");
                return HttpStatusCode.NotFound;
            }

            var attr = _storage.ReadFileAttributes(file);
            _logger.LogTrace(LogEvents.ReadInfo, $"File info for: {file}");

            return new Response
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers    = attr.ToDictionary()
            };
        }

        private Response ReadDirectoryInfo(string directory)
        {
            if (!_storage.DirectoryExists(directory))
            {
                _logger.LogWarning(LogEvents.NotFound, $"Directory not found: {directory}");
                return HttpStatusCode.NotFound;
            }

            var attr = _storage.ReadDirAttributes(directory);
            _logger.LogTrace(LogEvents.ReadInfo, $"Directory info for: {directory}");

            return new Response
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers    = attr.ToDictionary()
            };
        }

        private Response DeleteFile(string file)
        {
            if (!_storage.FileExists(file))
            {
                _logger.LogWarning(LogEvents.NotFound, $"File not found: {file}");
                return HttpStatusCode.NotFound;
            }

            _storage.DeleteFile(file);
            _logger.LogTrace(LogEvents.Delete, $"Delete file: {file}");

            return HttpStatusCode.NoContent;
        }

        private Response DeleteDirectory(string directory)
        {
            if (!_storage.DirectoryExists(directory))
            {
                _logger.LogWarning(LogEvents.NotFound, $"Directory not found: {directory}");
                return HttpStatusCode.NotFound;
            }

            _storage.DeleteDirectory(directory);
            _logger.LogTrace(LogEvents.Delete, $"Delete directory: {directory}");

            return HttpStatusCode.NoContent;
        }

        private async Task<Response> WriteFileAsync(string file, byte[] content, bool overwrite)
        {
            if (_storage.FileExists(file) && overwrite == false)
            {
                _logger.LogWarning(LogEvents.NotFound, $"File already exists: {file}");
                return HttpStatusCode.Conflict;
            }

            await _storage.WriteFileAsync(file, content);
            _logger.LogTrace(LogEvents.Write, $"Write file: {file}");

            return HttpStatusCode.NoContent;
        }

        private Response CreateDirectory(string directory, bool overwrite)
        {
            if (_storage.DirectoryExists(directory) && overwrite == false)
            {
                _logger.LogWarning(LogEvents.NotFound, $"Directory already exists: {directory}");
                return HttpStatusCode.Conflict;
            }

            _storage.CreateDirectory(directory);
            _logger.LogTrace(LogEvents.Write, $"Create directory: {directory}");

            return HttpStatusCode.NoContent;
        }

        private async Task<Response> ListDirectoryAsync(string dir)
        {
            if (!_storage.DirectoryExists(dir))
            {
                _logger.LogTrace(LogEvents.NotFound, $"Directory not found: {dir}");
                return HttpStatusCode.NotFound;
            }

            var list       = await _storage.ListDirectoryAsync(dir);
            var content    = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list));
            var attributes = _storage.ReadDirAttributes(dir);
            _logger.LogTrace(LogEvents.Read, $"List directory: {dir}");
            return new Response
            {
                ContentType = "application/json",
                Contents    = s => { s.WriteAsync(content, 0, content.Length); },
                Headers     = attributes.ToDictionary(),
                StatusCode  = HttpStatusCode.OK
            };
        }

        private async Task<Response> ReadFileAsync(string file)
        {
            if (!_storage.FileExists(file))
            {
                _logger.LogTrace(LogEvents.NotFound, $"File not found: {file}");
                return HttpStatusCode.NotFound;
            }

            var content    = await _storage.ReadFileAsync(file);
            var attributes = _storage.ReadFileAttributes(file);
            _logger.LogTrace(LogEvents.Read, $"Read file: {file}");

            return new Response
            {
                Contents    = s => { s.Write(content, 0, content.Length); },
                ContentType = "application/octet-stream",
                Headers     = attributes.ToDictionary(),
                StatusCode  = HttpStatusCode.OK
            };
        }
    }
}