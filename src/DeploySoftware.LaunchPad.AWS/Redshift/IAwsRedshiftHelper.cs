﻿using Amazon.Redshift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Redshift
{
    public interface IAwsRedshiftHelper : IAwsHelper<AmazonRedshiftConfig>
    {
    }
}
