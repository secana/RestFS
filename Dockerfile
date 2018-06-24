FROM microsoft/dotnet:2.1.1-runtime-deps-alpine3.7

COPY artifacts/RestFS.Console/ /data
WORKDIR /data
ENTRYPOINT ./RestFS.Console
