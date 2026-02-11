// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="Altitude.cs" company="Deploy Software Solutions, inc.">
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
    public partial class Altitude : IAltitude
    {


        /// <summary>
        /// The minimum Altitude
        /// </summary>
        protected double _minimum = 0.0;

        /// <summary>
        /// Gets or sets the minimum Altitude.
        /// </summary>
        /// <value>The minimum Altitude.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Minimum
        {
            get { return _minimum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Altitude);
                _minimum = value;
            }
        }

        /// <summary>
        /// The maximum Altitude
        /// </summary>
        protected double _maximum = 0.0;

        /// <summary>
        /// Gets or sets the maximum Altitude.
        /// </summary>
        /// <value>The maximum Altitude.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Maximum
        {
            get { return _maximum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Altitude);
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

        protected Altitude()
        {

        }

        public Altitude(double Altitude, double? confidence = null)
        {
            Minimum = Altitude;
            Maximum = Altitude;
            Confidence = confidence;
        }

        public Altitude(double Altitude, string unitOfMeasure, double? confidence = null)
        {
            Minimum = Altitude;
            Maximum = Altitude;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }

        public Altitude(double minimum, double maximum, string unitOfMeasure, double? confidence = null)
        {
            Minimum = minimum;
            Maximum = maximum;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }
    }
}
