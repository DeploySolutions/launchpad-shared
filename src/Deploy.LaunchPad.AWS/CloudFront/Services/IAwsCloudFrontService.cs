// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-23-2023
// ***********************************************************************
// <copyright file="IAwsS3Service.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.S3;
using Deploy.LaunchPad.Code.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.CloudFront.Services
{
    /// <summary>
    /// Interface IAwsS3Service
    /// Extends the <see cref="ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    public partial interface IAwsCloudFrontService : ILaunchPadSystemIntegrationService
    {
        
      
        /// <summary>
        /// Checks if file exists.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> CheckIfFileExists(string bucketName, string s3KeyName);

       
    }
}
