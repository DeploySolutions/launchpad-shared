// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.CDK
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsCdkHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace Deploy.LaunchPad.AWS.CDK
{
    /// <summary>
    /// Interface IAwsCdkHelper
    /// </summary>
    public partial interface IAwsCdkHelper
    {
        /// <summary>
        /// Initializes the specified stack.
        /// </summary>
        /// <param name="stack">The stack.</param>
        /// <param name="stackProps">The stack props.</param>
        public void Initialize(Stack stack, IStackProps stackProps);

        /// <summary>
        /// Gets the VPC.
        /// </summary>
        /// <param name="vpcId">The VPC identifier.</param>
        /// <returns>IVpc.</returns>
        public IVpc GetVpc(string vpcId = "");

        /// <summary>
        /// Gets the user data commands from file.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String[].</returns>
        public string[] GetUserDataCommandsFromFile(string key);

        /// <summary>
        /// Gets the ami image identifier.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public string GetAmiImageId(string key);

        /// <summary>
        /// Gets the deployment username.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetDeploymentUsername();

        /// <summary>
        /// Gets the deployment principal arn.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetDeploymentPrincipalArn();

        /// <summary>
        /// Gets the aws account root principal arn.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetAwsAccountRootPrincipalArn();

        /// <summary>
        /// Gets the deployment account.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetDeploymentAccount();

        /// <summary>
        /// CDKs the output.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="description">The description.</param>
        /// <returns>System.String.</returns>
        public string CdkOutput(string id, string name, string value, string description);

        /// <summary>
        /// Gets the name of the security group by CDK output.
        /// </summary>
        /// <param name="cdkOutputName">Name of the CDK output.</param>
        /// <returns>ISecurityGroup.</returns>
        public ISecurityGroup GetSecurityGroupByCdkOutputName(string cdkOutputName);

    }
}
