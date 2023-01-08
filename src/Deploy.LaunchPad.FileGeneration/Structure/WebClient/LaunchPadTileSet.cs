using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a set of tiles being displayed in an area such as a <div> or it can take up the whole page.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "TileSet")]
    public partial class LaunchPadTileSet : LaunchPadWebClientObjectBase
    {

        public const int DefaultNumberOfDisplayColumns = 3;

        /// <summary>
        /// The number of columns displaying on this tile set
        /// </summary>
        [XmlAttribute("numberDisplayColumns")]
        public int NumberDisplayColumns { get; set; }

        /// <summary>
        /// Tiles included in this tile set. They will be displayed in an area according to the number of column specific. Scroll vertically, if the number of tiles exceed the column number.
        /// </summary>
        [XmlElement("tiles")]
        public IDictionary<string, LaunchPadTile> Tiles { get; set; }

        public LaunchPadTileSet() : base()
        {
            NumberDisplayColumns = DefaultNumberOfDisplayColumns;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tiles = new Dictionary<string, LaunchPadTile>(comparer);
        }

        public LaunchPadTileSet(int numberDisplayColumns) : base()
        {
            NumberDisplayColumns = numberDisplayColumns;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tiles = new Dictionary<string, LaunchPadTile>(comparer);
        }


        public LaunchPadTileSet(IDictionary<string, LaunchPadTile> tiles) : base()
        {
            NumberDisplayColumns = DefaultNumberOfDisplayColumns;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tiles = tiles;
        }
    }
}
