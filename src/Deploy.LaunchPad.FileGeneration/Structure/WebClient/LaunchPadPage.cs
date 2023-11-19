// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-20-2023
// ***********************************************************************
// <copyright file="LaunchPadPage.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FileTypeChecker.Types;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a page in the application.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Page")]
    public partial class LaunchPadPage : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Type of this page. Types can be "basic", "crud", "report" or "map".
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Domain entity that represents the backend service of this page.
        /// </summary>
        /// <value>The domain entity.</value>
        public string DomainEntity { get; set; }

        /// <summary>
        /// Tiles are blocks of label and icon with a link to other page
        /// </summary>
        /// <value>The tileset.</value>
        [XmlElement]
        public LaunchPadTileSet Tileset { get; set; }

        /// <summary>
        /// Datatable presenting a list of items with sorting, filter and pagination functionality.
        /// </summary>
        /// <value>The data table.</value>
        [XmlElement]
        public LaunchPadDataTable DataTable { get; set; }

        /// <summary>
        /// Web form object represents a form. It's structured as rows and columns, each column may be InputField or button of multiple types.
        /// </summary>
        /// <value>The web form.</value>
        [XmlElement]
        public LaunchPadWebForm WebForm { get; set; }

        /// <summary>
        /// Detail view represents a view page for the details of an item in the domain eneity.
        /// </summary>
        /// <value>The detail view.</value>
        [XmlElement]
        public LaunchPadDetailView DetailView { get; set; }

        /// <summary>
        /// Detail view represents a view page for the details of an item in the domain eneity.
        /// </summary>
        /// <value>The maps.</value>
        [XmlElement]
        public IList<LaunchPadMap> Maps { get; set; }

        /// <summary>
        /// Tiles with link to external URL and background, a bit different design in the frontend UI than the Tileset.
        /// </summary>
        /// <value>The tiles.</value>
        [XmlElement]
        public IList<LaunchPadTile> Tiles { get; set; }

        /// <summary>
        /// HTML content location, or the embedded HTML content
        /// </summary>
        /// <value>The HTML.</value>
        [XmlElement]
        public LaunchPadHtml Html { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadPage"/> class.
        /// </summary>
        public LaunchPadPage() : base()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadPage(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Type = info.GetString("Type");
            DomainEntity = info.GetString("DomainEntity");
            Tileset = (LaunchPadTileSet)info.GetValue("Tileset", typeof(LaunchPadTileSet));
            DataTable = (LaunchPadDataTable)info.GetValue("DataTable", typeof(LaunchPadDataTable));
            WebForm = (LaunchPadWebForm)info.GetValue("WebForm", typeof(LaunchPadWebForm));
            DetailView = (LaunchPadDetailView)info.GetValue("DetailView", typeof(LaunchPadDetailView));
            Maps = (IList<LaunchPadMap>)info.GetValue("Maps", typeof(List<LaunchPadMap>));
            Tiles = (IList<LaunchPadTile>)info.GetValue("Tiles", typeof(List<LaunchPadTile>));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Type", Type);
            info.AddValue("DomainEntity", DomainEntity);
            info.AddValue("Tileset", Tileset);
            info.AddValue("DataTable", DataTable);
            info.AddValue("WebForm", WebForm);
            info.AddValue("DetailView", DetailView);
            info.AddValue("Tiles", Tiles);
        }
    }
}
