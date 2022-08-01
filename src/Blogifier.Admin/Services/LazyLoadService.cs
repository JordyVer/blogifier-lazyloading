using LazyShared.Services.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Admin.Services
{
    public class LazyLoadService : ILazyLoadService
    {
        private readonly LazyAssemblyLoader _lazyAssemblyLoader;

        public LazyLoadService(LazyAssemblyLoader lazyAssemblyLoader)
        {
            _lazyAssemblyLoader = lazyAssemblyLoader ?? throw new ArgumentNullException(nameof(lazyAssemblyLoader));
        }

        public async Task<IEnumerable<Assembly>> LoadAssembliesAsync(string navigationPath, CancellationToken cancellationToken = default)
        {
            var assembliesToLoad = GetAssemblyNamesBasedOnPath(navigationPath);

            if (assembliesToLoad.Any())
            {
                return await _lazyAssemblyLoader.LoadAssembliesAsync(assembliesToLoad);
            }

            return Enumerable.Empty<Assembly>();
        }

        internal IEnumerable<string> GetAssemblyNamesBasedOnPath(string path)
        {
            var assemblyNames = Enumerable.Empty<string>();
            var result = DetermineAssemblyNames(path);

            if (string.IsNullOrEmpty(result))
                return assemblyNames;

            if (result.Contains(','))
                assemblyNames = result.Split(',').ToList();
            else
                assemblyNames = assemblyNames.Append(result);

            return assemblyNames;
        }

        private string DetermineAssemblyNames(string path)
        {
            return path switch
            {
                string when path.Contains("admin/settings") => "LazySettings.dll",
                string when path.Contains("admin/blog") => "LazyBlog.dll",
                string when path.Contains("admin/editor") => "LazyBlog.dll",
                string when path.Contains("admin/newsletter") => "LazyNewsletter.dll",
                _ => null
            };
        }
    }
}
