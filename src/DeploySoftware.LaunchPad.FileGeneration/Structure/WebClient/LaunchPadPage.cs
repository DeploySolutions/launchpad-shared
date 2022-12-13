using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
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
        public string Type { get; set; }

        /// <summary>
        /// Domain entity that represents the backend service of this page.
        /// </summary>
        public string DomainEntity { get; set; }

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

        /// <summary>
        /// Web form object represents a form. It's structured as rows and columns, each column may be InputField or button of multiple types.
        /// </summary>
        [XmlElement]
        public LaunchPadWebForm WebForm { get; set; }

        /// <summary>
        /// Detail view represents a view page for the details of an item in the domain eneity.
        /// </summary>
        [XmlElement]
        public LaunchPadDetailView DetailView { get; set; }

        /// <summary>
        /// Detail view represents a view page for the details of an item in the domain eneity.
        /// </summary>
        [XmlElement]
        public IList<LaunchPadMap> Maps { get; set; }

        /// <summary>
        /// Tiles with link to external URL and background, a bit different design in the frontend UI than the Tileset. 
        /// </summary>
        [XmlElement]
        public IList<LaunchPadTile> Tiles { get; set; }

        public LaunchPadPage() : base()
        {
        }
    }
}
