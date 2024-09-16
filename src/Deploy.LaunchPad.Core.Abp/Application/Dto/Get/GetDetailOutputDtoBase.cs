﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetDetailOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Abp.Domain.SoftwareApplications;
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
    /// Class GetDetailOutputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetOutputDtoBase{TIdType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetOutputDtoBase{TIdType}" />
    public abstract partial class GetDetailOutputDtoBase<TIdType> : GetOutputDtoBase<TIdType>
    {

        protected ElementDescription _description;
        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementDescription Description
        {
            get { return _description; }
            protected set { _description = value; }
        }

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The user name that created the entity
        /// </summary>
        /// <value>The name of the creator user.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String CreatorUserName { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The user name that last modified the entity
        /// </summary>
        /// <value>The last name of the modifier user.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String LastModifierUserName { get; set; }

        /// <summary>
        /// The sequence number for this entity, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        /// <value>The seq number.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Int32 SeqNum { get; set; } = 0;

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetDetailOutputDtoBase() : base()
        {
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            Description = new ElementDescription(string.Empty);
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected GetDetailOutputDtoBase(TIdType id) : base(id)
        {
            Id = id;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            Description = new ElementDescription(string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDetailOutputDtoBase{TIdType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="culture">The culture.</param>
        protected GetDetailOutputDtoBase(TIdType id, String culture) : base(id, culture)
        {
            Id = id;
            Culture = culture;
            Description = new ElementDescription(string.Empty);
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetDetailOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Id = (TIdType)info.GetValue("Id", typeof(TIdType));
            Culture = info.GetString("Culture");
            Name = (ElementName)info.GetValue("Name", typeof(ElementName)); 
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserName = info.GetString("CreatorUserName");
            LastModifierUserName = info.GetString("LastModifierUserName");
            LastModificationTime = info.GetDateTime("LastModificationTime");
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
            info.AddValue("Culture", Culture);
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserName", CreatorUserName);
            info.AddValue("LastModifierUserName", LastModifierUserName);
            info.AddValue("LastModificationTime", LastModificationTime);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[GetDetailOutputDtoBase : ");
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
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Culture={0};", Culture);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("Description={0};", Description);
            sb.AppendFormat("CreatorUserName={0};", CreatorUserName);
            sb.AppendFormat("LastModifierUserName={0};", LastModifierUserName);

            // ABP properties
            //
            sb.AppendFormat("CreationTime={0};", CreationTime);
            sb.AppendFormat("LastModificationTime={0};", LastModificationTime);

            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new TEntity Clone<TEntity>() where TEntity : GetDetailOutputDtoBase<TIdType>, new()
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
        public virtual int CompareTo(GetDetailOutputDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by name and description short
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is GetDetailOutputDtoBase<TIdType>)
            {
                return Equals(obj as GetDetailOutputDtoBase<TIdType>);
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
        public virtual bool Equals(GetDetailOutputDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture)
                    && Description.Equals(obj.Description)
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
        public static bool operator ==(GetDetailOutputDtoBase<TIdType> x, GetDetailOutputDtoBase<TIdType> y)
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
        public static bool operator !=(GetDetailOutputDtoBase<TIdType> x, GetDetailOutputDtoBase<TIdType> y)
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
            return Id.GetHashCode() + Culture.GetHashCode() + Name.GetHashCode() + CreatorUserName.GetHashCode() + LastModifierUserName.GetHashCode();
        }

    }
}
