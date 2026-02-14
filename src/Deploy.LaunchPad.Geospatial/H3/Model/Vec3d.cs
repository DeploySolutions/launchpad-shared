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
using System.Runtime.CompilerServices;
using NetTopologySuite.Geometries;
using static Deploy.LaunchPad.Geospatial.H3.H3Constants;
using static Deploy.LaunchPad.Geospatial.H3.H3Utils;

#nullable enable

namespace Deploy.LaunchPad.Geospatial.H3.Model; 

public sealed partial class Vec3d {

    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vec3d() { }

    public Vec3d(double x, double y, double z) {
        X = x;
        Y = y;
        Z = z;
    }

    public Vec3d(Vec3d source) {
        X = source.X;
        Y = source.Y;
        Z = source.Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double PointSquareDistance(Vec3d v2) =>
        Square(X - v2.X) + Square(Y - v2.Y) + Square(Z - v2.Z);

    public static Vec3d FromGeoCoord(LatLng coord, Vec3d? result = default) {
        return FromLonLat(coord.Longitude, coord.Latitude, result);
    }

    public static Vec3d FromLonLat(double longitudeRadians, double latitudeRadians, Vec3d? result = default) {
        unchecked {
            var ret = result ?? new Vec3d();
            var r = Math.Cos(latitudeRadians);
            ret.X = Math.Cos(longitudeRadians) * r;
            ret.Y = Math.Sin(longitudeRadians) * r;
            ret.Z = Math.Sin(latitudeRadians);
            return ret;
        }
    }

    public static Vec3d FromPoint(Point point) => FromGeoCoord(LatLng.FromPoint(point));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vec3d a, Vec3d b) => Math.Abs(a.X - b.X) < EPSILON && Math.Abs(a.Y - b.Y) < EPSILON && Math.Abs(a.Z - b.Z) < EPSILON;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vec3d a, Vec3d b) => Math.Abs(a.X - b.X) >= EPSILON || Math.Abs(a.Y - b.Y) >= EPSILON || Math.Abs(a.Z - b.Z) >= EPSILON;

    public override bool Equals(object? other) => other is Vec3d v && this == v;

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

}