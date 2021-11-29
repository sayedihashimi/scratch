﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.TemplateEngine.Abstractions.TemplatePackage;

namespace MyTemplateHost;

internal class BuiltInTemplatePackagesProviderFactory : ITemplatePackageProviderFactory {
    public string DisplayName => "Sayedha.SampleHost BuiltIn";

    public Guid Id { get; } = new Guid("{6717BAD2-2556-49F4-B3D6-EAB91BEB3B82}");

    public ITemplatePackageProvider CreateProvider(IEngineEnvironmentSettings settings) {
        return new BuiltInTemplatePackagesProvider(this, settings);
    }

    private class BuiltInTemplatePackagesProvider : ITemplatePackageProvider {
        private readonly IEngineEnvironmentSettings settings;

        public BuiltInTemplatePackagesProvider(BuiltInTemplatePackagesProviderFactory factory, IEngineEnvironmentSettings settings) {
            this.settings = settings;
            this.Factory = factory;
        }

        event Action ITemplatePackageProvider.TemplatePackagesChanged {
            add { }
            remove { }
        }

        public ITemplatePackageProviderFactory Factory { get; }

        public Task<IReadOnlyList<ITemplatePackage>> GetAllTemplatePackagesAsync(CancellationToken cancellationToken) {
            List<ITemplatePackage> toInstallList = new List<ITemplatePackage>();
            string codebase = typeof(BootstrapperFactory).GetTypeInfo().Assembly.Location;
            Uri cb = new Uri(codebase);
            string asmPath = cb.LocalPath;
            string dir = Path.GetDirectoryName(asmPath);
            string[] locations = new[]
            {
                Path.Combine(dir, "..", "..", "..", "..", "..", "template_feed"),
                Path.Combine(dir, "..", "..", "..", "..", "..", "test", "Microsoft.TemplateEngine.TestTemplates", "test_templates")
            };

            foreach (string location in locations) {
                if (Directory.Exists(location)) {
                    toInstallList.Add(new TemplatePackage(this, new DirectoryInfo(location).FullName, File.GetLastWriteTime(location)));
                }
            }
            return Task.FromResult((IReadOnlyList<ITemplatePackage>)toInstallList);
        }
    }
}