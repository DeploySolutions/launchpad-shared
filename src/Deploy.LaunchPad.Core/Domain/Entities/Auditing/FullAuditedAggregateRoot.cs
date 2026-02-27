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
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Entities.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="FullAuditedAggregateRoot{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract partial class FullAuditedAggregateRoot : FullAuditedAggregateRoot<System.Guid>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        protected FullAuditedAggregateRoot() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullFullAuditedAggregateRoot">FullFullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(ElementName name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(ElementName name, ElementDescription description) : base(name, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(System.Guid id) : base(id)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(System.Guid id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(System.Guid id, string name, CultureInfo culture) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(System.Guid id, ElementName name, CultureInfo culture) : base(id, name, culture)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(System.Guid id, ElementName name, ElementDescription description, CultureInfo culture) : base(id, name, description, culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(SerializationInfo info, StreamingContext context) : base(info, context)
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
    /// Implements <see cref="IFullAudited"/> to be a base class for full-audited aggregate roots.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract partial class FullAuditedAggregateRoot<TPrimaryKey> : AuditedAggregateRoot<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// Is this entity Deleted?
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected FullAuditedAggregateRoot() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullFullAuditedAggregateRoot">FullFullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(ElementName name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(ElementName name, ElementDescription description) : base(name, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id) : base(id)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, string name, CultureInfo culture) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, ElementName name, CultureInfo culture) : base(id, name, culture)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, ElementName name, ElementDescription description, CultureInfo culture) : base(id, name, description, culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = (long?)info.GetValue("DeleterUserId", typeof(long?));
            DeletionTime = (DateTime?)info.GetValue("DeletionTime", typeof(DateTime?));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
        }
    }

    /// <summary>
    /// Implements <see cref="IFullAudited{TUser}"/> to be a base class for full-audited aggregate roots.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    [Serializable]
    public abstract partial class FullAuditedAggregateRoot<TPrimaryKey, TUser> : AuditedAggregateRoot<TPrimaryKey, TUser>, IFullAudited<TUser>
        where TUser : IEntity<System.Guid>
    {
        /// <summary>
        /// Is this entity Deleted?
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        [ForeignKey("DeleterUserId")]
        public virtual TUser DeleterUser { get; set; }

        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        protected FullAuditedAggregateRoot() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullFullAuditedAggregateRoot">FullFullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(ElementName name) : base(name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(ElementName name, ElementDescription description) : base(name, description)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id) : base(id)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, string name, CultureInfo culture) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, ElementName name, CultureInfo culture) : base(id, name, culture)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAuditedAggregateRoot">FullAuditedAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(TPrimaryKey id, ElementName name, ElementDescription description, CultureInfo culture) : base(id, name, description, culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        [SetsRequiredMembers]
        protected FullAuditedAggregateRoot(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = (long?)info.GetValue("DeleterUserId", typeof(long?));
            DeletionTime = (DateTime?)info.GetValue("DeletionTime", typeof(DateTime?));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
        }
    }
}