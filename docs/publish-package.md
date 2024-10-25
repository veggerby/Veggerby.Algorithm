# Publish Nuget Package

## Manually using dotnet CLI

See <https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli1>

```sh
dotnet nuget push bin/Release/Veggerby.Algorithm.[version].nupkg --api-key [api-key] --source https://api.nuget.org/v3/index.json
```
