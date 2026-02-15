// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="UpdateInputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.SoftwareApplications;
using Deploy.LaunchPad.Code.Services;
using Deploy.LaunchPad.Domain.Metadata;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Deploy.LaunchPad.Domain;
using Deploy.LaunchPad.Core;

namespace Deploy.LaunchPad.Code.Services.Dto
{
    /// <summary>
    /// Class UpdateInputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Services.Dto.EntityDtoBase{TIdType}" />
    /// Implements the <see cref="ICanBeAppServiceMethodInput" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Code.Services.Dto.EntityDtoBase{TIdType}" />
    /// <seealso cref="ICanBeAppServiceMethodInput" />
    public abstract partial class UpdateInputDtoBase<TIdType> : EntityDtoBase<TIdType>, ICanBeAppServiceMethodInput
    {
        /// <summary>
        /// The id of this object
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        [Required]
        public override TIdType Id { get; set; }

        protected string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected string _description;
        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// The culture of this object
        /// </summary>
        /// <value>The culture.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        public virtual String Culture { get; set; }

        /// <summary>
        /// If this object is not a translation this field will be null.
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        /// <value>The translated from identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual Guid? TranslatedFromId { get; set; }


        /// <summary>
        /// The sequence number for this entity, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        /// <value>The seq number.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Int32 SeqNum { get; set; } = 0;

        /// <summary>
        /// The external ID stored in a client system (if any). Can be any type on client system, but retained here as text.
        /// </summary>
        /// <value>The external identifier.</value>
        [MaxLength(36, ErrorMessageResourceName = "Validation_ExternalId_36CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String ExternalId { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected UpdateInputDtoBase() : base()
        {

            ExternalId = string.Empty; Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            Name = string.Empty;
            Description = string.Empty;            
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected UpdateInputDtoBase(TIdType id) : base()
        {
            Id = id;
            ExternalId = string.Empty;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            Name = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInputDtoBase{TIdType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        protected UpdateInputDtoBase(TIdType id, String culture) : base()
        {
            Id = id;
            ExternalId = string.Empty;
            Culture = culture;
            Name = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected UpdateInputDtoBase(SerializationInfo info, StreamingContext context)
        {
            Id = (TIdType)info.GetValue("Id", typeof(TIdType));
            ExternalId = info.GetString("ExternalId");
            Culture = info.GetString("Culture");
            Name = info.GetString("Name");
            Description = info.GetString("Description");
            SeqNum = info.GetInt32("SeqNum");
        }

        #endregion



        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("ExternalId", ExternalId);
            info.AddValue("Culture", Culture);
            info.AddValue("TranslatedFromId", TranslatedFromId);
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
            info.AddValue("SeqNum", SeqNum);
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
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            sb.AppendFormat("ExternalId={0};", ExternalId);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("Description={0};", Description);
            sb.AppendFormat("SeqNum={0};", SeqNum);

            // ABP properties
            //
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new TEntity Clone<TEntity>() where TEntity : UpdateInputDtoBase<TIdType>, new()
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
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(UpdateInputDtoBase<TIdType> other)
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
            if (obj != null && obj is UpdateInputDtoBase<TIdType>)
            {
                return Equals(obj as UpdateInputDtoBase<TIdType>);
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
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Equals(UpdateInputDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) && ExternalId.Equals(obj.ExternalId) && SeqNum == obj.SeqNum
                    && Description.Equals(obj.Description) && Name.Equals(obj.Name) && TranslatedFromId.Equals(obj.TranslatedFromId)
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
        public static bool operator ==(UpdateInputDtoBase<TIdType> x, UpdateInputDtoBase<TIdType> y)
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
        public static bool operator !=(UpdateInputDtoBase<TIdType> x, UpdateInputDtoBase<TIdType> y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="Object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + Culture.GetHashCode()
                + ExternalId.GetHashCode()
                + Name.GetHashCode()
                + Description.GetHashCode()
                + TranslatedFromId.GetHashCode()
           ;
        }
    }
}
