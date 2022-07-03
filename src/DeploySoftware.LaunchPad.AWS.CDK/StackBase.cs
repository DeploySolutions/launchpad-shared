using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.CDK
{
    public partial class StackBase<TStackHelper> : Stack
        where TStackHelper: AwsCdkHelper, new()
    {
        protected Construct _scope;
        protected IStackProps _stackProps;

        protected IVpc _vpc;

        TStackHelper _stackHelper;

        public TStackHelper StackHelper { get { return _stackHelper; } }

        protected StackBase(Construct scope, string id, IStackProps stackProps) : base(scope, id, stackProps)
        {
            _scope = scope;
            _stackProps = stackProps;
            _stackHelper = new TStackHelper();
            _stackHelper.Initialize(this, stackProps);
        }

        public virtual TStackHelper CreateHelper(Stack s, IStackProps stackProps)
        {
            _stackHelper = new TStackHelper();
            _stackHelper.Initialize(s, stackProps);
            _vpc = _stackHelper.GetVpc();
            return _stackHelper;
        }

        
    }
}
