using Deploy.LaunchPad.Core.BackgroundProcess;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.BackgroundProcess
{
    [Serializable]
    public abstract partial class LaunchPadBackgroundJobArgsBase : ICanBeLaunchPadBackgroundJobArgs
    {

        [Required]
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Id { get; set; }

        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Name { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionShort { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionFull { get; set; } = string.Empty;


        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Chron { get; set; }

        protected LaunchPadBackgroundJobArgsBase()
        {
            Id = Guid.NewGuid().ToString();
            Name = Id;
        }

        protected LaunchPadBackgroundJobArgsBase(string id)
        {
            Id = id;
            Name = Id;
        }


        protected LaunchPadBackgroundJobArgsBase(string id, string name, string chron)
        {
            Id = id;
            Name = name;
            Chron = chron;
        }

    }
}
