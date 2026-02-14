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
using System.Text;
using static Deploy.LaunchPad.Geospatial.H3.H3Constants;

namespace Deploy.LaunchPad.Geospatial.H3.Model
{
    internal static partial class BaseCells
    {
        /// <summary>
        /// Resolution-0 base cells (0..121).
        /// </summary>
        internal static readonly BaseCell[] Cells = Build();

        private static readonly sbyte[][] NeighbouringCellsByBaseCell = new sbyte[][]
        {
        new sbyte[] { (sbyte)0, (sbyte)1, (sbyte)5, (sbyte)2, (sbyte)4, (sbyte)3, (sbyte)8 }, // 0
        new sbyte[] { (sbyte)1, (sbyte)7, (sbyte)6, (sbyte)9, (sbyte)0, (sbyte)3, (sbyte)2 }, // 1
        new sbyte[] { (sbyte)2, (sbyte)6, (sbyte)10, (sbyte)11, (sbyte)0, (sbyte)1, (sbyte)5 }, // 2
        new sbyte[] { (sbyte)3, (sbyte)13, (sbyte)1, (sbyte)7, (sbyte)4, (sbyte)12, (sbyte)0 }, // 3
        new sbyte[] { (sbyte)4, (sbyte)127, (sbyte)15, (sbyte)8, (sbyte)3, (sbyte)0, (sbyte)12 }, // 4
        new sbyte[] { (sbyte)5, (sbyte)2, (sbyte)18, (sbyte)10, (sbyte)8, (sbyte)0, (sbyte)16 }, // 5
        new sbyte[] { (sbyte)6, (sbyte)14, (sbyte)11, (sbyte)17, (sbyte)1, (sbyte)9, (sbyte)2 }, // 6
        new sbyte[] { (sbyte)7, (sbyte)21, (sbyte)9, (sbyte)19, (sbyte)3, (sbyte)13, (sbyte)1 }, // 7
        new sbyte[] { (sbyte)8, (sbyte)5, (sbyte)22, (sbyte)16, (sbyte)4, (sbyte)0, (sbyte)15 }, // 8
        new sbyte[] { (sbyte)9, (sbyte)19, (sbyte)14, (sbyte)20, (sbyte)1, (sbyte)7, (sbyte)6 }, // 9
        new sbyte[] { (sbyte)10, (sbyte)11, (sbyte)24, (sbyte)23, (sbyte)5, (sbyte)2, (sbyte)18 }, // 10
        new sbyte[] { (sbyte)11, (sbyte)17, (sbyte)23, (sbyte)25, (sbyte)2, (sbyte)6, (sbyte)10 }, // 11
        new sbyte[] { (sbyte)12, (sbyte)28, (sbyte)13, (sbyte)26, (sbyte)4, (sbyte)15, (sbyte)3 }, // 12
        new sbyte[] { (sbyte)13, (sbyte)26, (sbyte)21, (sbyte)29, (sbyte)3, (sbyte)12, (sbyte)7 }, // 13
        new sbyte[] { (sbyte)14, (sbyte)127, (sbyte)17, (sbyte)27, (sbyte)9, (sbyte)20, (sbyte)6 }, // 14
        new sbyte[] { (sbyte)15, (sbyte)22, (sbyte)28, (sbyte)31, (sbyte)4, (sbyte)8, (sbyte)12 }, // 15
        new sbyte[] { (sbyte)16, (sbyte)18, (sbyte)33, (sbyte)30, (sbyte)8, (sbyte)5, (sbyte)22 }, // 16
        new sbyte[] { (sbyte)17, (sbyte)11, (sbyte)14, (sbyte)6, (sbyte)35, (sbyte)25, (sbyte)27 }, // 17
        new sbyte[] { (sbyte)18, (sbyte)24, (sbyte)30, (sbyte)32, (sbyte)5, (sbyte)10, (sbyte)16 }, // 18
        new sbyte[] { (sbyte)19, (sbyte)34, (sbyte)20, (sbyte)36, (sbyte)7, (sbyte)21, (sbyte)9 }, // 19
        new sbyte[] { (sbyte)20, (sbyte)14, (sbyte)19, (sbyte)9, (sbyte)40, (sbyte)27, (sbyte)36 }, // 20
        new sbyte[] { (sbyte)21, (sbyte)38, (sbyte)19, (sbyte)34, (sbyte)13, (sbyte)29, (sbyte)7 }, // 21
        new sbyte[] { (sbyte)22, (sbyte)16, (sbyte)41, (sbyte)33, (sbyte)15, (sbyte)8, (sbyte)31 }, // 22
        new sbyte[] { (sbyte)23, (sbyte)24, (sbyte)11, (sbyte)10, (sbyte)39, (sbyte)37, (sbyte)25 }, // 23
        new sbyte[] { (sbyte)24, (sbyte)127, (sbyte)32, (sbyte)37, (sbyte)10, (sbyte)23, (sbyte)18 }, // 24
        new sbyte[] { (sbyte)25, (sbyte)23, (sbyte)17, (sbyte)11, (sbyte)45, (sbyte)39, (sbyte)35 }, // 25
        new sbyte[] { (sbyte)26, (sbyte)42, (sbyte)29, (sbyte)43, (sbyte)12, (sbyte)28, (sbyte)13 }, // 26
        new sbyte[] { (sbyte)27, (sbyte)40, (sbyte)35, (sbyte)46, (sbyte)14, (sbyte)20, (sbyte)17 }, // 27
        new sbyte[] { (sbyte)28, (sbyte)31, (sbyte)42, (sbyte)44, (sbyte)12, (sbyte)15, (sbyte)26 }, // 28
        new sbyte[] { (sbyte)29, (sbyte)43, (sbyte)38, (sbyte)47, (sbyte)13, (sbyte)26, (sbyte)21 }, // 29
        new sbyte[] { (sbyte)30, (sbyte)32, (sbyte)48, (sbyte)50, (sbyte)16, (sbyte)18, (sbyte)33 }, // 30
        new sbyte[] { (sbyte)31, (sbyte)41, (sbyte)44, (sbyte)53, (sbyte)15, (sbyte)22, (sbyte)28 }, // 31
        new sbyte[] { (sbyte)32, (sbyte)30, (sbyte)24, (sbyte)18, (sbyte)52, (sbyte)50, (sbyte)37 }, // 32
        new sbyte[] { (sbyte)33, (sbyte)30, (sbyte)49, (sbyte)48, (sbyte)22, (sbyte)16, (sbyte)41 }, // 33
        new sbyte[] { (sbyte)34, (sbyte)19, (sbyte)38, (sbyte)21, (sbyte)54, (sbyte)36, (sbyte)51 }, // 34
        new sbyte[] { (sbyte)35, (sbyte)46, (sbyte)45, (sbyte)56, (sbyte)17, (sbyte)27, (sbyte)25 }, // 35
        new sbyte[] { (sbyte)36, (sbyte)20, (sbyte)34, (sbyte)19, (sbyte)55, (sbyte)40, (sbyte)54 }, // 36
        new sbyte[] { (sbyte)37, (sbyte)39, (sbyte)52, (sbyte)57, (sbyte)24, (sbyte)23, (sbyte)32 }, // 37
        new sbyte[] { (sbyte)38, (sbyte)127, (sbyte)34, (sbyte)51, (sbyte)29, (sbyte)47, (sbyte)21 }, // 38
        new sbyte[] { (sbyte)39, (sbyte)37, (sbyte)25, (sbyte)23, (sbyte)59, (sbyte)57, (sbyte)45 }, // 39
        new sbyte[] { (sbyte)40, (sbyte)27, (sbyte)36, (sbyte)20, (sbyte)60, (sbyte)46, (sbyte)55 }, // 40
        new sbyte[] { (sbyte)41, (sbyte)49, (sbyte)53, (sbyte)61, (sbyte)22, (sbyte)33, (sbyte)31 }, // 41
        new sbyte[] { (sbyte)42, (sbyte)58, (sbyte)43, (sbyte)62, (sbyte)28, (sbyte)44, (sbyte)26 }, // 42
        new sbyte[] { (sbyte)43, (sbyte)62, (sbyte)47, (sbyte)64, (sbyte)26, (sbyte)42, (sbyte)29 }, // 43
        new sbyte[] { (sbyte)44, (sbyte)53, (sbyte)58, (sbyte)65, (sbyte)28, (sbyte)31, (sbyte)42 }, // 44
        new sbyte[] { (sbyte)45, (sbyte)39, (sbyte)35, (sbyte)25, (sbyte)63, (sbyte)59, (sbyte)56 }, // 45
        new sbyte[] { (sbyte)46, (sbyte)60, (sbyte)56, (sbyte)68, (sbyte)27, (sbyte)40, (sbyte)35 }, // 46
        new sbyte[] { (sbyte)47, (sbyte)38, (sbyte)43, (sbyte)29, (sbyte)69, (sbyte)51, (sbyte)64 }, // 47
        new sbyte[] { (sbyte)48, (sbyte)49, (sbyte)30, (sbyte)33, (sbyte)67, (sbyte)66, (sbyte)50 }, // 48
        new sbyte[] { (sbyte)49, (sbyte)127, (sbyte)61, (sbyte)66, (sbyte)33, (sbyte)48, (sbyte)41 }, // 49
        new sbyte[] { (sbyte)50, (sbyte)48, (sbyte)32, (sbyte)30, (sbyte)70, (sbyte)67, (sbyte)52 }, // 50
        new sbyte[] { (sbyte)51, (sbyte)69, (sbyte)54, (sbyte)71, (sbyte)38, (sbyte)47, (sbyte)34 }, // 51
        new sbyte[] { (sbyte)52, (sbyte)57, (sbyte)70, (sbyte)74, (sbyte)32, (sbyte)37, (sbyte)50 }, // 52
        new sbyte[] { (sbyte)53, (sbyte)61, (sbyte)65, (sbyte)75, (sbyte)31, (sbyte)41, (sbyte)44 }, // 53
        new sbyte[] { (sbyte)54, (sbyte)71, (sbyte)55, (sbyte)73, (sbyte)34, (sbyte)51, (sbyte)36 }, // 54
        new sbyte[] { (sbyte)55, (sbyte)40, (sbyte)54, (sbyte)36, (sbyte)72, (sbyte)60, (sbyte)73 }, // 55
        new sbyte[] { (sbyte)56, (sbyte)68, (sbyte)63, (sbyte)77, (sbyte)35, (sbyte)46, (sbyte)45 }, // 56
        new sbyte[] { (sbyte)57, (sbyte)59, (sbyte)74, (sbyte)78, (sbyte)37, (sbyte)39, (sbyte)52 }, // 57
        new sbyte[] { (sbyte)58, (sbyte)127, (sbyte)62, (sbyte)76, (sbyte)44, (sbyte)65, (sbyte)42 }, // 58
        new sbyte[] { (sbyte)59, (sbyte)63, (sbyte)78, (sbyte)79, (sbyte)39, (sbyte)45, (sbyte)57 }, // 59
        new sbyte[] { (sbyte)60, (sbyte)72, (sbyte)68, (sbyte)80, (sbyte)40, (sbyte)55, (sbyte)46 }, // 60
        new sbyte[] { (sbyte)61, (sbyte)53, (sbyte)49, (sbyte)41, (sbyte)81, (sbyte)75, (sbyte)66 }, // 61
        new sbyte[] { (sbyte)62, (sbyte)43, (sbyte)58, (sbyte)42, (sbyte)82, (sbyte)64, (sbyte)76 }, // 62
        new sbyte[] { (sbyte)63, (sbyte)127, (sbyte)56, (sbyte)45, (sbyte)79, (sbyte)59, (sbyte)77 }, // 63
        new sbyte[] { (sbyte)64, (sbyte)47, (sbyte)62, (sbyte)43, (sbyte)84, (sbyte)69, (sbyte)82 }, // 64
        new sbyte[] { (sbyte)65, (sbyte)58, (sbyte)53, (sbyte)44, (sbyte)86, (sbyte)76, (sbyte)75 }, // 65
        new sbyte[] { (sbyte)66, (sbyte)67, (sbyte)81, (sbyte)85, (sbyte)49, (sbyte)48, (sbyte)61 }, // 66
        new sbyte[] { (sbyte)67, (sbyte)66, (sbyte)50, (sbyte)48, (sbyte)87, (sbyte)85, (sbyte)70 }, // 67
        new sbyte[] { (sbyte)68, (sbyte)56, (sbyte)60, (sbyte)46, (sbyte)90, (sbyte)77, (sbyte)80 }, // 68
        new sbyte[] { (sbyte)69, (sbyte)51, (sbyte)64, (sbyte)47, (sbyte)89, (sbyte)71, (sbyte)84 }, // 69
        new sbyte[] { (sbyte)70, (sbyte)67, (sbyte)52, (sbyte)50, (sbyte)83, (sbyte)87, (sbyte)74 }, // 70
        new sbyte[] { (sbyte)71, (sbyte)89, (sbyte)73, (sbyte)91, (sbyte)51, (sbyte)69, (sbyte)54 }, // 71
        new sbyte[] { (sbyte)72, (sbyte)127, (sbyte)73, (sbyte)55, (sbyte)80, (sbyte)60, (sbyte)88 }, // 72
        new sbyte[] { (sbyte)73, (sbyte)91, (sbyte)72, (sbyte)88, (sbyte)54, (sbyte)71, (sbyte)55 }, // 73
        new sbyte[] { (sbyte)74, (sbyte)78, (sbyte)83, (sbyte)92, (sbyte)52, (sbyte)57, (sbyte)70 }, // 74
        new sbyte[] { (sbyte)75, (sbyte)65, (sbyte)61, (sbyte)53, (sbyte)94, (sbyte)86, (sbyte)81 }, // 75
        new sbyte[] { (sbyte)76, (sbyte)86, (sbyte)82, (sbyte)96, (sbyte)58, (sbyte)65, (sbyte)62 }, // 76
        new sbyte[] { (sbyte)77, (sbyte)63, (sbyte)68, (sbyte)56, (sbyte)93, (sbyte)79, (sbyte)90 }, // 77
        new sbyte[] { (sbyte)78, (sbyte)74, (sbyte)59, (sbyte)57, (sbyte)95, (sbyte)92, (sbyte)79 }, // 78
        new sbyte[] { (sbyte)79, (sbyte)78, (sbyte)63, (sbyte)59, (sbyte)93, (sbyte)95, (sbyte)77 }, // 79
        new sbyte[] { (sbyte)80, (sbyte)68, (sbyte)72, (sbyte)60, (sbyte)99, (sbyte)90, (sbyte)88 }, // 80
        new sbyte[] { (sbyte)81, (sbyte)85, (sbyte)94, (sbyte)101, (sbyte)61, (sbyte)66, (sbyte)75 }, // 81
        new sbyte[] { (sbyte)82, (sbyte)96, (sbyte)84, (sbyte)98, (sbyte)62, (sbyte)76, (sbyte)64 }, // 82
        new sbyte[] { (sbyte)83, (sbyte)127, (sbyte)74, (sbyte)70, (sbyte)100, (sbyte)87, (sbyte)92 }, // 83
        new sbyte[] { (sbyte)84, (sbyte)69, (sbyte)82, (sbyte)64, (sbyte)97, (sbyte)89, (sbyte)98 }, // 84
        new sbyte[] { (sbyte)85, (sbyte)87, (sbyte)101, (sbyte)102, (sbyte)66, (sbyte)67, (sbyte)81 }, // 85
        new sbyte[] { (sbyte)86, (sbyte)76, (sbyte)75, (sbyte)65, (sbyte)104, (sbyte)96, (sbyte)94 }, // 86
        new sbyte[] { (sbyte)87, (sbyte)83, (sbyte)102, (sbyte)100, (sbyte)67, (sbyte)70, (sbyte)85 }, // 87
        new sbyte[] { (sbyte)88, (sbyte)72, (sbyte)91, (sbyte)73, (sbyte)99, (sbyte)80, (sbyte)105 }, // 88
        new sbyte[] { (sbyte)89, (sbyte)97, (sbyte)91, (sbyte)103, (sbyte)69, (sbyte)84, (sbyte)71 }, // 89
        new sbyte[] { (sbyte)90, (sbyte)77, (sbyte)80, (sbyte)68, (sbyte)106, (sbyte)93, (sbyte)99 }, // 90
        new sbyte[] { (sbyte)91, (sbyte)73, (sbyte)89, (sbyte)71, (sbyte)105, (sbyte)88, (sbyte)103 }, // 91
        new sbyte[] { (sbyte)92, (sbyte)83, (sbyte)78, (sbyte)74, (sbyte)108, (sbyte)100, (sbyte)95 }, // 92
        new sbyte[] { (sbyte)93, (sbyte)79, (sbyte)90, (sbyte)77, (sbyte)109, (sbyte)95, (sbyte)106 }, // 93
        new sbyte[] { (sbyte)94, (sbyte)86, (sbyte)81, (sbyte)75, (sbyte)107, (sbyte)104, (sbyte)101 }, // 94
        new sbyte[] { (sbyte)95, (sbyte)92, (sbyte)79, (sbyte)78, (sbyte)109, (sbyte)108, (sbyte)93 }, // 95
        new sbyte[] { (sbyte)96, (sbyte)104, (sbyte)98, (sbyte)110, (sbyte)76, (sbyte)86, (sbyte)82 }, // 96
        new sbyte[] { (sbyte)97, (sbyte)127, (sbyte)98, (sbyte)84, (sbyte)103, (sbyte)89, (sbyte)111 }, // 97
        new sbyte[] { (sbyte)98, (sbyte)110, (sbyte)97, (sbyte)111, (sbyte)82, (sbyte)96, (sbyte)84 }, // 98
        new sbyte[] { (sbyte)99, (sbyte)80, (sbyte)105, (sbyte)88, (sbyte)106, (sbyte)90, (sbyte)113 }, // 99
        new sbyte[] { (sbyte)100, (sbyte)102, (sbyte)83, (sbyte)87, (sbyte)108, (sbyte)114, (sbyte)92 }, // 100
        new sbyte[] { (sbyte)101, (sbyte)102, (sbyte)107, (sbyte)112, (sbyte)81, (sbyte)85, (sbyte)94 }, // 101
        new sbyte[] { (sbyte)102, (sbyte)101, (sbyte)87, (sbyte)85, (sbyte)114, (sbyte)112, (sbyte)100 }, // 102
        new sbyte[] { (sbyte)103, (sbyte)91, (sbyte)97, (sbyte)89, (sbyte)116, (sbyte)105, (sbyte)111 }, // 103
        new sbyte[] { (sbyte)104, (sbyte)107, (sbyte)110, (sbyte)115, (sbyte)86, (sbyte)94, (sbyte)96 }, // 104
        new sbyte[] { (sbyte)105, (sbyte)88, (sbyte)103, (sbyte)91, (sbyte)113, (sbyte)99, (sbyte)116 }, // 105
        new sbyte[] { (sbyte)106, (sbyte)93, (sbyte)99, (sbyte)90, (sbyte)117, (sbyte)109, (sbyte)113 }, // 106
        new sbyte[] { (sbyte)107, (sbyte)127, (sbyte)101, (sbyte)94, (sbyte)115, (sbyte)104, (sbyte)112 }, // 107
        new sbyte[] { (sbyte)108, (sbyte)100, (sbyte)95, (sbyte)92, (sbyte)118, (sbyte)114, (sbyte)109 }, // 108
        new sbyte[] { (sbyte)109, (sbyte)108, (sbyte)93, (sbyte)95, (sbyte)117, (sbyte)118, (sbyte)106 }, // 109
        new sbyte[] { (sbyte)110, (sbyte)98, (sbyte)104, (sbyte)96, (sbyte)119, (sbyte)111, (sbyte)115 }, // 110
        new sbyte[] { (sbyte)111, (sbyte)97, (sbyte)110, (sbyte)98, (sbyte)116, (sbyte)103, (sbyte)119 }, // 111
        new sbyte[] { (sbyte)112, (sbyte)107, (sbyte)102, (sbyte)101, (sbyte)120, (sbyte)115, (sbyte)114 }, // 112
        new sbyte[] { (sbyte)113, (sbyte)99, (sbyte)116, (sbyte)105, (sbyte)117, (sbyte)106, (sbyte)121 }, // 113
        new sbyte[] { (sbyte)114, (sbyte)112, (sbyte)100, (sbyte)102, (sbyte)118, (sbyte)120, (sbyte)108 }, // 114
        new sbyte[] { (sbyte)115, (sbyte)110, (sbyte)107, (sbyte)104, (sbyte)120, (sbyte)119, (sbyte)112 }, // 115
        new sbyte[] { (sbyte)116, (sbyte)103, (sbyte)119, (sbyte)111, (sbyte)113, (sbyte)105, (sbyte)121 }, // 116
        new sbyte[] { (sbyte)117, (sbyte)127, (sbyte)109, (sbyte)118, (sbyte)113, (sbyte)121, (sbyte)106 }, // 117
        new sbyte[] { (sbyte)118, (sbyte)120, (sbyte)108, (sbyte)114, (sbyte)117, (sbyte)121, (sbyte)109 }, // 118
        new sbyte[] { (sbyte)119, (sbyte)111, (sbyte)115, (sbyte)110, (sbyte)121, (sbyte)116, (sbyte)120 }, // 119
        new sbyte[] { (sbyte)120, (sbyte)115, (sbyte)114, (sbyte)112, (sbyte)121, (sbyte)119, (sbyte)118 }, // 120
        new sbyte[] { (sbyte)121, (sbyte)116, (sbyte)120, (sbyte)119, (sbyte)117, (sbyte)113, (sbyte)118 }, // 121
        };

