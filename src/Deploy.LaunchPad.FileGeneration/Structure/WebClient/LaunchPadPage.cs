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
            Maps = new List<LaunchPadMap>();
            Tiles = new List<LaunchPadTile>();
            Tileset = new LaunchPadTileSet();
            WebForm = new LaunchPadWebForm();
            DetailView = new LaunchPadDetailView();
            DataTable = new LaunchPadDataTable();
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
            Maps = (IList<LaunchPadMap>)info.GetValue("Maps", typeof(IList<LaunchPadMap>));
            Tiles = (IList<LaunchPadTile>)info.GetValue("Tiles", typeof(IList<LaunchPadTile>));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
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
