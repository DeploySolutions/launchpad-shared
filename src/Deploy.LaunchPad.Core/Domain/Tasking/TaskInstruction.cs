using Deploy.LaunchPad.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Tasking
{
    [Serializable]
    public partial class TaskInstruction : ITaskInstructions
    {
        protected string _short = string.Empty;
        /// <summary>
        /// The short name of this element (if different from the FullName field). If not set, it will default to the first 20 characters of the full name.
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(24, ErrorMessageResourceName = "Validation_ElementName_Short_24CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("short", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string Short
        {
            get
            {
                if (string.IsNullOrEmpty(_short))
                {
                    return Full;
                }
                else
                {
                    return _short;
                }
            }
            set
            {
                _short = value;
            }
        }

        protected string _full = string.Empty;
        /// <summary>
        /// The full name of this element
        /// </summary>
        /// <value>The full name.</value>
        [Required]
        [MaxLength(8096, ErrorMessageResourceName = "Validation_ElementDescription_Full_8096CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("full")]
        public virtual string Full
        {
            get
            {
                return _full;
            }
            set
            {
                _full = value;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri? MoreInformationUri { get; set; }

        public TaskInstruction()
        {
            Full = "Missing instructions.";
            Short = "Missing instructions.";
        }

        public TaskInstruction(string instructionsFull)
        {
            Full = instructionsFull;
            if (!string.IsNullOrEmpty(instructionsFull))
            {
                Short = instructionsFull.Length > 24 ? instructionsFull.Substring(0, 24) : instructionsFull;
            }
        }

        public TaskInstruction(string instructionsFull, Uri moreInformationUri)
        {
            Full = instructionsFull;
            if (!string.IsNullOrEmpty(instructionsFull))
            {
                Short = instructionsFull.Length > 24 ? instructionsFull.Substring(0, 24) : instructionsFull;
            }
            MoreInformationUri = moreInformationUri;
        }


        public TaskInstruction(string instructionsFull, string instructionsShort, Uri moreInformationUri)
        {
            Full = instructionsFull;
            if (!string.IsNullOrEmpty(instructionsShort))
            {
                Short = instructionsShort.Length > 24 ? instructionsShort.Substring(0, 24) : instructionsShort;
            }
            MoreInformationUri = moreInformationUri;
        }

    }
}
