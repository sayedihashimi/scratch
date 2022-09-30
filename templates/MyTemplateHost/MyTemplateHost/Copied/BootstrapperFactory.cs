using System;
using System.Collections.Generic;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Abstractions.TemplatePackage;
using Microsoft.TemplateEngine.Edge;

namespace MyTemplateHost;

internal static class BootstrapperFactory {
    private const string HostIdentifier = "Sayedha.SampleHost";
    private const string HostVersion = "v1.0.0";

    internal static Bootstrapper GetBootstrapper(IEnumerable<string> additionalVirtualLocations = null, bool loadBuiltInTemplates = true) {
        ITemplateEngineHost host = CreateHost(loadBuiltInTemplates);
        if (additionalVirtualLocations != null) {
            foreach (string virtualLocation in additionalVirtualLocations) {
                host.VirtualizeDirectory(virtualLocation);
            }
        }
        var res = new Bootstrapper(host, virtualizeConfiguration: true, loadDefaultComponents: true);
        res.LoadDefaultComponents();
        return res;
    }

    private static ITemplateEngineHost CreateHost(bool loadBuiltInTemplates = true) {
        var preferences = new Dictionary<string, string>
        {
                { "prefs:language", "C#" }
        };

        var builtIns = new List<(Type, IIdentifiedComponent)>();
        if (loadBuiltInTemplates) {

            // from: https://github.com/dotnet/sdk/blob/60aaae761755ecbff3971733150eb79d9f10427b/src/Cli/dotnet/commands/dotnet-new/NewCommandShim.cs#L61-L86
            // TODO: doesn't seem to be working
            builtIns.Add((typeof(ITemplatePackageProviderFactory), new BuiltInTemplatePackagesProviderFactory()));

            builtIns.Add((typeof(ITemplatePackageProviderFactory), new GlobalSettingsTemplatePackageProviderFactory()));
            // (typeof(ITemplatePackageProviderFactory), new GlobalSettingsTemplatePackageProviderFactory())

            // TODO: revisit adding this later
            // builtIns.Add((typeof(ITemplatePackageProviderFactory), new OptionalWorkloadProviderFactory()));
        }

        var host = new DefaultTemplateEngineHost(HostIdentifier + Guid.NewGuid().ToString(), HostVersion, preferences, builtIns, Array.Empty<string>());
        return host;
    }
}
