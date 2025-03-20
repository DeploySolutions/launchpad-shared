// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="CreateUpdateOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Application.Dto.NameDescription;
using Deploy.LaunchPad.Core.Abp.Domain.SoftwareApplications;
using Deploy.LaunchPad.Util;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    /// <summary>
    /// Class CreateUpdateOutputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetOutputDtoBase{TIdType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetOutputDtoBase{TIdType}" />
    public abstract partial class CreateUpdateOutputDtoBase<TIdType> : GetOutputDtoBase<TIdType>
    {

        protected ElementNameDto _name;
        /// <summary>
        /// The name of this item.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementNameDto Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        protected ElementDescriptionDto _description;
        /// <summary>
        /// A description of this item.
        /// </summary>
        /// <value>The description.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementDescriptionDto Description
        {
            get { return _description; }
            protected set { _description = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// If this object is not a translation this field will be null.
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        /// <value>The translated from identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual TIdType TranslatedFromId { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected CreateUpdateOutputDtoBase() : base()
        {
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            ExternalId = string.Empty;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected CreateUpdateOutputDtoBase(TIdType id) : base(id)
        {
            Id = id;
            ExternalId = string.Empty;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUpdateOutputDtoBase{TIdType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        protected CreateUpdateOutputDtoBase(TIdType id, String culture) : base(id, culture)
        {
            Id = id;
            ExternalId = string.Empty;
            Culture = culture;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected CreateUpdateOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Id = (TIdType)info.GetValue("Id", typeof(TIdType));
            SeqNum = info.GetInt32("SeqNum");
            ExternalId = info.GetString("ExternalId");
            TranslatedFromId = (TIdType)info.GetValue("TranslatedFromId", typeof(TIdType));
            Culture = info.GetString("Culture");
            Name = (ElementNameDto)info.GetValue("Name", typeof(ElementNameDto)); // DisplayName?
            Description = (ElementDescriptionDto)info.GetValue("Description", typeof(ElementDescriptionDto));
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
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
            info.AddValue("TranslatedFromId", TranslatedFromId);
            info.AddValue("Culture", Culture);
            info.AddValue("ExternalId", ExternalId);
            info.AddValue("SeqNum", SeqNum);

        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[CreateUpdateOutputDtoBase : ");
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
            sb.AppendFormat("Description={0};", Description);
            sb.AppendFormat("SeqNum={0};", SeqNum);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            sb.AppendFormat("ExternalId={0};", ExternalId);
            // ABP properties
            //
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new TEntity Clone<TEntity>() where TEntity : CreateUpdateOutputDtoBase<TIdType>, new()
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
        public virtual int CompareTo(CreateUpdateOutputDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by name and description short
            return Name.Full.CompareTo(other.Name.Full);
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is CreateUpdateOutputDtoBase<TIdType>)
            {
                return Equals(obj as CreateUpdateOutputDtoBase<TIdType>);
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
        public virtual bool Equals(CreateUpdateOutputDtoBase<TIdType> obj)
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
        public static bool operator ==(CreateUpdateOutputDtoBase<TIdType> x, CreateUpdateOutputDtoBase<TIdType> y)
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
        public static bool operator !=(CreateUpdateOutputDtoBase<TIdType> x, CreateUpdateOutputDtoBase<TIdType> y)
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
            return Id.GetHashCode() + Culture.GetHashCode() + ExternalId.GetHashCode() + SeqNum.GetHashCode()
                + Name.GetHashCode()
                + Description.GetHashCode()
                + TranslatedFromId.GetHashCode();
        }

    }
}
