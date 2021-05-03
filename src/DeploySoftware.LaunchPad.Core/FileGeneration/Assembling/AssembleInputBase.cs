
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class AssembleInputBase<TGeneratedSolution>
        where TGeneratedSolution : LaunchPadGeneratedSolution, new()
    {

        public TGeneratedSolution GeneratedSolutionDefinition { get; set; }

        /// <summary>
        /// List of exclusion files (or directories) to skip when assembling output structure. 
        /// For instance, system files, sensitive or development files.
        /// </summary>
        public IList<string> Exclusions { get; set; }

        /// <summary>
        /// If true, skip empty directories.
        /// </summary>
        public bool ExcludeEmptyDirectories { get; set; }

        /// <summary>
        /// If true, only assemble directories (tree) structure, without including any files.
        /// </summary>
        public bool AssembleDirectoriesOnly { get; set; }

    }
}
