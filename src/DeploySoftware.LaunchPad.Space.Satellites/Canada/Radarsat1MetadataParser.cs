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
            catch (Exception e)
            {
                //Console.WriteLine("The file could not be read:");
                //Console.WriteLine(e.Message);
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
            Radarsat1Observation observation = new Radarsat1Observation(
                String.Empty,
                sceneID,
                mdaOrderNumber,
                geographicalArea,
                sceneStartTime,
                sceneStopTime
            );                
            return observation;
        }
    }
}
