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

#nullable enable

namespace Deploy.LaunchPad.Geospatial.H3.Model; 

public sealed partial class CoordIJ {

    public int I { get; set; }
    public int J { get; set; }


    public CoordIJ(int i, int j) {
        I = i;
        J = j;
    }

    public CoordIJ(CoordIJ source) {
        I = source.I;
        J = source.J;
    }

    public static CoordIJ FromCoordIJK(CoordIJK ijk) => new(ijk.I - ijk.K, ijk.J - ijk.K);

    public static implicit operator CoordIJ((int, int) coord) =>
        new(coord.Item1, coord.Item2);

    public CoordIJK ToCoordIJK() => new CoordIJK(I, J, 0).Normalize();

    public static bool operator ==(CoordIJ a, CoordIJ b) => a.I == b.I && a.J == b.J;

    public static bool operator !=(CoordIJ a, CoordIJ b) => a.I != b.I || a.J != b.J;

    public override bool Equals(object? other) => other is CoordIJ c && I == c.I && J == c.J;
    public override string ToString() {
        return $"({I}, {J})";
    }

    public override int GetHashCode() => HashCode.Combine(I, J);
}