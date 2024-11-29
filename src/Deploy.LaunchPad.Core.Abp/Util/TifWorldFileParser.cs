// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="TifWorldFileParser.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

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


using Deploy.LaunchPad.Core.Abp.Domain;
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Files;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Deploy.LaunchPad.Core.Abp.Util
{
    /// <summary>
    /// Class TifWorldFileParser.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TFileStorageLocationType">The type of the t file storage location type.</typeparam>
    public partial class TifWorldFileParser<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public TFileStorageLocationType Location { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TifWorldFileParser{TPrimaryKey, TFileStorageLocationType}"/> class.
        /// </summary>
        public TifWorldFileParser()
        {
            Location = new TFileStorageLocationType();
        }

        /// <summary>
        /// Gets the tif world file from metadata file.
        /// </summary>
        /// <param name="metadataFileName">Name of the metadata file.</param>
        /// <returns>TifWorldFile&lt;TPrimaryKey&gt;.</returns>
        /// <exception cref="System.IO.FileLoadException"></exception>
        public TifWorldFile<TPrimaryKey> GetTifWorldFileFromMetadataFile(string metadataFileName)
        {
            TifWorldFile<TPrimaryKey> file = null;
            // Tif World File metadata files are in .tfw format
            // ReSharper disable once RedundantAssignment
            var metadataFileText = string.Empty;

            if (!metadataFileName.EndsWith(".tfw")) return null;
            try
            {
                // Open the Metadata text file
                using (StreamReader sr = new StreamReader(metadataFileName,
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
