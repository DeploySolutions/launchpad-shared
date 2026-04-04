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

namespace Deploy.LaunchPad.Geospatial.H3.Data; 

/// <summary>
/// Definition for one of the 122 base cells that form the H3 indexing scheme.
/// </summary>
public sealed partial class BaseCellData {
    /// <summary>
    /// The cell number, from 0 - 121.
    /// </summary>
    public int Cell { get; private set; }

    /// <summary>
    /// The home face and IJK address of the cell.
    /// </summary>
    public BaseCellHomeAddress Home { get; private set; }

    /// <summary>
    /// Whether or not this base cell is a pentagon.
    /// </summary>
    public bool IsPentagon { get; private set; }

    /// <summary>
    /// If a pentagon, the cell's two clockwise offset faces.
    /// </summary>
    public int[] ClockwiseOffsetPent { get; private set; }

    private BaseCellData() { }

    /// <summary>
    /// Creates a <see cref="BaseCell"/> from an input set of parameters.
    /// </summary>
    /// <param name="tuple"></param>
    /// <returns></returns>
    public static implicit operator BaseCellData((int, (int, (int, int, int)), int, (int, int)) tuple) =>
        new BaseCellData {
            Cell = tuple.Item1,
            Home = new BaseCellHomeAddress {
                Face = tuple.Item2.Item1,
                I = tuple.Item2.Item2.Item1,
                J = tuple.Item2.Item2.Item2,
                K = tuple.Item2.Item2.Item3
            },
            IsPentagon = tuple.Item3 == 1,
            ClockwiseOffsetPent = new[] { tuple.Item4.Item1, tuple.Item4.Item2 }
        };

}