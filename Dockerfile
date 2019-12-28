FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine

COPY artifacts/RestFS.Console/ /data
WORKDIR /data
ENTRYPOINT ./RestFS.Console
