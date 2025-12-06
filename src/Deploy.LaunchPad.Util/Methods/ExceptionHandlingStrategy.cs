using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FactoryLite.Methods
{
    public enum ExceptionHandlingStrategy
    {
        ReturnResultWithError, // Return a Result.WithError
        ThrowException         // Throw an exception
    }
}
