// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.CDK
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="WafStackConstruct.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.CDK;
using Amazon.CDK.AWS.WAFv2;
using Amazon.JSII.Runtime.Deputy;
using System.Collections.Generic;

namespace Deploy.LaunchPad.AWS.CDK
{
    /// <summary>
    /// Class WafStackConstruct.
    /// Implements the <see cref="DeputyBase" />
    /// Implements the <see cref="ICfnWebACLProps" />
    /// Implements the <see cref="ICfnWebACLAssociationProps" />
    /// Implements the <see cref="ICfnResourceProps" />
    /// </summary>
    /// <seealso cref="DeputyBase" />
    /// <seealso cref="ICfnWebACLProps" />
    /// <seealso cref="ICfnWebACLAssociationProps" />
    /// <seealso cref="ICfnResourceProps" />
    public partial class WafStackConstruct : DeputyBase, ICfnWebACLProps, ICfnWebACLAssociationProps, ICfnResourceProps
    {
        /// <summary>
        /// The action to perform if none of the `Rules` contained in the `WebACL` match.
        /// </summary>
        /// <value>The default action.</value>
        /// <remarks><strong>Link</strong>: http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-wafv2-webacl.html#cfn-wafv2-webacl-defaultaction</remarks>
        public object DefaultAction { get; set; }

        /// <summary>
        /// Gets or sets the name of the metric.
        /// </summary>
        /// <value>The name of the metric.</value>
        public string MetricName { get; set; }

        /// <summary>
        /// The name of the web ACL.
        /// </summary>
        /// <value>The name.</value>
        /// <remarks>You cannot change the name of a web ACL after you create it.
        /// <strong>Link</strong>: http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-wafv2-webacl.html#cfn-wafv2-webacl-name</remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the geo match statement.
        /// </summary>
        /// <value>The geo match statement.</value>
        public CfnRuleGroup.GeoMatchStatementProperty geoMatchStatement { get; set; }
        /// <summary>
        /// Gets or sets the country codes for geo match statement.
        /// </summary>
        /// <value>The country codes for geo match statement.</value>
        public string[] CountryCodesForGeoMatchStatement { get; set; }
        /// <summary>
        /// Gets or sets the forwarded ip configuration fall back behavior.
        /// </summary>
        /// <value>The forwarded ip configuration fall back behavior.</value>
        public string ForwardedIpConfigFallBackBehavior { get; set; }
        /// <summary>
        /// Gets or sets the name of the forwarded ip configuration header.
        /// </summary>
        /// <value>The name of the forwarded ip configuration header.</value>
        public string ForwardedIpConfigHeaderName { get; set; }
        /// <summary>
        /// Gets or sets the forwarded ip configuration.
        /// </summary>
        /// <value>The forwarded ip configuration.</value>
        public CfnRuleGroup.ForwardedIPConfigurationProperty forwardedIPConfiguration { get; set; }
        /// <summary>
        /// Struct visibilityConfigProperty
        /// </summary>
        public struct visibilityConfigProperty
        {
            /// <summary>
            /// Gets or sets a value indicating whether [cloud watch metrics enabled].
            /// </summary>
            /// <value><c>true</c> if [cloud watch metrics enabled]; otherwise, <c>false</c>.</value>
            public bool CloudWatchMetricsEnabled { get; set; }
            /// <summary>
            /// Gets or sets the name of the metric.
            /// </summary>
            /// <value>The name of the metric.</value>
            public string MetricName { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [sampled requests enabled].
            /// </summary>
            /// <value><c>true</c> if [sampled requests enabled]; otherwise, <c>false</c>.</value>
            public bool SampledRequestsEnabled { get; set; }
        }
        /// <summary>
        /// The visibility configuration property
        /// </summary>
        public visibilityConfigProperty visibilityConfigProp;
        /// <summary>
        /// Gets or sets the geo rule property.
        /// </summary>
        /// <value>The geo rule property.</value>
        public CfnWebACL.RuleProperty GeoRuleProperty { get; set; }

