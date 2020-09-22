//LaunchPad Space
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{
    using CoordinateSharp;
    using DeploySoftware.LaunchPad.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ImageObservationCornerCoordinates
    {
        public Coordinate UpperLeft { get; set; }
        public Coordinate UpperRight { get; set; }
        public Coordinate LowerLeft { get; set; }
        public Coordinate LowerRight { get; set; }

        public ImageObservationCornerCoordinates()
        {
            UpperLeft = new Coordinate();
            UpperLeft.EagerLoadSettings.Celestial = false;
            UpperRight = new Coordinate();
            UpperRight.EagerLoadSettings.Celestial = false;
            LowerLeft = new Coordinate();
            LowerLeft.EagerLoadSettings.Celestial = false;
            LowerRight = new Coordinate();
            LowerRight.EagerLoadSettings.Celestial = false;
        }

        public ImageObservationCornerCoordinates(EagerLoad load)
        {
            UpperLeft = new Coordinate(load);
            UpperRight = new Coordinate(load);
            LowerLeft = new Coordinate(load);
            LowerRight = new Coordinate(load);
        }
    }
}
