using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Guids
{
    /// <summary>
    /// Static GUID special values to allow consistency for data handling purposes. Design rules:
    /// All values conform to version/variant rules
    /// They are easy to tell apart, with standard prefixes
    /// The tail of the GUID encodes meaning in hex such as "any", "none", "unk"
    /// They are compatible with C#, EF Core, Postgres, APIs
    /// </summary>
    public static partial class GuidConstants
    {
        public static readonly Guid Any = new Guid("aaaaaaaa-aaaa-4aaa-8aaa-616e79000000"); // "any"
        public static readonly Guid None = new Guid("bbbbbbbb-bbbb-4bbb-8bbb-6e6f6e650000"); // "none"
        public static readonly Guid Unknown = new Guid("ffffffff-ffff-4fff-8fff-756e6b000000"); // "unk"
        public static readonly Guid Default = new Guid("00000000-0000-0000-0000-000000000001"); // Default ID of 1
        public static readonly Guid Empty = Guid.Empty;
        public static bool IsAny(Guid guid) => guid == Any;
        public static bool IsNone(Guid guid) => guid == None;
        public static bool IsUnknown(Guid guid) => guid == Unknown;
        public static bool IsEmpty(Guid guid) => guid == Empty;

        public enum GuidConstantType
        {
            Other = 0,
            Any = 1,
            None = 2,
            Empty = 3,
            Unknown = 4
        }

        /// <summary>
        /// Determines which of the GUID Constant types the caller-provider GUID corresponds to 
        /// (or "Other" if it does not match any of the special values).
        /// </summary>
        /// <param name="guid">The guid to check</param>
        /// <returns>An enum indicating which of the Guid Constants the caller-provided Guid corresponds to</returns>
        public static GuidConstantType GetGuidConstantType(Guid guid)
        {
            if (guid == Any) return GuidConstantType.Any;
            if (guid == None) return GuidConstantType.None;
            if (guid == Unknown) return GuidConstantType.Unknown;
            if (guid == Empty) return GuidConstantType.Empty;
            return GuidConstantType.Other;
        }
    }
}
