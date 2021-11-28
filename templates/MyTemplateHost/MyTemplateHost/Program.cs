// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TemplateEngine.Abstractions.Installer;
using Microsoft.TemplateEngine.Abstractions.TemplatePackage;
using Microsoft.TemplateEngine.Edge.Settings;
using Microsoft.TemplateEngine.IDE;
using MyTemplateHost;

Console.WriteLine("Hello, World!");
PackageManager _packageManager = new PackageManager();

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
if(result.Count == 1) {
    Console.WriteLine($"Successfully installed package '{packageName}'");
}
else {
    Console.WriteLine($"Unknown error trying to install package '{packageName}'");
}