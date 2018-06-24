using System.Net;
using Nancy.Metadata.Modules;
using Nancy.Swagger;
using Swagger.ObjectModel;

namespace RestFS.Console.RestApi.Module
{
    public class FileSystemMetadataModule : MetadataModule<PathItem>
    {
        public FileSystemMetadataModule()
        {
            Describe["Read"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("Read")
                        .Parameter(p => p.In(ParameterIn.Query).Name("file").Description("The file to read."))
                        .Parameter(p => p.In(ParameterIn.Query).Name("directory").Description("The directory to read."))
                        .Summary("Reads the content of a file or directory.")
                        .Description(
                            "If a file is requested, the files content is returned. If a directory is requested, a directory listing is returned.")
                        .Tag("File System")
                        .Response(HttpStatusCode.OK, r => r.Description("File content or directory listing."))
                        .Response(HttpStatusCode.NotFound, r => r.Description("File or directory not found."))
                        .Response(HttpStatusCode.BadRequest, r => r.Description("Wrong query parameter."))
                ));

            Describe["Delete"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("Delete")
                        .Parameter(p => p.In(ParameterIn.Query).Name("file").Description("The file to delete."))
                        .Parameter(p =>
                            p.In(ParameterIn.Query).Name("directory").Description("The directory to delete."))
                        .Summary("Delete a file or directory.")
                        .Description(
                            "If a file is requested, the files deleted. If a directory is requested, the directory is deleted recursively.")
                        .Tag("File System")
                        .Response(HttpStatusCode.NoContent,
                            r => r.Description("File or directory sucessfully deleted."))
                        .Response(HttpStatusCode.NotFound, r => r.Description("File or directory not found."))
                        .Response(HttpStatusCode.BadRequest, r => r.Description("Wrong query parameter."))
                ));

            Describe["Info"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("Info")
                        .Parameter(p =>
                            p.In(ParameterIn.Query).Name("file").Description("The file to read the file info for."))
                        .Parameter(p =>
                            p.In(ParameterIn.Query).Name("directory")
                                .Description("The directory to read the directory info for."))
                        .Summary("Read file for directory information.")
                        .Description(
                            "Same as GET but without the body. File or directory information are retuned in the headers.")
                        .Tag("File System")
                        .Response(HttpStatusCode.NoContent, r => r.Description("File or directory info."))
                        .Response(HttpStatusCode.NotFound, r => r.Description("File or directory not found."))
                        .Response(HttpStatusCode.BadRequest, r => r.Description("Wrong query parameter."))
                ));

            Describe["Write"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("Write")
                        .Parameter(p => p.In(ParameterIn.Query).Name("file").Description("The file to write."))
                        .Parameter(p =>
                            p.In(ParameterIn.Query).Name("directory").Description("The directory to create."))
                        .Parameter(p =>
                            p.In(ParameterIn.Query).Name("overwrite").Description(
                                "If \"true\" overwrites a existing file or directory. Default is \"false\"."))
                        .Summary("Writes a file or creates a directory.")
                        .Description(
                            "If a file with optional content is given, the new file is created. If a directory is given, the directory is created.")
                        .Tag("File System")
                        .Response(HttpStatusCode.NoContent,
                            r => r.Description("File or directory successfully created."))
                        .Response(HttpStatusCode.Conflict,
                            r => r.Description("File or directory already exists and overwrite flag not set."))
                        .Response(HttpStatusCode.BadRequest, r => r.Description("Wrong query parameter."))
                ));
        }
    }
}