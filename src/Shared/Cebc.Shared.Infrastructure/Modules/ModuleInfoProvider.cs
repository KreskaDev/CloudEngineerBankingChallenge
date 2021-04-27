using System.Collections.Generic;

namespace Cebc.Shared.Infrastructure.Modules
{
    public class ModuleInfoProvider
    {
        public List<ModuleInfo> Modules { get; } = new();
    }
    public record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
}