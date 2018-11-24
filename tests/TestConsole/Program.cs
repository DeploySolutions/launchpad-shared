using DeploySoftware.LaunchPad.Shared.Domain;
using DeploySoftware.LaunchPad.Space.Satellites.Canada;
using Elasticsearch.Net;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
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

namespace DeploySoftware.LaunchPad.Space.Tests.TestConsole
{
    using DeploySoftware.LaunchPad.Images;
    using ImageMagick;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using static DeploySoftware.LaunchPad.Images.ImageManager;

    class Program
    {
        protected static readonly string DataRootPath = @"F:\Data\Radarsat1\";

        static void Main(string[] args)
        {
            RadarsatImageSearchExample();
            CompareSatelliteImageExample();
        }

        private static void RadarsatImageSearchExample()
        {
            // Load a set of images from Radarsat1 metadata
            List<Radarsat1Observation> images = new List<Radarsat1Observation>();
            Radarsat1Observation image = null;
            Radarsat1MetadataParser parser = new Radarsat1MetadataParser();
            string[] files = Directory.GetFiles(DataRootPath, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                if (file.EndsWith(".txt")) // only load the metadata files
                {
                    image = parser.GetRadarsat1ObservationFromMetadataFile(new FileKey(
                                       file)
                                    );
                    images.Add(image);
                }

            }

            // setup Elasticsearch
            var pool = new SingleNodeConnectionPool(
                new Uri(System.Configuration.ConfigurationManager.AppSettings["ElasticSearchUri"]));
            var settings = new ConnectionSettings(pool, (builtInSerializer, connectionSettings) =>
            new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }))
            .DefaultIndex("radarsat1")
            ;

            ElasticClient _elasticSearch = new ElasticClient(settings);
            _elasticSearch.DeleteIndex("radarsat1"); // delete previous index for testing purposes
            _elasticSearch.CreateIndex("radarsat1"); // create the new index
            foreach (Radarsat1Observation img in images)
            {
                _elasticSearch.IndexDocument(img); // add the images to the index
            }
        }

        private static void CompareSatelliteImageExample()
        {
            ImageManager imageMan = new ImageManager();
            MagickImage imageA = imageMan.GetMagickImageFromFile(
                new FileInfo(DataRootPath + @"Tuktoyaktuk\Radarsat1_Images\RS1_m0700829_S7_19970329_151016_HH_SGF\RS1_m0700829_S7_19970329_151016_HH_SGF.tif"));
            MagickImage imageB = imageMan.GetMagickImageFromFile(
                new FileInfo(DataRootPath + @"Tuktoyaktuk\Radarsat1_Images\RS1_m0700835_S5_20120919_152932_HH_SGF\RS1_m0700835_S5_20120919_152932_HH_SGF.tif"));
            CompareSettings compareSettings = new CompareSettings()
            {
                Metric = ErrorMetric.Absolute,
                HighlightColor = MagickColor.FromRgb(255, 255, 255), // white
                LowlightColor = MagickColor.FromRgb(0, 0, 0), // black

            };
            FileInfo info = new FileInfo(DataRootPath + @"Tuktoyaktuk\compareA.tif");
            byte[] diffImage = imageMan.CompareImages(imageA, imageB, compareSettings);
            byte[] thumbImage = imageMan.GetThumbnailFromImage(diffImage, ThumbnailSize.Large);
            FileInfo thumbFile = new FileInfo(DataRootPath + @"Tuktoyaktuk\compareA_thumb_medium.jpeg");
            
            if (diffImage.Length > 0)
            {
                using (var fs = new FileStream(info.FullName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(diffImage, 0, diffImage.Length);
                }
            }
            if (thumbImage.Length > 0)
            {
                using (var fs = new FileStream(thumbFile.FullName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(thumbImage, 0, thumbImage.Length);
                }
            }

            compareSettings.Metric = ErrorMetric.Fuzz;
            FileInfo info2 = new FileInfo(DataRootPath + @"Tuktoyaktuk\compareFuzz.tif");
            imageMan.CompareImages(imageA, imageB, compareSettings);

            compareSettings.Metric = ErrorMetric.MeanAbsolute;
            FileInfo info3 = new FileInfo(DataRootPath + @"Tuktoyaktuk\compareMeanAbsolute.tif");
            imageMan.CompareImages(imageA, imageB, compareSettings);


        }
    }
}
