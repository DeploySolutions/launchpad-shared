// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="Elevation.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.Geospatial.ReferencePoint
{
    public partial class Elevation : IElevation
    {


        /// <summary>
        /// The minimum elevation
        /// </summary>
        protected double _minimum = 0.0;

        /// <summary>
        /// Gets or sets the minimum elevation.
        /// </summary>
        /// <value>The minimum elevation.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Minimum
        {
            get { return _minimum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);
                _minimum = value;
            }
        }

        /// <summary>
        /// The maximum elevation
        /// </summary>
        protected double _maximum = 0.0;

        /// <summary>
        /// Gets or sets the maximum elevation.
        /// </summary>
        /// <value>The maximum elevation.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Maximum
        {
            get { return _maximum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation);
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
    
        protected Elevation()
        {

        }

        public Elevation(double elevation)
        {
            Minimum = elevation;
            Maximum = elevation;
        }

        public Elevation(double elevation, string unitOfMeasure, double? confidence = null)
        {
            Minimum = elevation;
            Maximum = elevation;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }

        public Elevation(double minimum, double maximum, string unitOfMeasure, double? confidence = null)
        {
            Minimum = minimum;
            Maximum = maximum;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }
    }
}
