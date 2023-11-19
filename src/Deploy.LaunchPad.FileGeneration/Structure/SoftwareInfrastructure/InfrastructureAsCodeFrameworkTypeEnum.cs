// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="InfrastructureAsCodeFrameworkTypeEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace Deploy.LaunchPad.FileGeneration.Structure
{

    /// <summary>
    /// Enum InfrastructureAsCodeFrameworkTypeEnum
    /// </summary>
    public enum InfrastructureAsCodeFrameworkTypeEnum
    {
        /// <summary>
        /// The terraform
        /// </summary>
        [Description("Terraform")]
        Terraform = 0,
        /// <summary>
        /// The aws CDK
        /// </summary>
        [Description("AWS Cloud Development Kit")]
        AWS_CDK = 1,
        /// <summary>
        /// The aws cloud formation
        /// </summary>
        [Description("AWS Cloud Formation")]
        AWS_CloudFormation = 2,
        /// <summary>
        /// The azure arm
        /// </summary>
        [Description("Azure Resource Manager (ARM) templates")]
        Azure_ARM = 2
    }
}
