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

using Deploy.LaunchPad.Core.Events;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Entities
{
    public partial class AggregateRoot : AggregateRoot<System.Guid>, IAggregateRoot
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AggregateRoot() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected AggregateRoot(System.Guid id) : base(id)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAggregateRoot">FullAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AggregateRoot(Guid id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected AggregateRoot(Guid id, ElementName name) : base(id, name.Name)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        protected AggregateRoot(System.Guid id, ElementName name, CultureInfo culture) : base(id, name, culture)
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected AggregateRoot(SerializationInfo info, StreamingContext context) : base(info, context)
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

    public partial class AggregateRoot<TPrimaryKey> : FrameworkEntityBase<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        [NotMapped]
        public virtual ICollection<IEventData> DomainEvents { get; }

        /// <summary>
        /// If this object is a regular domain entity, an aggregate root, or an aggregate child
        /// </summary>
        /// <value>The type of the entity.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public override EntityType EntityType { get; } = EntityType.AggregateRoot;

        public AggregateRoot()
        {
            DomainEvents = new Collection<IEventData>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FullAggregateRoot">FullAggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        public AggregateRoot(TPrimaryKey id, string name) : base(id, name)
        {
            DomainEvents = new Collection<IEventData>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        public AggregateRoot(TPrimaryKey id, ElementName name) : base(id, name.Name)
        {
            DomainEvents = new Collection<IEventData>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        public AggregateRoot(TPrimaryKey id) : base(id)
        {
            DomainEvents = new Collection<IEventData>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        public AggregateRoot(TPrimaryKey id, string name, CultureInfo culture) : base(id, name)
        {
            DomainEvents = new Collection<IEventData>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRoot">AggregateRoot</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]
        public AggregateRoot(TPrimaryKey id, ElementName name, CultureInfo culture) : base(id, name, culture)
        {
            DomainEvents = new Collection<IEventData>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public AggregateRoot(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DomainEvents = (Collection<IEventData>)info.GetValue("DomainEvents", typeof(Collection<IEventData>));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("DomainEvents", DomainEvents);
        }
    }
}