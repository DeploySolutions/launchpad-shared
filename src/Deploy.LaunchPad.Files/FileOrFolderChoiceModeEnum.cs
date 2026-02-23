using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// Allows a choice between manipulating files or folders.
    /// </summary>
    public enum FileOrFolderChoiceMode
    {
        [Description("Files")]
        Files,
        [Description("Folders")]
        Folders,
        [Description("Files and Folders")]
        FilesAndFolders
    }
}
