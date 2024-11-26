using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Classes
{
    partial class LaunchPadCustomPropertyOrderComparer : IComparer<string>
    {
        private readonly List<string> _order;

        public LaunchPadCustomPropertyOrderComparer(List<string> order)
        {
            _order = order.Select(o => o.ToLower()).ToList();
        }

        public int Compare(string x, string y)
        {
            int indexX = _order.IndexOf((x ?? string.Empty).ToLower());
            int indexY = _order.IndexOf((y ?? string.Empty).ToLower());

            if (indexX == -1) indexX = int.MaxValue; // Keys not in the order list go last
            if (indexY == -1) indexY = int.MaxValue;

            // If both keys are "not in order," fall back to regular string comparison
            int result = indexX.CompareTo(indexY);
            return result != 0 ? result : string.Compare(x, y, StringComparison.Ordinal);
        }
    }
}
