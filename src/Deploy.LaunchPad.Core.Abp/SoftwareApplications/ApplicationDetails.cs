// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ApplicationDetails.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util.Guids;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using IMayHaveTenant = Deploy.LaunchPad.Core.Metadata.IMayHaveTenant;


namespace Deploy.LaunchPad.Core.Abp.SoftwareApplications
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="Guid">The type of the t identifier type.</typeparam>
    [Serializable()]
    public partial class ApplicationDetails : DomainEntityBase<System.Guid>, 
        IApplicationDetails
    {
        /// <summary>
        /// The default culture
        /// </summary>
        public const string DEFAULT_CULTURE = "en";
        /// <summary>
        /// The default hexadecimal c ol our
        /// </summary>
        public const string DEFAULT_HEX_COlOUR = "1dbff0";

        /// <summary>
        /// Gets or sets the launch pad application identifier.
        /// </summary>
        /// <value>The launch pad application identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LaunchPadApplicationId))]
        public virtual Guid LaunchPadApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the default id of the Creator user, when not otherwise specified
        /// </summary>
        /// <value>A valid user id that corresponds to the creator of an object.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(CreatorUserId))]
        public virtual Guid DefaultCreatorUserId { get; set; }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        /// <value>The culture default.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IHaveCultureDetails CultureDetails
        {
            get; set;
        }

        /// <summary>
        /// The main theme
        /// </summary>
        /// <value>The theme.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Theme { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this application
        /// </summary>
        /// <value>The logo URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri LogoUri
        {
            get; set;
        }

        /// <summary>
        /// The primary colour (in HEX) for displays in this application
        /// </summary>
        /// <value>The primary colour hexadecimal.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String PrimaryColourHex
        {
            get; set;
        }

        /// <summary>
        /// The default display time zone of the application
        /// </summary>
        /// <value>The default time zone.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DefaultTimeZone
        {
            get; set;
        }


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDetails"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        [SetsRequiredMembers]
        public ApplicationDetails() : base()
        {
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDetails = new CultureDetails();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDetails"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        [SetsRequiredMembers]
        public ApplicationDetails(Guid id) : base(id)
        {
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDetails = new CultureDetails();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDetails"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        [SetsRequiredMembers]
        public ApplicationDetails(Guid id, string cultureName) : base(id, cultureName)
        {
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureInfo culture = new CultureInfo(cultureName);
            CultureDetails = new CultureDetails(culture);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDetails"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="cultureDefault">The culture default.</param>
        [SetsRequiredMembers]
        public ApplicationDetails(Guid id, string cultureName, Guid defaultCreatorUserId) : base(id, cultureName)
        {
            Id = id;
            PrimaryColourHex = ApplicationDetails.DEFAULT_HEX_COlOUR;
            CultureInfo culture = new CultureInfo(cultureName);
            CultureDetails = new CultureDetails(culture);
            DefaultCreatorUserId = defaultCreatorUserId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDetails"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">Name of the culture.</param>
        /// <param name="cultureDefault">The culture default.</param>
        /// <param name="cultureSupported">The culture supported.</param>
        [SetsRequiredMembers]
        public ApplicationDetails(Guid id, IHaveCultureDetails cultureDetails) : base(id)
        {
            Id = id;
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDetails = cultureDetails;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ApplicationDetails(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DefaultCreatorUserId = (Guid)info.GetValue("DefaultCreatorUserId", typeof(Uri));
            PrimaryColourHex = info.GetString("DisplayPrimaryColourHex");
            CultureDetails = (IHaveCultureDetails)info.GetValue("CultureDetails", typeof(IHaveCultureDetails));
            LogoUri = (Uri)info.GetValue("DisplayLogoUri", typeof(Uri));
            Theme = info.GetString("DisplayThemeName");
            DefaultTimeZone = info.GetString("DisplayDefaultTimeZone");
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CultureDetails", CultureDetails);
            info.AddValue("DefaultCreatorUserId", DefaultCreatorUserId);
            info.AddValue("PrimaryColourHex", PrimaryColourHex);
            info.AddValue("LogoUri", LogoUri);
            info.AddValue("Theme", Theme);
            info.AddValue("DefaultTimeZone", DefaultTimeZone);
        }

        /// <summary>
        /// Displays information about the class in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ApplicationInformation : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" CultureDetails={0};", CultureDetails);
            sb.AppendFormat(" DefaultCreatorUserId={0};", DefaultCreatorUserId);
            sb.AppendFormat(" PrimaryColourHex={0};", PrimaryColourHex);
            sb.AppendFormat(" LogoUri={0};", LogoUri);
            sb.AppendFormat(" Theme={0};", Theme);
            sb.AppendFormat(" DefaultTimeZone={0};", DefaultTimeZone);
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new virtual TEntity Clone<TEntity>() where TEntity : IApplicationDetails, new()
        {
            TEntity clone = new TEntity();
            foreach (PropertyInfo info in GetType().GetProperties())
            {
                // ensure the property type is serializable
                if (info.GetType().IsSerializable)
                {
                    PropertyInfo cloneInfo = GetType().GetProperty(info.Name);
                    if (cloneInfo != null) cloneInfo.SetValue(clone, info.GetValue(this, null), null);
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
        public virtual int CompareTo(ApplicationDetails other)
        {
            return other == null ? 1 : String.Compare(Name, other.Name, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ApplicationDetails information)
            {
                return Equals(information);
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
        public virtual bool Equals(ApplicationDetails obj)
        {
            if (obj != null)
            {

                // Transient objects are not considered as equal
                if (IsTransient() && obj.IsTransient())
                {
                    return false;
                }
                else
                {
                    return Id.Equals(obj.Id)
                    && DefaultCreatorUserId.Equals(obj.DefaultCreatorUserId)
                    && Name.Equals(obj.Name);

                }

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(ApplicationDetails x, ApplicationDetails y)
        {
            if (System.Object.ReferenceEquals(x, null))
            {
                if (System.Object.ReferenceEquals(y, null))
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
        public static bool operator !=(ApplicationDetails x, ApplicationDetails y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + DefaultCreatorUserId.GetHashCode();
        }

        int IComparable<Domain.Entities.FrameworkEntityBase<Guid>>.CompareTo(Domain.Entities.FrameworkEntityBase<Guid> other)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<Domain.Entities.FrameworkEntityBase<Guid>>.Equals(Domain.Entities.FrameworkEntityBase<Guid> other)
        {
            throw new NotImplementedException();
        }

        Domain.Entities.FrameworkEntityBase<Guid> IAmCloneable<Domain.Entities.FrameworkEntityBase<Guid>>.CloneGeneric()
        {
            throw new NotImplementedException();
        }

        int IComparable<DomainEntityBase<Guid>>.CompareTo(DomainEntityBase<Guid> other)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<DomainEntityBase<Guid>>.Equals(DomainEntityBase<Guid> other)
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        DomainEntityBase<Guid> IAmCloneable<DomainEntityBase<Guid>>.CloneGeneric()
        {
            throw new NotImplementedException();
        }
    }
}
