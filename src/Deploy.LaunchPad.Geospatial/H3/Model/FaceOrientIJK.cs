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

public sealed partial class FaceOrientIJK {
    public int Face { get; init; }
    public CoordIJK Translate { get; init; } = new();
    public int CounterClockwiseRotations { get; init; }

    private FaceOrientIJK() { }

    public FaceOrientIJK(int face, CoordIJK translate, int rotation) {
        Face = face;
        Translate = translate;
        CounterClockwiseRotations = rotation;
    }

    public static implicit operator FaceOrientIJK((int, (int, int, int), int) args) =>
        new(args.Item1, args.Item2, args.Item3);

    public static bool operator ==(FaceOrientIJK a, FaceOrientIJK b) =>
        a.Face == b.Face && a.Translate == b.Translate && a.CounterClockwiseRotations == b.CounterClockwiseRotations;

    public static bool operator !=(FaceOrientIJK a, FaceOrientIJK b) =>
        a.Face != b.Face || a.Translate != b.Translate || a.CounterClockwiseRotations != b.CounterClockwiseRotations;

    public override bool Equals(object? other) {
        return other is FaceOrientIJK f && Face == f.Face && Translate == f.Translate &&
               CounterClockwiseRotations == f.CounterClockwiseRotations;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Face, Translate, CounterClockwiseRotations);
    }
}