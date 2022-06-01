using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Python
{
    [Serializable()]
    public partial class PythonScript : IPythonScript
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
                else
                {
                    _fileName = value;
                }
            }
        }

        protected PythonScript()
        {
        }

        public PythonScript(string scriptFilePath)
        {
            FileName = scriptFilePath;
            FolderPath = new FileInfo(scriptFilePath).Directory.FullName;
        }

    }
}
