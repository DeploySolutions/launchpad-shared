﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a page in the application.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Page")]
    public partial class LaunchPadPage : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Tiles are blocks of label and icon with a link to other page
        /// </summary>
        [XmlElement]
        public LaunchPadTileSet Tileset { get; set; }

        /// <summary>
        /// Datatable presenting a list of items with sorting, filter and pagination functionality.
        /// </summary>
        [XmlElement]
        public LaunchPadDataTable DataTable { get; set; }

        public LaunchPadPage() : base()
        {
            Tileset = new LaunchPadTileSet();
            DataTable = new LaunchPadDataTable();
        }
    }
}
