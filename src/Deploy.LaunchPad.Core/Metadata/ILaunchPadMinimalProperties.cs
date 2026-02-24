using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface ILaunchPadMinimalProperties :
        IMustHaveName,
        IMustHaveDescription
    {


        

    }
}
