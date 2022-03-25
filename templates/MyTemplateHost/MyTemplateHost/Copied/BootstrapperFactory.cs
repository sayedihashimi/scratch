using System;
using System.Collections.Generic;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Abstractions.TemplatePackage;
using Microsoft.TemplateEngine.Edge;

namespace MyTemplateHost;

internal static class BootstrapperFactory {
    private const string HostIdentifier = "Sayedha.SampleHost";
    private const string HostVersion = "v1.0.0";

    internal static Bootstrapper GetBootstrapper(IEnumerable<string> additionalVirtualLocations = null, bool loadBuiltInTemplates = false) {
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

    private static ITemplateEngineHost CreateHost(bool loadBuiltInTemplates = false) {
        var preferences = new Dictionary<string, string>
        {
                { "prefs:language", "C#" }
        };

        var builtIns = new List<(Type, IIdentifiedComponent)>();
        if (loadBuiltInTemplates) {
            // TODO: is this needed?
            builtIns.Add((typeof(ITemplatePackageProviderFactory), new BuiltInTemplatePackagesProviderFactory()));

            // from: https://github.com/dotnet/sdk/blob/60aaae761755ecbff3971733150eb79d9f10427b/src/Cli/dotnet/commands/dotnet-new/NewCommandShim.cs#L61-L86

            builtIns.Add((typeof(ITemplatePackageProviderFactory), new BuiltInTemplatePackageProviderFactory()));
            builtIns.Add((typeof(ITemplatePackageProviderFactory), new OptionalWorkloadProviderFactory()));

            //builtIns.AddRange(Microsoft.TemplateEngine.Orchestrator.RunnableProjects.Components.AllComponents);
            //builtIns.AddRange(Microsoft.TemplateEngine.Edge.Components.AllComponents);
            //builtIns.AddRange(Components.AllComponents);
            //builtIns.AddRange(Microsoft.TemplateSearch.Common.Components.AllComponents);
        }

        var host = new DefaultTemplateEngineHost(HostIdentifier + Guid.NewGuid().ToString(), HostVersion, preferences, builtIns, Array.Empty<string>());
        return host;
    }
}
