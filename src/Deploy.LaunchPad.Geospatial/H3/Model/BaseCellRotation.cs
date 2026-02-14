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

using System;
using static Deploy.LaunchPad.Geospatial.H3.H3Constants;

#nullable enable

namespace Deploy.LaunchPad.Geospatial.H3.Model; 

public sealed partial class BaseCellRotation {
    public int Cell { get; private init; }
    public int CounterClockwiseRotations { get; private init; }
    public BaseCell BaseCell { get; private init; } = null!;

    public const int InvalidRotations = -1;

    private BaseCellRotation() { }

    public static implicit operator BaseCellRotation((int, int) tuple) =>
        new() {
            Cell = tuple.Item1,
            CounterClockwiseRotations = tuple.Item2,
            BaseCell =  BaseCells.Cells[tuple.Item1]
        };

    public static int GetCounterClockwiseRotationsForBaseCell(int cell, int face) {
        if (face is < 0 or > NUM_ICOSA_FACES) return InvalidRotations;

        for (var i = 0; i < 3; i+= 1) {
            for (var j = 0; j < 3; j += 1) {
                for (var k = 0; k < 3; k += 1) {
                    var e = LookupTables.FaceIjkBaseCells[face, i, j, k];
                    if (e.Cell == cell) return e.CounterClockwiseRotations;
                }
            }
        }

        return InvalidRotations;
    }

    public override bool Equals(object? other) => other is BaseCellRotation r &&
                                                  Cell == r.Cell &&
                                                  CounterClockwiseRotations == r.CounterClockwiseRotations;

    public override int GetHashCode() => HashCode.Combine(Cell, CounterClockwiseRotations);
}