// SPDX-License-Identifier: Apache-2.0
//
// This file contains code originally from the H3.net project:
//   Repository: https://github.com/pocketken/H3.net/
//   Original license: Apache License, Version 2.0
//
// Modifications in this forked/copied version:
//   - Integrated into Deploy.LaunchPad.Geospatial (namespace adjustments)
//   - Local fixes and adaptations (see git history for details)
//
// Original Copyright (c) Ken March (pocketken), https://github.com/pocketken
// Modified files Copyright (c) Deploy Software Solutions, 2026
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Linq;
using static Deploy.LaunchPad.Geospatial.H3.H3Constants;

#nullable enable

namespace Deploy.LaunchPad.Geospatial.H3.Model;

public partial class PentagonDirectionToFaceMapping {
    public int BaseCellNumber { get; init; }
    public int[] Faces { get; init; } = new int[NUM_PENT_VERTS];

    public BaseCell BaseCell => BaseCells.Cells[BaseCellNumber];

    public static PentagonDirectionToFaceMapping ForBaseCell(int baseCellNumber) =>
        LookupTables.PentagonDirectionFaces.First(df => df.BaseCellNumber == baseCellNumber);

    public static implicit operator PentagonDirectionToFaceMapping((int, (int, int, int, int, int)) data) {
        return new PentagonDirectionToFaceMapping {
            BaseCellNumber = data.Item1,
            Faces = new[] {
                data.Item2.Item1,
                data.Item2.Item2,
                data.Item2.Item3,
                data.Item2.Item4,
                data.Item2.Item5
            }
        };
    }
}