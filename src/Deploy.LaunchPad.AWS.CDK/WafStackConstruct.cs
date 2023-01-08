using Amazon.CDK;
using Amazon.CDK.AWS.WAFv2;
using Amazon.JSII.Runtime.Deputy;
using System.Collections.Generic;

namespace Deploy.LaunchPad.AWS.CDK
{
    public class WafStackConstruct : DeputyBase, ICfnWebACLProps, ICfnWebACLAssociationProps, ICfnResourceProps
    {
        public object DefaultAction { get; set; }

        public string MetricName { get; set; }

        public string Name { get; set; }

        public CfnRuleGroup.GeoMatchStatementProperty geoMatchStatement { get; set; }
        public string[] CountryCodesForGeoMatchStatement { get; set; }
        public string ForwardedIpConfigFallBackBehavior { get; set; }
        public string ForwardedIpConfigHeaderName { get; set; }
        public CfnRuleGroup.ForwardedIPConfigurationProperty forwardedIPConfiguration { get; set; }
        public struct visibilityConfigProperty
        {
            public bool CloudWatchMetricsEnabled { get; set; }
            public string MetricName { get; set; }
            public bool SampledRequestsEnabled { get; set; }
        }
        public visibilityConfigProperty visibilityConfigProp;
        public CfnWebACL.RuleProperty GeoRuleProperty { get; set; }

        public Amazon.CDK.AWS.WAFv2.CfnWebACL apiGatewayWebACL { get; set; }
        public Amazon.CDK.AWS.WAFv2.CfnWebACL sentinelApiGatewayWebACL { get; set; }
        public List<Amazon.CDK.AWS.WAFv2.CfnWebACL.RuleProperty> webAclRules { get; set; }
        public string Type { get; set; }
        public enum RuleDefaultActions
        {
            ALLOW,
            BLOCK,
            COUNT
        }
        public RuleDefaultActions ruleActions { get; set; }
        public List<Amazon.CDK.AWS.WAFv2.CfnWebACL.RuleProperty> GeoRuleProperties { get; set; }
        //public CfnWebACL.DefaultActionProperty 
        public string Scope { get; set; }

        public object VisibilityConfig { get; set; }

        public string ResourceArn { get; set; }

        public string WebAclArn { get; set; }
    }
    public class WafStackProps : StackProps
    {
        internal WafStackConstruct Props;
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
