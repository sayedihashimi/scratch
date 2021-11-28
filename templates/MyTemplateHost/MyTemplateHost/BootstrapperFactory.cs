using System;
using System.Collections.Generic;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Abstractions.TemplatePackage;
using Microsoft.TemplateEngine.Edge;

namespace MyTemplateHost;

internal static class BootstrapperFactory {
    private const string HostIdentifier = "IDE.IntegrationTests";
    private const string HostVersion = "v1.0.0";

    internal static Bootstrapper GetBootstrapper(IEnumerable<string> additionalVirtualLocations = null, bool loadBuiltInTemplates = false) {
        ITemplateEngineHost host = CreateHost(loadBuiltInTemplates);
        if (additionalVirtualLocations != null) {
            foreach (string virtualLocation in additionalVirtualLocations) {
                host.VirtualizeDirectory(virtualLocation);
            }
        }
        return new Bootstrapper(host, virtualizeConfiguration: true, loadDefaultComponents: true);
    }

    private static ITemplateEngineHost CreateHost(bool loadBuiltInTemplates = false) {
        var preferences = new Dictionary<string, string>
        {
                { "prefs:language", "C#" }
            };

        var builtIns = new List<(Type, IIdentifiedComponent)>();
        if (loadBuiltInTemplates) {
            builtIns.Add((typeof(ITemplatePackageProviderFactory), new BuiltInTemplatePackagesProviderFactory()));
        }

        return new DefaultTemplateEngineHost(HostIdentifier + Guid.NewGuid().ToString(), HostVersion, preferences, builtIns, Array.Empty<string>());
    }
}
