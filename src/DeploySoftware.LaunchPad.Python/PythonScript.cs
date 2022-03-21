using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Python
{
    [Serializable()]
    public partial class PythonScript
    {
        public string FolderPath { get; set; } = string.Empty;

        private string _fileName = string.Empty;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (!value.EndsWith(".py"))
                {
                    _fileName = value + ".py";
                }
            }
        }

        public PythonScript()
        {
        }

        public PythonScript(string scriptFileName)
        {
            FileName = scriptFileName;
        }

    }
}
