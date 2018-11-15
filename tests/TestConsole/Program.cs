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
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            // Load a set of images from Radarsat1 metadata
            List<Radarsat1Observation> images = new List<Radarsat1Observation>();
            Radarsat1Observation image = null;
            Radarsat1MetadataParser parser = new Radarsat1MetadataParser();
            string[] files = Directory.GetFiles("F:\\Data\\Radarsat1", "*", SearchOption.AllDirectories);
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
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
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
    }
}
