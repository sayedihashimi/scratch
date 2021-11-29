// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Abstractions.Installer;
using Microsoft.TemplateEngine.Abstractions.TemplatePackage;
using Microsoft.TemplateEngine.Edge.Settings;
using Microsoft.TemplateEngine.IDE;
using MyTemplateHost;

namespace MyTemplateHost;

class Program {
    private PackageManager _packageManager = new PackageManager();
    public static void Main(string[] args) {
        Console.WriteLine("Hello, World!");
        _ = new Program().RunAsync(args);
    }

    private async Task RunAsync(string[] args) {
        await ListInstalledTemplatesAsync();
        await InstallRemotePackage();

    }
    private async Task ListInstalledTemplatesAsync() {
        // TODO
        //  this will not return any results because there are no templates in the folders that it's looking at
        //  need to figure out a way to get the templates that are installed in the version of dotnet that is currently running
        using MyTemplateHost.Bootstrapper bootstrapper = BootstrapperFactory.GetBootstrapper(loadBuiltInTemplates: true);
        var result = await bootstrapper.GetTemplatesAsync(default).ConfigureAwait(false);
        Console.WriteLine($"Num templates installed: '{result.Count}'");
        foreach (var template in result) {
            Console.WriteLine($"\t{template.Identity}");
        }
    }
    private async Task InstallRemotePackage() {
        // try to install a nuget package
        using MyTemplateHost.Bootstrapper bootstrapper = BootstrapperFactory.GetBootstrapper();

        var packageName = "sayedha.templates";
        string packageLocation = await _packageManager.GetNuGetPackage(packageName).ConfigureAwait(false);
        Console.WriteLine($"downloaded package to: '{packageLocation}'");

        InstallRequest installRequest = new InstallRequest(
                        packageName,
                        "1.0.5",
                        details: new Dictionary<string, string>
                        {
                    { InstallerConstants.NuGetSourcesKey, "https://api.nuget.org/v3/index.json" }
                        });
        IReadOnlyList<InstallResult> result = await bootstrapper.InstallTemplatePackagesAsync(new[] { installRequest }, InstallationScope.Global, CancellationToken.None).ConfigureAwait(false);
        if (result.Count == 1) {
            Console.WriteLine($"Successfully installed package '{packageName}'");
        }
        else {
            Console.WriteLine($"Unknown error trying to install package '{packageName}'");
        }
    }
}




