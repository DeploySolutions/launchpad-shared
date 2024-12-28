using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.CodeRepresentation
{
    public partial class ClassRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual SortedDictionary<string, PropertyRepresentation> Properties { get; set; } = new SortedDictionary<string, PropertyRepresentation>();

        public virtual List<string> PropertySortOrder { get; set; } = new List<string>();

        public virtual SortedDictionary<string, MethodRepresentation> Methods { get; set; } = new SortedDictionary<string, MethodRepresentation>();

        public virtual List<string> MethodSortOrder { get; set; } = new List<string>();
        

        public ClassRepresentation(string name)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
        }


        /// <summary>
        /// Set the PropertySortOrder with the provided list and then 
        /// sort the properties according to the PropertySortOrder. 
        /// 
        /// </summary>
        /// <param name="propertySortOrder"></param>
        public virtual void SortMethods(List<string> methodSortOrder)
        {
            Guard.Against<ArgumentNullException>(methodSortOrder == null, "methodSortOrder cannot be null.");
            MethodSortOrder = methodSortOrder;
            SortMethods();
        }

        /// <summary>
        /// Sort the properties according to the PropertySortOrder (if any is set, if not then the existing order will not change). 
        /// 
        /// </summary>
        public virtual void SortMethods()
        {
            Guard.Against<ArgumentNullException>(MethodSortOrder == null, "MethodSortOrder cannot be null.");
            Methods = new SortedDictionary<string, MethodRepresentation>(
               Methods,
               new LaunchPadCustomPropertyOrderComparer(MethodSortOrder)
            );
        }
        /// <summary>
        /// Set the PropertySortOrder with the provided list and then 
        /// sort the properties according to the PropertySortOrder. 
        /// 
        /// </summary>
        /// <param name="propertySortOrder"></param>
        public virtual void SortProperties(List<string> propertySortOrder)
        {
            Guard.Against<ArgumentNullException>(propertySortOrder == null, "propertySortOrder cannot be null.");
            PropertySortOrder = propertySortOrder;
            SortProperties();
        }

        /// <summary>
        /// Sort the properties according to the PropertySortOrder (if any is set, if not then the existing order will not change). 
        /// 
        /// </summary>
        public virtual void SortProperties()
        {
            Guard.Against<ArgumentNullException>(PropertySortOrder == null, "PropertySortOrder cannot be null.");
            Properties = new SortedDictionary<string, PropertyRepresentation>(
               Properties,
               new LaunchPadCustomPropertyOrderComparer(PropertySortOrder)
            );
        }

    }
}
