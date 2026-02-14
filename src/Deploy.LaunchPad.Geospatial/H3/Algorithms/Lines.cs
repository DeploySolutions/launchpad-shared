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
using System.Collections.Generic;

using Deploy.LaunchPad.Geospatial.H3.Extensions;
using Deploy.LaunchPad.Geospatial.H3.Model;

namespace Deploy.LaunchPad.Geospatial.H3.Algorithms;

public static partial class Lines {

    /// <summary>
    /// Produces the grid distance between the two indexes.
    ///
    /// This function may fail to find the distance between two indexes, for
    /// example if they are very far apart. It may also fail when finding
    /// distances for indexes on opposite sides of a pentagon.
    /// </summary>
    /// <param name="origin">index to find distance from</param>
    /// <param name="destination">index to find distance to</param>
    /// <returns>grid distance in cells; -1 if could not be computed</returns>
    [Obsolete("as of 4.0: use GridDistance instead")]
    public static int DistanceTo(this H3Index origin, H3Index destination) {
        return origin.GridDistance(destination);
    }

    /// <summary>
    /// Produces the grid distance between the two indexes.
    ///
    /// This function may fail to find the distance between two indexes, for
    /// example if they are very far apart. It may also fail when finding
    /// distances for indexes on opposite sides of a pentagon.
    /// </summary>
    /// <param name="origin">index to find distance from</param>
    /// <param name="destination">index to find distance to</param>
    /// <returns>grid distance in cells; -1 if could not be computed</returns>
    public static int GridDistance(this H3Index origin, H3Index destination) {
        try {
            var originIjk = LocalCoordIJK.ToLocalIJK(origin, origin);
            var destinationIjk = LocalCoordIJK.ToLocalIJK(origin, destination);

            return originIjk.GetDistanceTo(destinationIjk);
        } catch {
            return -1;
        }
    }

    /// <summary>
    /// Given two H3 cells, return the path of cells between them (inclusive).
    /// </summary>
    /// <remarks>
    /// This function may fail to find the line between two cells, for
    /// example if they are very far apart. It may also fail when finding
    /// distances for indexes on opposite sides of a pentagon.
    /// - The specific output of this function should not be considered stable
    ///   across library versions. The only guarantees the library provides are
    ///   that the line length will be `GridDistance(start, end) + 1` and that
    ///   every index in the line will be a neighbor of the preceding index.
    /// - Lines are drawn in grid space, and may not correspond exactly to either
    ///   Cartesian lines or great arcs.
    /// </remarks>
    /// <param name="origin">start index of the line</param>
    /// <param name="destination">end index of the line</param>
    /// <returns>all points from start to end, inclusive; empty if could not
    /// compute a line</returns>
    [Obsolete("as of 4.0: use GridPathCells instead")]
    public static IEnumerable<H3Index> LineTo(this H3Index origin, H3Index destination) {
        return origin.GridPathCells(destination);
    }

    /// <summary>
    /// Given two H3 cells, return the path of cells between them (inclusive).
    /// </summary>
    /// <remarks>
    /// This function may fail to find the line between two cells, for
    /// example if they are very far apart. It may also fail when finding
    /// distances for indexes on opposite sides of a pentagon.
    /// - The specific output of this function should not be considered stable
    ///   across library versions. The only guarantees the library provides are
    ///   that the line length will be `GridDistance(start, end) + 1` and that
    ///   every index in the line will be a neighbor of the preceding index.
    /// - Lines are drawn in grid space, and may not correspond exactly to either
    ///   Cartesian lines or great arcs.
    /// </remarks>
    /// <param name="origin">start index of the line</param>
    /// <param name="destination">end index of the line</param>
    /// <returns>all points from start to end, inclusive; empty if could not
    /// compute a line</returns>
    public static IEnumerable<H3Index> GridPathCells(this H3Index origin, H3Index destination) {
        CoordIJK startIjk;
        CoordIJK endIjk;
        var workIjk1 = new CoordIJK();
        var workIjk2 = new CoordIJK();

        // translate to local coordinates
        try {
            startIjk = LocalCoordIJK.ToLocalIJK(origin, origin);
            endIjk = LocalCoordIJK.ToLocalIJK(origin, destination);
        } catch {
            yield break;
        }

        // get grid distance between start/end
        var distance = startIjk.GetDistanceTo(endIjk);

        // Convert IJK to cube coordinates suitable for linear interpolation
        startIjk.Cube();
        endIjk.Cube();

        double d = distance;
        var iStep = distance > 0 ? (endIjk.I - startIjk.I) / d : 0.0;
        var jStep = distance > 0 ? (endIjk.J - startIjk.J) / d : 0.0;
        var kStep = distance > 0 ? (endIjk.K - startIjk.K) / d : 0.0;

        double startI = startIjk.I;
        double startJ = startIjk.J;
        double startK = startIjk.K;

        for (var n = 0; n < distance + 1; n += 1) {
            CoordIJK.CubeRound(
                startI + iStep * n,
                startJ + jStep * n,
                startK + kStep * n,
                endIjk
            ).Uncube();
            yield return LocalCoordIJK.ToH3Index(origin, endIjk, startIjk, workIjk1, workIjk2);
        }
    }

}