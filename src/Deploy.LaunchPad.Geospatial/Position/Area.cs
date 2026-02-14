// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="Area.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Util;
using NetTopologySuite.Algorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Geospatial.Position
{
    public partial class Area : IArea
    {


        /// <summary>
        /// The minimum Area
        /// </summary>
        protected double _minimum = 0.0;

        /// <summary>
        /// Gets or sets the minimum Area.
        /// </summary>
        /// <value>The minimum Area.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Minimum
        {
            get { return _minimum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Geospatial_Resources.Guard_GeographicLocation_Set_Area);
                _minimum = value;
            }
        }

        /// <summary>
        /// The maximum Area
        /// </summary>
        protected double _maximum = 0.0;

        /// <summary>
        /// Gets or sets the maximum Area.
        /// </summary>
        /// <value>The maximum Area.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Maximum
        {
            get { return _maximum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Geospatial_Resources.Guard_GeographicLocation_Set_Area);
                _maximum = value;
            }
        }

        // IMustHaveUnitOfMeasure
        public virtual string UnitOfMeasure { get; set; } = "meters";

        // IMayHaveConfidence

        /// <summary>
        /// Gets or sets the level of confidence/accuracy of a measurement.
        /// </summary>
        /// <value>The confidence level of a measurement.</value>
        public virtual double? Confidence { get; set; }

        protected Area()
        {

        }

        public Area(double Area)
        {
            Minimum = Area;
            Maximum = Area;
        }

        public Area(double Area, string unitOfMeasure, double? confidence = null)
        {
            Minimum = Area;
            Maximum = Area;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }

        public Area(double minimum, double maximum, string unitOfMeasure, double? confidence = null)
        {
            Minimum = minimum;
            Maximum = maximum;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }
    }
}
