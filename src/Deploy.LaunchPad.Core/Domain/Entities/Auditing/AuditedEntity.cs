#region "Licensing"
/*
 * SPDX-FileCopyrightText: Copyright (c) Volosoft (https://volosoft.com) and contributors
 * SPDX-License-Identifier: MIT
 *
 * This file contains code originally from the ASP.NET Boilerplate - Web Application Framework:
 *   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
 *
 * The original portions of this file remain licensed under the MIT License.
 * You may obtain a copy of the MIT License at:
 *
 *   https://opensource.org/license/mit
 *
 *
 * SPDX-FileCopyrightText: Copyright (c) 2026 Deploy Software Solutions (https://www.deploy.solutions)
 * SPDX-License-Identifier: Apache-2.0
 *
 * Modifications and additional code in this file are licensed under
 * the Apache License, Version 2.0.
 *
 * You may obtain a copy of the Apache License at:
 *
 *   https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the Apache License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the applicable license for governing permissions and limitations.
 */
#endregion

using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util.Timing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="System.Guid"/>).
    /// </summary>
    [Serializable]
    public abstract partial class AuditedEntity : AuditedEntity<System.Guid>, IEntity
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected AuditedEntity() : base()
        {
            CreationTime = Clock.Now;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(ElementName name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(ElementName name, ElementDescription description) : base(name, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected AuditedEntity(System.Guid id) : base(id)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected AuditedEntity(System.Guid id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(System.Guid id, string name, CultureInfo culture) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(System.Guid id, ElementName name, CultureInfo culture) : base(id, name,culture)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(System.Guid id, ElementName name, ElementDescription description, CultureInfo culture) : base(id, name, description,culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected AuditedEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract partial class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AuditedEntity() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedEntity">FullAuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(ElementName name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(ElementName name, ElementDescription description) : base(name, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id) : base(id)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, string name, CultureInfo culture) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, ElementName name, CultureInfo culture) : base(id, name,culture)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, ElementName name, ElementDescription description, CultureInfo culture) : base(id, name, description,culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected AuditedEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("LastModifierUserId", LastModifierUserId);
        }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited{TUser}"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    [Serializable]
    public abstract partial class AuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey>, IAudited<TUser>
        where TUser : IEntity<System.Guid>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        [ForeignKey("LastModifierUserId")]
        public virtual TUser LastModifierUser { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AuditedEntity() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(ElementName name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(ElementName name, ElementDescription description) : base(name, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id) : base(id)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, string name, CultureInfo culture) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, ElementName name, CultureInfo culture) : base(id, name,culture)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AuditedEntity">AuditedEntity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AuditedEntity(TPrimaryKey id, ElementName name, ElementDescription description, CultureInfo culture) : base(id, name, description,culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected AuditedEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            CreatorUser = (TUser)info.GetValue("CreatorUser", typeof(TUser));
            LastModifierUser = (TUser)info.GetValue("LastModifierUser", typeof(TUser));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CreatorUser", CreatorUser);
            info.AddValue("LastModifierUser", LastModifierUser);
        }
    }
}