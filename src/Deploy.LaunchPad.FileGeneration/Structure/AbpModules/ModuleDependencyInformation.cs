using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.AbpModules
{
    public partial class ModuleDependencyInformation
    {
        public virtual IDictionary<string, string> BaseDependencies { get; init; }

        public virtual IDictionary<string, string> CustomDependencies { get; set; }

        public ModuleDependencyInformation()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            BaseDependencies = new Dictionary<string, string>(comparer);
            CustomDependencies = new Dictionary<string, string>(comparer);
        }
    }
}
