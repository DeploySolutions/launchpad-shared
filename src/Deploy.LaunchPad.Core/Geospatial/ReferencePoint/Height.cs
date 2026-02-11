// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="Height.cs" company="Deploy Software Solutions, inc.">
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
    public partial class Height : IHeight
    {


        /// <summary>
        /// The minimum Height
        /// </summary>
        protected double _minimum = 0.0;

        /// <summary>
        /// Gets or sets the minimum Height.
        /// </summary>
        /// <value>The minimum Height.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Minimum
        {
            get { return _minimum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Height);
                _minimum = value;
            }
        }

        /// <summary>
        /// The maximum Height
        /// </summary>
        protected double _maximum = 0.0;

        /// <summary>
        /// Gets or sets the maximum Height.
        /// </summary>
        /// <value>The maximum Height.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual double Maximum
        {
            get { return _maximum; }
            set
            {
                Guard.Against<ArgumentException>(double.IsNaN(value), Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Height);
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

        protected Height()
        {

        }

        public Height(double Height)
        {
            Minimum = Height;
            Maximum = Height;
        }

        public Height(double Height, string unitOfMeasure, double? confidence = null)
        {
            Minimum = Height;
            Maximum = Height;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }

        public Height(double minimum, double maximum, string unitOfMeasure, double? confidence = null)
        {
            Minimum = minimum;
            Maximum = maximum;
            UnitOfMeasure = unitOfMeasure;
            Confidence = confidence;
        }
    }
}
