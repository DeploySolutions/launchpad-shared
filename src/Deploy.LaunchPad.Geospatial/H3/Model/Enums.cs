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

#nullable enable

using System.Runtime.CompilerServices;

namespace Deploy.LaunchPad.Geospatial.H3.Model; 

public enum Direction {
    Center = 0,
    K = 1,
    J = 2,
    JK = 3,
    I = 4,
    IK = 5,
    IJ = 6,
    Invalid = 7
}

public static partial class DirectionExtensions {
    /// <summary>
    /// Clockwise rotation steps, by <see cref="Direction"/> and number of rotations from 0-5.
    /// </summary>
    private static readonly Direction[,] Clockwise = {
        { Direction.Center, Direction.Center, Direction.Center, Direction.Center, Direction.Center, Direction.Center },
        { Direction.K, Direction.JK, Direction.J, Direction.IJ, Direction.I, Direction.IK },
        { Direction.J, Direction.IJ, Direction.I, Direction.IK, Direction.K, Direction.JK },
        { Direction.JK, Direction.J, Direction.IJ, Direction.I, Direction.IK, Direction.K },
        { Direction.I, Direction.IK, Direction.K, Direction.JK, Direction.J, Direction.IJ },
        { Direction.IK, Direction.K, Direction.JK, Direction.J, Direction.IJ, Direction.I },
        { Direction.IJ, Direction.I, Direction.IK, Direction.K, Direction.JK, Direction.J },
        { Direction.Invalid, Direction.Invalid, Direction.Invalid, Direction.Invalid, Direction.Invalid, Direction.Invalid }
    };

    /// <summary>
    /// Counter-clockwise rotation steps, by <see cref="Direction"/> and number of rotations from 0-5.
    /// </summary>
    private static readonly Direction[,] CounterClockwise = {
        { Direction.Center, Direction.Center, Direction.Center, Direction.Center, Direction.Center, Direction.Center },
        { Direction.K, Direction.IK, Direction.I, Direction.IJ, Direction.J, Direction.JK },
        { Direction.J , Direction.JK, Direction.K, Direction.IK, Direction.I, Direction.IJ },
        { Direction.JK, Direction.K, Direction.IK, Direction.I, Direction.IJ, Direction.J },
        { Direction.I, Direction.IJ, Direction.J, Direction.JK, Direction.K, Direction.IK },
        { Direction.IK, Direction.I, Direction.IJ, Direction.J, Direction.JK, Direction.K },
        { Direction.IJ, Direction.J, Direction.JK, Direction.K, Direction.IK, Direction.I },
        { Direction.Invalid, Direction.Invalid, Direction.Invalid, Direction.Invalid, Direction.Invalid, Direction.Invalid }
    };

    /// <summary>
    /// Returns the <see cref="Direction"/> that is 60 degrees clockwise to the current
    /// direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction RotateClockwise(this Direction direction) {
        return Clockwise[(int)direction, 1];
    }

    /// <summary>
    /// Returns the <see cref="Direction"/> that is 60 degrees clockwise to the current
    /// direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="rotations">number of rotations to perform</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction RotateClockwise(this Direction direction, int rotations) {
        return Clockwise[(int)direction, rotations % 6];
    }

    /// <summary>
    /// Returns the <see cref="Direction"/> that is 60 degrees counter-clockwise to the current
    /// direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction RotateCounterClockwise(this Direction direction) {
        return CounterClockwise[(int)direction, 1];
    }

    /// <summary>
    /// Returns the <see cref="Direction"/> that is 60 degrees counter-clockwise to the current
    /// direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="rotations">number of rotations to perform</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction RotateCounterClockwise(this Direction direction, int rotations) {
        return CounterClockwise[(int)direction, rotations % 6];
    }
}

public enum Mode {
    Unknown = 0,
    Cell = 1,
    UniEdge = 2,
    Vertex = 4
}

public enum Overage {
    None = 0,
    FaceEdge = 1,
    NewFace = 2
}