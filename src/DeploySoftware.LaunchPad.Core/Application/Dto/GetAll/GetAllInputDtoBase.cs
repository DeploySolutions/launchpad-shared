using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using DeploySoftware.LaunchPad.Core.Domain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class GetAllInputDtoBase<TIdType> : 
        ICanBeAppServiceMethodInput, IPagedResultRequest
    {

        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public string Sort { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]

        public int SkipCount { get; set; }
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public int MaxResultCount { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual int TenantId { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String DescriptionFull { get; set; }

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The user name that created the entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String CreatorUserName { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The user name that last modified the entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String LastModifierUserName { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// The culture of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String Culture { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String Name { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllInputDtoBase() : base()
        {
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            Name = string.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;
            CreatorUserName = string.Empty;
            LastModifierUserName = string.Empty;
            CreationTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id"></param>
        public GetAllInputDtoBase(int tenantId) : base()
        {
            TenantId = tenantId;
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            Name = String.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;
            CreatorUserName = string.Empty;
            LastModifierUserName = string.Empty;
            CreationTime = DateTime.UtcNow;
        }

        public GetAllInputDtoBase(int tenantId, String culture) : base()
        {
            TenantId = tenantId;
            Culture = culture;
            Name = string.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;
            CreatorUserName = string.Empty;
            LastModifierUserName = string.Empty;
            CreationTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllInputDtoBase(SerializationInfo info, StreamingContext context)
        {
            Culture = info.GetString("Culture");
            TenantId = info.GetInt32("TenantId");
            Name = info.GetString("DisplayName");
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserName = info.GetString("CreatorUserName");
            LastModifierUserName = info.GetString("LastModifierUserName");
            LastModificationTime = info.GetDateTime("LastModificationTime");
            IsActive = info.GetBoolean("IsActive");
        }

        #endregion



        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Culture", Culture);
            info.AddValue("TenantId", TenantId);
            info.AddValue("Name", Name);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserName", CreatorUserName);
            info.AddValue("LastModifierUserName", LastModifierUserName);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("IsActive", IsActive);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[GetAllInputDtoBase : ");
            sb.Append(ToStringBaseProperties());
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected virtual String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            // LaunchPAD RAD properties
            //
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("CreatorUserName={0};", CreatorUserName);
            sb.AppendFormat("LastModifierUserName={0};", LastModifierUserName);
            
            // ABP properties
            //

            sb.AppendFormat("IsActive={0};", IsActive);
            sb.AppendFormat("CreationTime={0};", CreationTime);
            sb.AppendFormat("LastModificationTime={0};", LastModificationTime);
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new TEntity Clone<TEntity>() where TEntity : GetAllInputDtoBase<TIdType>, new()
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
        public virtual int CompareTo(GetAllInputDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by id and culture
            return Name.CompareTo(other.Name)
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
            if (obj != null && obj is GetAllInputDtoBase<TIdType>)
            {
                return Equals(obj as GetAllInputDtoBase<TIdType>);
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
        public virtual bool Equals(GetAllInputDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                return Name.Equals(obj.Name) && Culture.Equals(obj.Culture) && TenantId.Equals(obj.TenantId)
                    && DescriptionShort.Equals(obj.DescriptionShort)
                    && DescriptionFull.Equals(obj.DescriptionFull)
                    && IsActive.Equals(obj.IsActive)
                    && CreationTime.Equals(obj.CreationTime)
                    && CreatorUserName.Equals(obj.CreatorUserName)
                    && LastModifierUserName.Equals(obj.LastModifierUserName)
                    && LastModificationTime.Equals(obj.LastModificationTime)
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
        public static bool operator ==(GetAllInputDtoBase<TIdType> x, GetAllInputDtoBase<TIdType> y)
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
        public static bool operator !=(GetAllInputDtoBase<TIdType> x, GetAllInputDtoBase<TIdType> y)
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
            return Name.GetHashCode() + Culture.GetHashCode() + TenantId.GetHashCode() + CreatorUserName.GetHashCode() + LastModifierUserName.GetHashCode();
        }
    }
}
