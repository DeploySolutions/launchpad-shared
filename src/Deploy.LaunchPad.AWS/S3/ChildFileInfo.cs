using Deploy.LaunchPad.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public class ChildFileInfo : S3FileInfo
    {
        public virtual ILaunchPadDataFact RegularFact { get; set; }
        public virtual ILaunchPadDataFact OutlierFact { get; set; }

        public bool IsOutlierFact()
        {
            bool isOutlier = false;
            if (OutlierFact != null)
            {
                isOutlier = true;
            }
            return isOutlier;
        }

        public bool IsRegularFact()
        {
            bool isRegular = false;
            if (RegularFact != null)
            {
                isRegular = true;
            }
            return isRegular;
        }

    }
}