        private static readonly sbyte[][] NeighbourRotationsByBaseCell = new sbyte[][]
        {
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)5, (sbyte)1 }, // 0
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1 }, // 1
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)0 }, // 2
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)2, (sbyte)5, (sbyte)1 }, // 3
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)1, (sbyte)0, (sbyte)3, (sbyte)4, (sbyte)2 }, // 4
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1 }, // 5
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)5, (sbyte)0 }, // 6
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)0 }, // 7
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)1 }, // 8
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)1 }, // 9
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)1 }, // 10
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 11
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)1 }, // 12
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1 }, // 13
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 14
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)4, (sbyte)5, (sbyte)1 }, // 15
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)0 }, // 16
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3 }, // 17
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)5, (sbyte)0 }, // 18
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 19
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 20
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)5, (sbyte)0 }, // 21
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1 }, // 22
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 23
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 24
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 25
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)0 }, // 26
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 27
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1 }, // 28
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)1 }, // 29
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 30
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)0 }, // 31
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3 }, // 32
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)1 }, // 33
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3 }, // 34
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3 }, // 35
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 36
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 37
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 38
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)0 }, // 39
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)0 }, // 40
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)5, (sbyte)0 }, // 41
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)5, (sbyte)0 }, // 42
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 43
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)1 }, // 44
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 45
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 46
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 47
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 48
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 49
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 50
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 51
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3 }, // 52
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 53
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3 }, // 54
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 55
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 56
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 57
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 58
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0 }, // 59
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0 }, // 60
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3 }, // 61
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3 }, // 62
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 63
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 64
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 65
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 66
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)0 }, // 67
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 68
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)0 }, // 69
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 70
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 71
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 72
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 73
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 74
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 75
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 76
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)0 }, // 77
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 78
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)1, (sbyte)0, (sbyte)1 }, // 79
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)1, (sbyte)0, (sbyte)1 }, // 80
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3 }, // 81
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3 }, // 82
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 83
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 84
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 85
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)0 }, // 86
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0 }, // 87
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)0 }, // 88
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0 }, // 89
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)1 }, // 90
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 91
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)0 }, // 92
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)5, (sbyte)0 }, // 93
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)3 }, // 94
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)1 }, // 95
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)3, (sbyte)0 }, // 96
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 97
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 98
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)5, (sbyte)0 }, // 99
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)1, (sbyte)0, (sbyte)1 }, // 100
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)3 }, // 101
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 102
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)1, (sbyte)0, (sbyte)1 }, // 103
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0 }, // 104
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)1 }, // 105
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)3, (sbyte)5, (sbyte)1 }, // 106
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)2, (sbyte)0 }, // 107
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)5, (sbyte)0 }, // 108
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)4, (sbyte)5, (sbyte)1 }, // 109
        new sbyte[] { (sbyte)0, (sbyte)3, (sbyte)3, (sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0 }, // 110
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)0 }, // 111
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)3, (sbyte)0, (sbyte)5, (sbyte)0 }, // 112
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)2, (sbyte)5, (sbyte)1 }, // 113
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)1 }, // 114
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)3, (sbyte)1, (sbyte)0, (sbyte)1 }, // 115
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)5, (sbyte)0 }, // 116
        new sbyte[] { (sbyte)0, (sbyte)-1, (sbyte)1, (sbyte)0, (sbyte)3, (sbyte)4, (sbyte)2 }, // 117
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)1 }, // 118
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, (sbyte)1 }, // 119
        new sbyte[] { (sbyte)0, (sbyte)5, (sbyte)0, (sbyte)0, (sbyte)5, (sbyte)5, (sbyte)0 }, // 120
        new sbyte[] { (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, (sbyte)1, (sbyte)5, (sbyte)1 }, // 121
        };

        private static readonly (sbyte face, sbyte i, sbyte j, sbyte k, bool isPentagon, sbyte off0, sbyte off1)[] BaseCellData =
            new (sbyte face, sbyte i, sbyte j, sbyte k, bool isPentagon, sbyte off0, sbyte off1)[]
        {
        ((sbyte)1, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 0
        ((sbyte)2, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 1
        ((sbyte)1, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 2
        ((sbyte)2, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 3
        ((sbyte)0, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)-1, (sbyte)-1), // 4
        ((sbyte)1, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 5
        ((sbyte)1, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 6
        ((sbyte)2, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 7
        ((sbyte)0, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 8
        ((sbyte)2, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 9
        ((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 10
        ((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 11
        ((sbyte)2, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 12
        ((sbyte)3, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 13
        ((sbyte)11, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)2, (sbyte)6), // 14
        ((sbyte)0, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 15
        ((sbyte)0, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 16
        ((sbyte)3, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 17
        ((sbyte)4, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 18
        ((sbyte)3, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 19
        ((sbyte)6, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 20
        ((sbyte)4, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 21
        ((sbyte)0, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 22
        ((sbyte)6, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 23
        ((sbyte)10, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)1, (sbyte)5), // 24
        ((sbyte)6, (sbyte)1, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 25
        ((sbyte)5, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 26
        ((sbyte)5, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 27
        ((sbyte)8, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 28
        ((sbyte)7, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 29
        ((sbyte)4, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 30
        ((sbyte)0, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 31
        ((sbyte)7, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 32
        ((sbyte)8, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 33
        ((sbyte)5, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 34
        ((sbyte)8, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 35
        ((sbyte)6, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 36
        ((sbyte)10, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 37
        ((sbyte)12, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)3, (sbyte)7), // 38
        ((sbyte)7, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 39
        ((sbyte)11, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 40
        ((sbyte)10, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 41
        ((sbyte)9, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 42
        ((sbyte)13, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 43
        ((sbyte)8, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 44
        ((sbyte)12, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 45
        ((sbyte)11, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 46
        ((sbyte)11, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 47
        ((sbyte)10, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 48
        ((sbyte)14, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)0, (sbyte)9), // 49
        ((sbyte)9, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 50
        ((sbyte)12, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 51
        ((sbyte)10, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 52
        ((sbyte)8, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 53
        ((sbyte)7, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 54
        ((sbyte)12, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 55
        ((sbyte)17, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 56
        ((sbyte)13, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 57
        ((sbyte)13, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)4, (sbyte)8), // 58
        ((sbyte)15, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 59
        ((sbyte)15, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 60
        ((sbyte)9, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 61
        ((sbyte)16, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 62
        ((sbyte)6, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)11, (sbyte)15), // 63
        ((sbyte)14, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 64
        ((sbyte)16, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 65
        ((sbyte)16, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 66
        ((sbyte)18, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 67
        ((sbyte)13, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 68
        ((sbyte)14, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 69
        ((sbyte)15, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 70
        ((sbyte)17, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 71
        ((sbyte)7, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)12, (sbyte)16), // 72
        ((sbyte)12, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 73
        ((sbyte)11, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 74
        ((sbyte)17, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 75
        ((sbyte)13, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 76
        ((sbyte)19, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 77
        ((sbyte)14, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 78
        ((sbyte)9, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 79
        ((sbyte)16, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 80
        ((sbyte)18, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 81
        ((sbyte)16, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 82
        ((sbyte)5, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)10, (sbyte)19), // 83
        ((sbyte)18, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 84
        ((sbyte)14, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 85
        ((sbyte)15, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 86
        ((sbyte)17, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 87
        ((sbyte)19, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 88
        ((sbyte)13, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 89
        ((sbyte)19, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 90
        ((sbyte)17, (sbyte)0, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 91
        ((sbyte)15, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 92
        ((sbyte)11, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 93
        ((sbyte)8, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 94
        ((sbyte)19, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 95
        ((sbyte)16, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 96
        ((sbyte)8, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)13, (sbyte)17), // 97
        ((sbyte)19, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 98
        ((sbyte)6, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 99
        ((sbyte)5, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 100
        ((sbyte)6, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 101
        ((sbyte)7, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 102
        ((sbyte)9, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 103
        ((sbyte)18, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 104
        ((sbyte)10, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 105
        ((sbyte)11, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 106
        ((sbyte)9, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)14, (sbyte)18), // 107
        ((sbyte)10, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 108
        ((sbyte)11, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 109
        ((sbyte)13, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 110
        ((sbyte)12, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 111
        ((sbyte)16, (sbyte)1, (sbyte)1, (sbyte)0, false, (sbyte)0, (sbyte)0), // 112
        ((sbyte)17, (sbyte)1, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 113
        ((sbyte)9, (sbyte)0, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 114
        ((sbyte)14, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 115
        ((sbyte)18, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 116
        ((sbyte)19, (sbyte)2, (sbyte)0, (sbyte)0, true, (sbyte)-1, (sbyte)-1), // 117
        ((sbyte)13, (sbyte)0, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 118
        ((sbyte)15, (sbyte)1, (sbyte)1, (sbyte)1, false, (sbyte)0, (sbyte)0), // 119
        ((sbyte)15, (sbyte)0, (sbyte)0, (sbyte)1, false, (sbyte)0, (sbyte)0), // 120
        ((sbyte)18, (sbyte)1, (sbyte)0, (sbyte)0, false, (sbyte)0, (sbyte)0), // 121
        };

        private static BaseCell[] Build()
        {
            var cells = new BaseCell[NUM_BASE_CELLS];

            for (var cell = 0; cell < NUM_BASE_CELLS; cell++)
            {
                var data = BaseCellData[cell];

                var neighbours = NeighbouringCellsByBaseCell[cell];
                var rotations = NeighbourRotationsByBaseCell[cell];

                var dict = new Dictionary<sbyte, Direction>(6);
                for (var d = 0; d < 7; d++)
                {
                    var n = neighbours[d];
                    if (n == (sbyte)LookupTables.INVALID_BASE_CELL) continue;
                    dict[n] = (Direction)d;
                }

                cells[cell] = new BaseCell
                {
                    Cell = (sbyte)cell,
                    Home = new FaceIJK(data.face, new CoordIJK(data.i, data.j, data.k)),
                    IsPentagon = data.isPentagon,
                    IsPolarPentagon = cell == 4 || cell == 117, // upstream _isBaseCellPolarPentagon()
                    ClockwiseOffsetPent = new[] { data.off0, data.off1 },
                    NeighbouringCells = neighbours,
                    NeighbourRotations = rotations,
                    NeighbourDirections = dict
                };
            }

            return cells;
        }
    }
}
