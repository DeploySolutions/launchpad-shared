using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a set of tiles being displayed in an area such as a <div> or it can take up the whole page.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Tiles")]
    public partial class LaunchPadTiles
    {
        /// <summary>
        /// The number of columns displaying on this tile set
        /// </summary>
        [XmlAttribute("column")]
        public int Column { get; set; }

        /// <summary>
        /// Tiles included in this tile set. They will be displayed in an area according to the number of column specific. Scroll vertically, if the number of tiles exceed the column number.
        /// </summary>
        [XmlElement("Tile")]
        public IList<LaunchPadTile> Tiles { get; set; }

        public LaunchPadTiles()
        {
            Column = 3;
            Tiles = new List<LaunchPadTile>();
        }
    }
}