        /// <summary>
        /// Gets or sets the API gateway web acl.
        /// </summary>
        /// <value>The API gateway web acl.</value>
        public Amazon.CDK.AWS.WAFv2.CfnWebACL apiGatewayWebACL { get; set; }
        /// <summary>
        /// Gets or sets the sentinel API gateway web acl.
        /// </summary>
        /// <value>The sentinel API gateway web acl.</value>
        public Amazon.CDK.AWS.WAFv2.CfnWebACL sentinelApiGatewayWebACL { get; set; }
        /// <summary>
        /// Gets or sets the web acl rules.
        /// </summary>
        /// <value>The web acl rules.</value>
        public List<Amazon.CDK.AWS.WAFv2.CfnWebACL.RuleProperty> webAclRules { get; set; }
        /// <summary>
        /// CloudFormation resource type (e.g. `AWS::S3::Bucket`).
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }
        /// <summary>
        /// Enum RuleDefaultActions
        /// </summary>
        public enum RuleDefaultActions
        {
            /// <summary>
            /// The allow
            /// </summary>
            ALLOW,
            /// <summary>
            /// The block
            /// </summary>
            BLOCK,
            /// <summary>
            /// The count
            /// </summary>
            COUNT
        }
        /// <summary>
        /// Gets or sets the rule actions.
        /// </summary>
        /// <value>The rule actions.</value>
        public RuleDefaultActions ruleActions { get; set; }
        /// <summary>
        /// Gets or sets the geo rule properties.
        /// </summary>
        /// <value>The geo rule properties.</value>
        public List<Amazon.CDK.AWS.WAFv2.CfnWebACL.RuleProperty> GeoRuleProperties { get; set; }
        //public CfnWebACL.DefaultActionProperty 
        /// <summary>
        /// Specifies whether this is for an Amazon CloudFront distribution or for a regional application.
        /// </summary>
        /// <value>The scope.</value>
        /// <remarks>A regional application can be an Application Load Balancer (ALB), an Amazon API Gateway REST API, an AWS AppSync GraphQL API, an Amazon Cognito user pool, or an AWS App Runner service. Valid Values are <c>CLOUDFRONT</c> and <c>REGIONAL</c> .
        /// For <c>CLOUDFRONT</c> , you must create your WAFv2 resources in the US East (N. Virginia) Region, <c>us-east-1</c> .
        /// For information about how to define the association of the web ACL with your resource, see <c>WebACLAssociation</c> .
        /// <strong>Link</strong>: http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-wafv2-webacl.html#cfn-wafv2-webacl-scope</remarks>
        public string Scope { get; set; }

        /// <summary>
        /// Defines and enables Amazon CloudWatch metrics and web request sample collection.
        /// </summary>
        /// <value>The visibility configuration.</value>
        /// <remarks><strong>Link</strong>: http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-wafv2-webacl.html#cfn-wafv2-webacl-visibilityconfig</remarks>
        public object VisibilityConfig { get; set; }

        /// <summary>
        /// The Amazon Resource Name (ARN) of the resource to associate with the web ACL.
        /// </summary>
        /// <value>The resource arn.</value>
        /// <remarks>The ARN must be in one of the following formats:
        /// <list type="bullet"><description>For an Application Load Balancer: <c>arn:aws:elasticloadbalancing: *region* : *account-id* :loadbalancer/app/ *load-balancer-name* / *load-balancer-id*</c></description><description>For an Amazon API Gateway REST API: <c>arn:aws:apigateway: *region* ::/restapis/ *api-id* /stages/ *stage-name*</c></description><description>For an AWS AppSync GraphQL API: <c>arn:aws:appsync: *region* : *account-id* :apis/ *GraphQLApiId*</c></description><description>For an Amazon Cognito user pool: <c>arn:aws:cognito-idp: *region* : *account-id* :userpool/ *user-pool-id*</c></description><description>For an AWS App Runner service: <c>arn:aws:apprunner: *region* : *account-id* :service/ *apprunner-service-name* / *apprunner-service-id*</c></description></list><strong>Link</strong>: http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-wafv2-webaclassociation.html#cfn-wafv2-webaclassociation-resourcearn</remarks>
        public string ResourceArn { get; set; }

        /// <summary>
        /// The Amazon Resource Name (ARN) of the web ACL that you want to associate with the resource.
        /// </summary>
        /// <value>The web acl arn.</value>
        /// <remarks><strong>Link</strong>: http://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-wafv2-webaclassociation.html#cfn-wafv2-webaclassociation-webaclarn</remarks>
        public string WebAclArn { get; set; }
    }
    /// <summary>
    /// Class WafStackProps.
    /// Implements the <see cref="StackProps" />
    /// </summary>
    /// <seealso cref="StackProps" />
    public partial class WafStackProps : StackProps
    {
        /// <summary>
        /// The props
        /// </summary>
        internal WafStackConstruct Props;
        /// <summary>
        /// Initializes a new instance of the <see cref="WafStackProps"/> class.
        /// </summary>
        internal WafStackProps()
        {
            this.Props = new WafStackConstruct();
            this.Props.geoMatchStatement = new CfnRuleGroup.GeoMatchStatementProperty();
            this.Props.forwardedIPConfiguration = new CfnRuleGroup.ForwardedIPConfigurationProperty();
            this.Props.GeoRuleProperties = new List<Amazon.CDK.AWS.WAFv2.CfnWebACL.RuleProperty>();
            this.Props.webAclRules = new List<Amazon.CDK.AWS.WAFv2.CfnWebACL.RuleProperty>();
            this.Props.visibilityConfigProp = new WafStackConstruct.visibilityConfigProperty();
            this.Props.GeoRuleProperty = new CfnWebACL.RuleProperty();
        }
    }
}
