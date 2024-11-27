using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Classes
{
    public partial class ClassRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual Dictionary<string, PropertyRepresentation> Properties { get; set; }
        public virtual LinkedList<string> PropertySortOrder { get; set; } 
        public virtual Dictionary<string, MethodRepresentation> Methods { get; set; }

        public ClassRepresentation(string name)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            Properties = new Dictionary<string, PropertyRepresentation>();
            PropertySortOrder = new LinkedList<string>();
            Methods = new Dictionary<string, MethodRepresentation>();
        }


        public virtual void ReorderProperties()
        {
            var tempProperties = new Dictionary<string, PropertyRepresentation>(Properties);

            // Clear the existing Properties dictionary
            Properties.Clear();

            // Add properties based on the provided sort order
            foreach (var item in PropertySortOrder)
            {
                if (tempProperties.TryGetValue(item, out var property))
                {
                    Properties.Add(item, property);
                    tempProperties.Remove(item); // Remove the property from the temporary dictionary once added
                }
            }

            // Add any remaining properties that were not in the sort order
            foreach (var kvp in tempProperties)
            {
                Properties.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
