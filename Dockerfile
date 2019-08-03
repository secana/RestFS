FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0.0-preview7-alpine3.9

COPY artifacts/RestFS.Console/ /data
WORKDIR /data
ENTRYPOINT ./RestFS.Console
