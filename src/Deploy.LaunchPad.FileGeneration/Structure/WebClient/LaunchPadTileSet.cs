// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadTileSet.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class LaunchPadTileSet.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadWebClientObjectBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadWebClientObjectBase" />
    /// <font color="red">Badly formed XML comment.</font>
    [Serializable]
    [XmlRoot(ElementName = "TileSet")]
    public partial class LaunchPadTileSet : LaunchPadWebClientObjectBase
    {

        /// <summary>
        /// The default number of display columns
        /// </summary>
        public const int DefaultNumberOfDisplayColumns = 3;

        /// <summary>
        /// The number of columns displaying on this tile set
        /// </summary>
        /// <value>The number display columns.</value>
        [XmlAttribute("numberDisplayColumns")]
        public int NumberDisplayColumns { get; set; }

        /// <summary>
        /// Tiles included in this tile set. They will be displayed in an area according to the number of column specific. Scroll vertically, if the number of tiles exceed the column number.
        /// </summary>
        /// <value>The tiles.</value>
        [XmlElement("tiles")]
        public IDictionary<string, LaunchPadTile> Tiles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTileSet"/> class.
        /// </summary>
        public LaunchPadTileSet() : base()
        {
            NumberDisplayColumns = DefaultNumberOfDisplayColumns;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tiles = new Dictionary<string, LaunchPadTile>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTileSet"/> class.
        /// </summary>
        /// <param name="numberDisplayColumns">The number display columns.</param>
        public LaunchPadTileSet(int numberDisplayColumns) : base()
        {
            NumberDisplayColumns = numberDisplayColumns;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tiles = new Dictionary<string, LaunchPadTile>(comparer);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTileSet"/> class.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        public LaunchPadTileSet(IDictionary<string, LaunchPadTile> tiles) : base()
        {
            NumberDisplayColumns = DefaultNumberOfDisplayColumns;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Tiles = tiles;
        }
    }
}
