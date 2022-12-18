using DeploySoftware.LaunchPad.Core.Specifications;
using System;
using System.Linq.Expressions;

namespace DeploySoftware.LaunchPad.Core.Abp.Specifications
{
    public partial class LaunchPadAbpSpecification<T> : LaunchPadSpecification<T>, ILaunchPadAbpSpecification<T>
    {
        public LaunchPadAbpSpecification(Expression<Func<T, bool>> predicate) : base(predicate)
        {
        }

        /// <summary>
        /// Abp method to help with repository filtering
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Expression<Func<T, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
