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


using System;
using System.Globalization;
using System.IO;
using System.Text;
using DeploySoftware.LaunchPad.Shared.Domain;

namespace DeploySoftware.LaunchPad.Shared.Util
{
    public class TifWorldFileParser<TPrimaryKey>
    {
        public TifWorldFile<TPrimaryKey> GetTifWorldFileFromMetadataFile(FileKey metadataFileKey)
        {
            TifWorldFile<TPrimaryKey> file = null;
            // Tif World File metadata files are in .tfw format
            // ReSharper disable once RedundantAssignment
            var metadataFileText = string.Empty;

            if (!metadataFileKey.UniqueKey.EndsWith(".tfw")) return null;
            try
            {
                // Open the Metadata text file
                using (StreamReader sr = new StreamReader(metadataFileKey.UniqueKey,
                    Encoding.GetEncoding("iso-8859-1")))
                {
                    decimal a = new decimal();
                    decimal d = new decimal();
                    decimal b = new decimal();
                    decimal e = new decimal();
                    decimal c = new decimal();
                    decimal f = new decimal();
                    string lineText;
                    int lineNumber = 1;
                    while ((lineText = sr.ReadLine()) != null)
                    {
                        if (lineNumber == 1)
                        {
                            a = Decimal.Parse(lineText, NumberStyles.Float);
                        }
                        else if (lineNumber == 2)
                        {
                            d = Decimal.Parse(lineText, NumberStyles.Float);
                        }
                        else if (lineNumber == 3)
                        {
                            b = Decimal.Parse(lineText, NumberStyles.Float);
                        }
                        else if (lineNumber == 4)
                        {
                            e = Decimal.Parse(lineText, NumberStyles.Float);
                        }
                        else if (lineNumber == 5)
                        {
                            c = Decimal.Parse(lineText, NumberStyles.Float);
                        }
                        else if (lineNumber == 6)
                        {
                            f = Decimal.Parse(lineText, NumberStyles.Float);
                        }

                        lineNumber++;
                    }

                    file = new TifWorldFile<TPrimaryKey>()
                    {
                        A = a,
                        D = d,
                        B = b,
                        E = e,
                        C = c,
                        F = f
                    };
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }

            return file;
        }
    }
}
