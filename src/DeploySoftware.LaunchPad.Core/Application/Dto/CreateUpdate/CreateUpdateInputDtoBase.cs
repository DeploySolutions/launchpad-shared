using Abp.Domain.Entities;
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    public abstract partial class CreateUpdateInputDtoBase<TIdType> : EntityDtoBase<TIdType>
    {
        /// <summary>
        /// The culture of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String Culture { get; set; }

        /// <summary>
        /// If this object is not a translation this field will be null. 
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual TIdType TranslatedFromId { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String Name { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// A full description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual string? DescriptionFull { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public CreateUpdateInputDtoBase() : base()
        {
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            Name = string.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id"></param>
        public CreateUpdateInputDtoBase(TIdType id) : base()
        {
            Id = id;
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            Name = string.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;
        }

        public CreateUpdateInputDtoBase(TIdType id, String culture) : base()
        {
            Id = id;
            Culture = culture;
            Name = string.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected CreateUpdateInputDtoBase(SerializationInfo info, StreamingContext context)
        {
            Id = (TIdType)info.GetValue("Id", typeof(TIdType));
            TranslatedFromId = (TIdType)info.GetValue("TranslatedFromId", typeof(TIdType));
            Culture = info.GetString("Culture");
            Name = info.GetString("DisplayName");
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
        }

        #endregion



        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Culture", Culture);
            info.AddValue("TranslatedFromId", TranslatedFromId); 
            info.AddValue("Name", Name);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("DescriptionFull", DescriptionFull);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[CreateUpdateInputDtoBase : ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected override String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToStringBaseProperties());
            // LaunchPAD RAD properties
            //
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            // ABP properties
            //
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new TEntity Clone<TEntity>() where TEntity : CreateUpdateInputDtoBase<TIdType>, new()
        {
            TEntity clone = new TEntity();
            foreach (PropertyInfo info in GetType().GetProperties())
            {
                // ensure the property type is serializable
                if (info.GetType().IsSerializable)
                {
                    PropertyInfo cloneInfo = GetType().GetProperty(info.Name);
                    cloneInfo.SetValue(clone, info.GetValue(this, null), null);
                }
            }
            return clone;
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(CreateUpdateInputDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by id and culture
            return Id.ToString().CompareTo(other.Id.ToString())
                + Culture.CompareTo(other.Culture)
                ;
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is CreateUpdateInputDtoBase<TIdType>)
            {
                return Equals(obj as CreateUpdateInputDtoBase<TIdType>);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.        
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns></returns>
        public virtual bool Equals(CreateUpdateInputDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture)
                    && DescriptionShort.Equals(obj.DescriptionShort) && Name.Equals(obj.Name) && TranslatedFromId.Equals(obj.TranslatedFromId)
                ;
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(CreateUpdateInputDtoBase<TIdType> x, CreateUpdateInputDtoBase<TIdType> y)
        {
            if (x is null)
            {
                if (y is null)
                {
                    return true;
                }
                return false;
            }
            return x.Equals(y);
        }

        /// <summary>
        /// Override the != operator to test for inequality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are not equal based on the Equals logic</returns>
        public static bool operator !=(CreateUpdateInputDtoBase<TIdType> x, CreateUpdateInputDtoBase<TIdType> y)
        {
            return !(x == y);
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + Culture.GetHashCode()
                + Name.GetHashCode()
                + DescriptionShort.GetHashCode()
                + TranslatedFromId.GetHashCode()
           ;
        }
    }
}
