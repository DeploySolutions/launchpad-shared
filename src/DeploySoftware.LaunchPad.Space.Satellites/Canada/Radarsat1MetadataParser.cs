//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Space.Satellites.Canada
{

    using CoordinateSharp;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using DeploySoftware.LaunchPad.Shared.Util;
    using DeploySoftware.LaunchPad.Space.Satellites.Common;
    using DeploySoftware.LaunchPad.Space.Satellites.Common.ObservationFiles;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Utility to parse a Radarsat1 image observation metadata file and populate a Radarsat1Observation object from it.
    /// </summary>
    public class Radarsat1MetadataParser
    {
        /// <summary>
        /// Main utility method to parse a Radarsat1 Metadata text file and populate the Radarsat1 Image Observation from the information
        /// contained within the file. 
        /// 
        /// The metadata is in RADARSAT CEOS format and is (c) Canadian Space Agency 1997 Agence spatiale canadienne, and processed and distributed by MDA Geospatial Services Inc.
        /// The data and metadata used within this software has been made freely available by the Canadian Federal Government under its Open Government initiative, 
        /// subject to the following license terms: https://open.canada.ca/en/open-government-licence-canada
        /// </summary>
        /// <param name="metadataFileKey">The filepath/filename of the Radarsat1 metadata file</param>
        /// <returns>A populated Radarsat1Observation object containing the metadata values, or null if the object couldn't be populated</returns>
        /// <throws>A FileLoadException if the file cannot be found or loaded</throws>
        public Radarsat1Observation GetRadarsat1ObservationFromMetadataFile(FileKey metadataFileKey)
        {
            Radarsat1Observation observation = null;
            // Radarsat 1 metadata files are in .txt format
            if (metadataFileKey.UniqueKey.EndsWith(".txt"))
            {
                String metadataFileText = String.Empty;
                try
                {   // Open the Radarsat1 Metadata text file
                    using (StreamReader sr = new StreamReader(metadataFileKey.UniqueKey, Encoding.GetEncoding("iso-8859-1")))
                    {
                        metadataFileText = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    throw new FileLoadException(ex.Message);
                }
                String radarsatUniqueID = Path.GetFileNameWithoutExtension(metadataFileKey.UniqueKey);
                String sceneID = metadataFileText.FindStringWithinAnchorText("SCENE_ID", "MDA ORDER NUMBER", true, true);
                String mdaOrderNumber = metadataFileText.FindStringWithinAnchorText("MDA ORDER NUMBER", "GEOGRAPHICAL AREA", true, true);
                String geographicalArea = metadataFileText.FindStringWithinAnchorText("GEOGRAPHICAL AREA", "SCENE START TIME", true, true);
                String sceneStartTimeText = metadataFileText.FindStringWithinAnchorText("SCENE START TIME", "SCENE STOP TIME", true, true);
                String dateFormatPattern = "MMM dd yyyy HH:mm:ss.FFF";
                DateTime sceneStartTime;
                if (String.IsNullOrEmpty(sceneStartTimeText))
                {
                    throw new ArgumentNullException("Radarsat1 Scene Start Time Cannot be null");
                }
                else
                {
                    DateTime.TryParseExact(sceneStartTimeText,
                                           dateFormatPattern,
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out sceneStartTime);
                }
                String sceneStopTimeText = metadataFileText.FindStringWithinAnchorText("SCENE STOP TIME", "ORBIT", true, true);
                DateTime sceneStopTime;
                if (String.IsNullOrEmpty(sceneStartTimeText))
                {
                    throw new ArgumentNullException("Radarsat1 Scene End Time Cannot be null");
                }
                else
                {

                    DateTime.TryParseExact(sceneStopTimeText,
                                           dateFormatPattern,
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out sceneStopTime);
                }
                String orbit = metadataFileText.FindStringWithinAnchorText("ORBIT", "ORBIT DATA TYPE", true, true);
                String orbitDataType = metadataFileText.FindStringWithinAnchorText("ORBIT DATA TYPE", "APPLICATION LUT", true, true);
                String applicationLut = metadataFileText.FindStringWithinAnchorText("APPLICATION LUT", "BEAM MODE", true, true);
                String beamMode = metadataFileText.FindStringWithinAnchorText("BEAM MODE", "PRODUCT TYPE", true, true);
                String productType = metadataFileText.FindStringWithinAnchorText("PRODUCT TYPE", "FORMAT", true, true);
                String format = metadataFileText.FindStringWithinAnchorText("FORMAT", "# OF IMAGE LINES", true, true);
                Int32 numberImageLines = -1;
                Int32.TryParse(metadataFileText.FindStringWithinAnchorText("# OF IMAGE LINES", "# OF IMAGE PIXELS", true, true), out numberImageLines);
                Int32 numberImagePixels = -1;
                Int32.TryParse(metadataFileText.FindStringWithinAnchorText("# OF IMAGE PIXELS", "PIXEL SPACING", true, true), out numberImagePixels);
                String pixelSpacing = metadataFileText.FindStringWithinAnchorText("PIXEL SPACING", "SCENE CENTRE", true, true);

                // when loading coordinates, disable celestial calculations (not needed)
                EagerLoad load = new EagerLoad
                {
                    Celestial = false,
                    Cartesian = true,
                    UTM_MGRS = true
                };

                // get the scene centre coordinates. Centre is spelled properly in Canadian, eh.
                String sceneCentre = metadataFileText.FindStringWithinAnchorText("SCENE CENTRE", "CORNER COORDINATES", true, true);
                string[] latLongSplit = sceneCentre.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Coordinate c = new Coordinate(load);
                c.Latitude = GetLatitude(latLongSplit[0], c);
                c.Longitude = GetLongitude(latLongSplit[1], c);
                GeographicLocation centre = new GeographicLocation
                (
                    c.Latitude.ToDouble(),
                    c.Longitude.ToDouble(),
                    c.EagerLoadSettings
                );

                // get the image corner coordinates
                String cornerCoordinatesString = metadataFileText.FindStringWithinAnchorText("CORNER COORDINATES:", "For information on RADARSAT CEOS format see README.TXT", true, true);
                ImageObservationCornerCoordinates cornerCoords = GetCornerCoordinates(cornerCoordinatesString);
                

                // Create a new Radarsat1 Earth Observation image
                observation = new Radarsat1Observation(
                    sceneID,
                    mdaOrderNumber,
                    geographicalArea,
                    sceneStartTime,
                    sceneStopTime,
                    orbit,
                    orbitDataType,
                    applicationLut,
                    beamMode,
                    productType,
                    format,
                    numberImageLines,
                    numberImagePixels,
                    pixelSpacing,
                    centre,
                    cornerCoords
                )
                {
                    Name = radarsatUniqueID,
                    Description = radarsatUniqueID
                };
                
            }
            observation.Files = LoadExpectedObservationFiles(observation, metadataFileKey);
            return observation;
        }

        protected Radarsat1Observation.Radarsat1ObservationFiles<Guid> LoadExpectedObservationFiles(Radarsat1Observation observation, FileKey metadataFileKey)
        {
            // get the list of related observation files
            String baseFilePath = metadataFileKey.UniqueKey.Substring(0, metadataFileKey.UniqueKey.Length - 4);
            IList<KeyValuePair<Radarsat1Observation.FileTypes, String>> expectedFiles = new List<KeyValuePair<Radarsat1Observation.FileTypes, String>>
            {
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.nvol,
                baseFilePath + "." + Radarsat1Observation.FileTypes.nvol
                ),
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.sard, baseFilePath + "." + Radarsat1Observation.FileTypes.sard
                ),
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.sarl, baseFilePath + "." + Radarsat1Observation.FileTypes.sarl
                ),
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.sart, baseFilePath + "." + Radarsat1Observation.FileTypes.sart
                ),
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.tfw, baseFilePath + "." + Radarsat1Observation.FileTypes.tfw
                ),
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.tif, baseFilePath + "." + Radarsat1Observation.FileTypes.tif
                ),
                new KeyValuePair<Radarsat1Observation.FileTypes, string>(
                Radarsat1Observation.FileTypes.vol, baseFilePath + "." + Radarsat1Observation.FileTypes.vol
                )
            };

            // initialize the list of observation files
            Radarsat1Observation.Radarsat1ObservationFiles<Guid> observationFiles = new Radarsat1Observation.Radarsat1ObservationFiles<Guid>();

            // add each expected file type (if it exists)
            if (File.Exists(expectedFiles[0].Value))
            {
                observationFiles.Nvol = new NvolFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[0].Value),
                    FilePath = expectedFiles[0].Value
                };
            }
            if (File.Exists(expectedFiles[1].Value))
            {
                observationFiles.Sard = new SardFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[1].Value),
                    FilePath = expectedFiles[1].Value
                };
            }
            if (File.Exists(expectedFiles[2].Value))
            {
                observationFiles.Sarl = new SarlFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[2].Value),
                    FilePath = expectedFiles[2].Value
                };
            }
            if (File.Exists(expectedFiles[3].Value))
            {
                observationFiles.Sart = new SartFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[3].Value),
                    FilePath = expectedFiles[3].Value
                };
            }
            if (File.Exists(expectedFiles[4].Value))
            {
                observationFiles.Tfw = new TfwFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[4].Value),
                    FilePath = expectedFiles[4].Value
                };
            }
            if (File.Exists(expectedFiles[5].Value))
            {
                observationFiles.Tif = new TifFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[5].Value),
                    FilePath = expectedFiles[5].Value
                };
            }
            if (File.Exists(expectedFiles[6].Value))
            {
                observationFiles.Vol = new VolFile<Guid>()
                {
                    Id = Guid.NewGuid(),
                    ParentObservationKey = observation.GlobalKey,
                    FileName = Path.GetFileName(expectedFiles[6].Value),
                    FilePath = expectedFiles[6].Value
                };
            }
            return observationFiles;
        }

        /// <summary>
        /// Returns a series of four corner coordinates from a provided string of coordinate information.
        /// </summary>
        /// <param name="cornerCoordinatesString">The coordinate information to parse</param>
        /// <returns>An ImageObservationCornerCoordinates object containing four coordinate points for each side of a square</returns>
        protected ImageObservationCornerCoordinates GetCornerCoordinates(string cornerCoordinatesString)
        {
            string[] coordinates = Regex.Split(cornerCoordinatesString, @"(?<=[N,S,E,W])");
            // disable the celestial information (not needed)
            EagerLoad load = new EagerLoad
            {
                Celestial = false,
                Cartesian = true,
                UTM_MGRS = true
            };
            ImageObservationCornerCoordinates cornerCoords = new ImageObservationCornerCoordinates(load);

            // upper left latitude
            if (!String.IsNullOrEmpty(coordinates[0]))
            {
                cornerCoords.UpperLeft.Latitude = GetLatitude(coordinates[0].Trim(), cornerCoords.UpperLeft);
            }
            // upper left longitude
            if (!String.IsNullOrEmpty(coordinates[2]))
            {
                cornerCoords.UpperLeft.Longitude = GetLongitude(coordinates[2].Trim(), cornerCoords.UpperLeft);
            }
            // upper right latitude
            if (!String.IsNullOrEmpty(coordinates[1]))
            {
                cornerCoords.UpperRight.Latitude = GetLatitude(coordinates[1].Trim(), cornerCoords.UpperRight);

            }
            // upper right longitude
            if (!String.IsNullOrEmpty(coordinates[3]))
            {
                cornerCoords.UpperRight.Longitude = GetLongitude(coordinates[3].Trim(), cornerCoords.UpperRight);
            }
            // lower left latitude
            if (!String.IsNullOrEmpty(coordinates[4]))
            {
                cornerCoords.LowerLeft.Latitude = GetLatitude(coordinates[4].Trim(), cornerCoords.LowerLeft);

            }
            // lower left longitude
            if (!String.IsNullOrEmpty(coordinates[6]))
            {
                cornerCoords.LowerLeft.Longitude = GetLongitude(coordinates[6].Trim(), cornerCoords.LowerLeft);
            }
            // lower right latitude
            if (!String.IsNullOrEmpty(coordinates[5]))
            {
                cornerCoords.LowerRight.Latitude = GetLatitude(coordinates[5].Trim(), cornerCoords.LowerRight);
            }
            // lower right longitude
            if (!String.IsNullOrEmpty(coordinates[7]))
            {
                cornerCoords.LowerRight.Longitude = GetLongitude(coordinates[7].Trim(), cornerCoords.LowerRight);
            }
            return cornerCoords;
        }

        /// <summary>
        /// Gets a latitude coordinate part from a provided string
        /// </summary>
        /// <param name="v">The latitude value to attempt to populate from</param>
        /// <param name="c">The coordinate which contains this latitude coordinate part</param>
        /// <returns>A coordinate containing the parsed latitude</returns>
        protected CoordinatePart GetLatitude(string v, Coordinate c)
        {
            CoordinatePart cp = null;
            string latitudeCoordinatesPosition = v.Substring(v.Length - 1, 1);
            string latitudeCoordinates = v.Substring(0, v.Length - 1);
            string degreeString = StringParser.FindStringWithinAnchorText(v, String.Empty, "°", true, true);
            Int32.TryParse(degreeString, out int degree);
            string minuteString = StringParser.FindStringWithinAnchorText(v, "°", "'", true, true);
            Int32.TryParse(minuteString, out int minute);
            string secondString = StringParser.FindStringWithinAnchorText(v, "'", "\"", true, true);
            Double.TryParse(secondString, out double second);

            if (latitudeCoordinatesPosition == "N")
            {
                cp = new CoordinatePart(degree, minute, second, CoordinatesPosition.N, c);
            }
            else
            {
                cp = new CoordinatePart(degree, minute, second, CoordinatesPosition.S, c);
            }
            return cp;
        }

        /// <summary>
        /// Gets a longitude coordinate part from a provided string
        /// </summary>
        /// <param name="v">The longitude value to attempt to populate from</param>
        /// <param name="c">The coordinate which contains this longitude coordinate part</param>
        /// <returns>A coordinate containing the parsed longitude</returns>
        protected CoordinatePart GetLongitude(string v, Coordinate c)
        {
            CoordinatePart cp = null;
            string longitudeCoordinatesPosition = v.Substring(v.Length - 1, 1);
            string longitudeCoordinates = v.Substring(0, v.Length - 1);
            string degreeString = StringParser.FindStringWithinAnchorText(v,String.Empty,"°",true,true);
            Int32.TryParse(degreeString, out int degree);
            string minuteString = StringParser.FindStringWithinAnchorText(v, "°", "'", true, true);
            Int32.TryParse(minuteString, out int minute);
            string secondString = StringParser.FindStringWithinAnchorText(v, "'", "\"", true, true);
            Double.TryParse(secondString, out double second);
            
            if (longitudeCoordinatesPosition == "W")
            {
                cp = new CoordinatePart(degree, minute, second, CoordinatesPosition.W, c);
            }
            else
            {
                cp = new CoordinatePart(degree, minute, second, CoordinatesPosition.E, c);
            }
            return cp;
        }
    }
}
