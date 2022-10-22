using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Jobs
{
    public interface ICanBeRunInBackgroundJob
    {
        public void Run();

    }
}
