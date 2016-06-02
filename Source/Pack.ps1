$currentDirectoryPath = (Get-Item -Path '.\').FullName;
dotnet pack 'MVC6\Boilerplate.AspNetCore' --configuration 'Release' --output $currentDirectoryPath
dotnet pack 'MVC6\Boilerplate.AspNetCore.Razor' --configuration 'Release' --output $currentDirectoryPath
dotnet pack 'MVC6\Boilerplate.AspNetCore.TagHelpers' --configuration 'Release' --output $currentDirectoryPath