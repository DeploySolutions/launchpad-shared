﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Redshift
{
    public partial class AwsRedshiftHelper :  IAwsRedshiftHelper
    {
        public AwsCommonHelper AwsCommonHelper { get; set; }
    }
}