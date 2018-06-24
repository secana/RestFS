# RestFS

>RestFS provides an easy way to export a directory on the file system through a REST API.

All typical CRUD operations are supported.

>- **C**reate a file or directory
>- **R**ead a file or directory
>- **U**pdate a file or directory
>- **D**elete a file or directory

___

## Table of Content

 1. [Run RestFS](#run-restfs)  
  1.1 [Docker Container](#docker-container)  
 2. [API Reference](#api-reference)
 3. [Configuration](#configuration)  
  3.1 [appsettings.json](#appsettingsjson)  
  3.2 [Environment Variables](#environment-variables)  
  3.3 [Commandline Arguments](#commandline-arguments)
___

## Run RestFS

There are different ways to run RestFS.

 1. [Docker Container](#docker-container)  

### Docker Container

To run RestFS in as a Docker container on your host run which lets RestFS export your local directory */host/dir* through the REST API run:

```bash
docker run -i -p 8080:8080 -v /host/dir:/restfs/dir -e=RootDirectory=/restfs/dir secana/restfs:latest
```

___

## API Reference

The API reference is created with [Swagger](www.swagger.io). The docu is exposed through the Swagger UI and the Swagger JSON description.

**API Docs**: `[your host and port]/api/v1/api-docs`

If your RestFS, for example, runs on your localhost and port 8080 the API reference is reachable under `http://localhost:8080/api/v1/api-docs`

___

## Configuration

RestFS provides three ways to set the configuration. Where the following one overwrites the previous one.

 1. [appsettings.json](#appsettingsjson)  
 2. [Environment Variables](#environment-variables)  
 3. [Commandline Arguments](#commandline-arguments)

The avialable configuration values are:

|Config value | Description |
|-------------|-------------|
|LoggerName   |The name of the logger used by RestFS. Change this if you push the logs in a central log aggregator and need to identify the logs from RestFS. |
|RootDirectory|The directory which should be exported through the REST API. All CRUD operation will use this directory as their root. |
|Uri          |The URI where the RestFS should listen on. You can reach the API over this Uri. |

### appsettings.json

Per default an *appsettings.json* file resides in the same folder as the RestFS binary. You can overwrite the *appsettings.json* with out own file to change the configuration.

If you want to overwrite the *appsettings.json* in the Docker container, the location is `/data/appsettings.json`.

Example:

```json
{
  "LoggerName": "RestFS",
  "RootDirectory": "./",
  "Uri": "http://localhost:8080"
}
```

### Environment Variables

The settings from the *appsettings.json* can be overwritten by environment variables.

Example PowerShell:

```powershell
$env:LoggerName = "RestFS"
$env:RootDirectory = "./"
$env:Uri = "http://localhost:8080"
```

Example Bash:

```bash
export LoggerName="RestFS"
export RootDirectory="./"
export Uri="http://localhost:8080"
```

### Commandline Arguments

The environment variables and the *appsettings.json* can be overwritten by commandline parameters.

Example:

```powershell
dotnet RestFS LoggerName=RestFS RootDirectory=./ Uri=http://localhost:8080
```