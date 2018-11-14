using DeploySoftware.LaunchPad.Shared.Domain;
using DeploySoftware.LaunchPad.Shared.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Canada
{
    public static class Radarsat1MetadataParser
    {
        public static Radarsat1Observation GetRadarsat1ObservationFromMetadataFile(FileKey metadataFileKey)
        {
            String metadataFileText = String.Empty;
            try
            {   // Open the Radarsat1 Metadata text file
                using (StreamReader sr = new StreamReader(metadataFileKey.UniqueKey))
                {
                    metadataFileText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }
            String sceneID = metadataFileText.FindStringWithinAnchorText("SCENE_ID", "MDA ORDER NUMBER", true, true);
            String mdaOrderNumber = metadataFileText.FindStringWithinAnchorText("MDA ORDER NUMBER", "GEOGRAPHICAL AREA", true, true);
            String geographicalArea = metadataFileText.FindStringWithinAnchorText("GEOGRAPHICAL AREA","SCENE START TIME", true, true);
            String sceneStartTimeText = metadataFileText.FindStringWithinAnchorText("SCENE START TIME","SCENE STOP TIME", true, true);
            String dateFormatPattern = "MMM dd yyyy HH:mm:ss.FFF";
            DateTime sceneStartTime;
            if (String.IsNullOrEmpty(sceneStartTimeText))
            {
                throw new Exception("Radarsat1 Scene Start Time Cannot be null");
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
                throw new Exception("Radarsat1 Scene End Time Cannot be null");
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
            String productType = metadataFileText.FindStringWithinAnchorText("PRODUCT TYPE","FORMAT", true, true);
            String format = metadataFileText.FindStringWithinAnchorText("FORMAT", "# OF IMAGE LINES", true, true);
            Int32 numberImageLines = -1;
            Int32.TryParse(metadataFileText.FindStringWithinAnchorText("# OF IMAGE LINES", "# OF IMAGE PIXELS", true, true), out numberImageLines);
            Int32 numberImagePixels = -1;
            Int32.TryParse(metadataFileText.FindStringWithinAnchorText("# OF IMAGE PIXELS","PIXEL SPACING", true, true), out numberImagePixels);
            String pixelSpacing = metadataFileText.FindStringWithinAnchorText("PIXEL SPACING","SCENE CENTRE", true, true);
            String sceneCentre = metadataFileText.FindStringWithinAnchorText("SCENE CENTRE", "CORNER COORDINATES", true, true);
            string[] split = sceneCentre.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string s in split)
            {
                GeographicLocation centre = new GeographicLocation
               (
                    s[0],
                    s[1]
               );
            }
           
            Radarsat1Observation observation = new Radarsat1Observation(
                String.Empty,
                sceneID,
                mdaOrderNumber,
                geographicalArea,
                sceneStartTime,
                sceneStopTime
            )
            {
                Orbit = orbit,
                OrbitDataType = orbitDataType,
                ApplicationLut = applicationLut,
                BeamMode = beamMode,
                ProductType = productType,
                Format =  format,
                NumberImageLines = numberImageLines,
                NumberImagePixels = numberImagePixels,
                PixelSpacing = pixelSpacing
            };
            
            return observation;
        }
    }
}
