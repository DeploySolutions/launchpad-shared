// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.CDK
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="StackBase.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Constructs;

namespace Deploy.LaunchPad.AWS.CDK
{
    /// <summary>
    /// Class StackBase.
    /// Implements the <see cref="Stack" />
    /// </summary>
    /// <typeparam name="TStackHelper">The type of the t stack helper.</typeparam>
    /// <seealso cref="Stack" />
    public partial class StackBase<TStackHelper> : Stack
        where TStackHelper : AwsCdkHelper, new()
    {
        /// <summary>
        /// The scope
        /// </summary>
        protected Construct _scope;
        /// <summary>
        /// The stack props
        /// </summary>
        protected IStackProps _stackProps;

        /// <summary>
        /// The VPC
        /// </summary>
        protected IVpc _vpc;

        /// <summary>
        /// The stack helper
        /// </summary>
        TStackHelper _stackHelper;

        /// <summary>
        /// Gets the stack helper.
        /// </summary>
        /// <value>The stack helper.</value>
        public TStackHelper StackHelper { get { return _stackHelper; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackBase{TStackHelper}"/> class.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="stackProps">The stack props.</param>
        protected StackBase(Construct scope, string id, IStackProps stackProps) : base(scope, id, stackProps)
        {
            _scope = scope;
            _stackProps = stackProps;
            _stackHelper = new TStackHelper();
            _stackHelper.Initialize(this, stackProps);
        }

        /// <summary>
        /// Creates the helper.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="stackProps">The stack props.</param>
        /// <returns>TStackHelper.</returns>
        public virtual TStackHelper CreateHelper(Stack s, IStackProps stackProps)
        {
            _stackHelper = new TStackHelper();
            _stackHelper.Initialize(s, stackProps);
            _vpc = _stackHelper.GetVpc();
            return _stackHelper;
        }


    }
}
