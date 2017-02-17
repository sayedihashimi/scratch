$ dotnet new -h
Template Instantiation Commands for .NET Core CLI.

Usage: dotnet new [arguments] [options]

Arguments:
  template  The template to instantiate.

Options:
  -l|--list         List templates containing the specified name.
  -lang|--language  Specifies the language of the template to create
  -n|--name         The name for the output being created. If no name is specified, the name of the current directory is used.
  -o|--output       Location to place the generated output.
  -h|--help         Displays help for this command.
  -all|--show-all   Shows all templates


Templates                                 Short Name      Language        Tags
----------------------------------------------------------------------------------------
Basic ASP.NET Core Web App                basicweb        [C#], [C#]      ASP.NET
Console Application                       console         [C#], F#        Common/Console
Class library                             classlib        [C#], F#        Common/Library
Unit Test Project                         mstest          [C#], F#        Test/MSTest
xUnit Test Project                        xunit           [C#], F#        Test/xUnit
Empty ASP.NET Core Web Application        web             [C#]            Web/Empty
MVC ASP.NET Core Web Application          mvc             [C#], F#        Web/MVC
Web API ASP.NET Core Web Application      webapi          [C#]            Web/WebAPI
GitFiles                                  gitfiles                        Git
Solution File                             sln                             Solution

Examples:
    dotnet new mvc --auth None --framework netcoreapp1.0
    dotnet new console --framework netcoreapp1.0
    dotnet new --help