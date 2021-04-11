using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        public void Assemble();

        public void Tokenize(); 
        
        public void Build();

        public void Publish();
    }
}
