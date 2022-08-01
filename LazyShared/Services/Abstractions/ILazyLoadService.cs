using System.Reflection;

namespace LazyShared.Services.Abstractions
{
    public interface ILazyLoadService
    {
        Task<IEnumerable<Assembly>> LoadAssembliesAsync(string navigationPath, CancellationToken cancellationToken = default);
    }
}
