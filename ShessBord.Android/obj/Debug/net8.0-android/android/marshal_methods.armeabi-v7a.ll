; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [267 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [528 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 69
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 68
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 109
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 241
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 254
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 49
	i32 39485524, ; 6: System.Net.WebSockets.dll => 0x25a8054 => 81
	i32 42639949, ; 7: System.Threading.Thread => 0x28aa24d => 146
	i32 66541672, ; 8: System.Diagnostics.StackTrace => 0x3f75868 => 31
	i32 68219467, ; 9: System.Security.Cryptography.Primitives => 0x410f24b => 125
	i32 82292897, ; 10: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 103
	i32 98325684, ; 11: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 204
	i32 117431740, ; 12: System.Runtime.InteropServices => 0x6ffddbc => 108
	i32 122350210, ; 13: System.Threading.Channels.dll => 0x74aea82 => 140
	i32 134690465, ; 14: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 258
	i32 142721839, ; 15: System.Net.WebHeaderCollection => 0x881c32f => 78
	i32 149972175, ; 16: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 125
	i32 159306688, ; 17: System.ComponentModel.Annotations => 0x97ed3c0 => 15
	i32 165246403, ; 18: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 225
	i32 176265551, ; 19: System.ServiceProcess => 0xa81994f => 133
	i32 184328833, ; 20: System.ValueTuple.dll => 0xafca281 => 152
	i32 205061960, ; 21: System.ComponentModel => 0xc38ff48 => 20
	i32 220171995, ; 22: System.Diagnostics.Debug => 0xd1f8edb => 28
	i32 221958352, ; 23: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 203
	i32 230752869, ; 24: Microsoft.CSharp.dll => 0xdc10265 => 3
	i32 231409092, ; 25: System.Linq.Parallel => 0xdcb05c4 => 60
	i32 231814094, ; 26: System.Globalization => 0xdd133ce => 43
	i32 246610117, ; 27: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 92
	i32 256616158, ; 28: Avalonia.Themes.Fluent => 0xf4ba6de => 193
	i32 276479776, ; 29: System.Threading.Timer.dll => 0x107abf20 => 148
	i32 280482487, ; 30: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 237
	i32 291076382, ; 31: System.IO.Pipes.AccessControl.dll => 0x1159791e => 55
	i32 291275502, ; 32: Microsoft.Extensions.Http.dll => 0x115c82ee => 205
	i32 298918909, ; 33: System.Net.Ping.dll => 0x11d123fd => 70
	i32 318968648, ; 34: Xamarin.AndroidX.Activity.dll => 0x13031348 => 217
	i32 321597661, ; 35: System.Numerics => 0x132b30dd => 84
	i32 342366114, ; 36: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 238
	i32 360082299, ; 37: System.ServiceModel.Web => 0x15766b7b => 132
	i32 367780167, ; 38: System.IO.Pipes => 0x15ebe147 => 56
	i32 374914964, ; 39: System.Transactions.Local => 0x1658bf94 => 150
	i32 375677976, ; 40: System.Net.ServicePoint.dll => 0x16646418 => 75
	i32 379916513, ; 41: System.Threading.Thread.dll => 0x16a510e1 => 146
	i32 385762202, ; 42: System.Memory.dll => 0x16fe439a => 63
	i32 392610295, ; 43: System.Threading.ThreadPool.dll => 0x1766c1f7 => 147
	i32 395744057, ; 44: _Microsoft.Android.Resource.Designer => 0x17969339 => 263
	i32 403441872, ; 45: WindowsBase => 0x180c08d0 => 166
	i32 442565967, ; 46: System.Collections => 0x1a61054f => 14
	i32 450948140, ; 47: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 236
	i32 451504562, ; 48: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 126
	i32 456227837, ; 49: System.Web.HttpUtility.dll => 0x1b317bfd => 153
	i32 459347974, ; 50: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 114
	i32 465846621, ; 51: mscorlib => 0x1bc4415d => 167
	i32 469710990, ; 52: System.dll => 0x1bff388e => 165
	i32 476646585, ; 53: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 237
	i32 497347661, ; 54: Avalonia.Dialogs => 0x1da4ec4d => 177
	i32 498788369, ; 55: System.ObjectModel => 0x1dbae811 => 85
	i32 513247710, ; 56: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 210
	i32 525008092, ; 57: SkiaSharp.dll => 0x1f4afcdc => 212
	i32 526420162, ; 58: System.Transactions.dll => 0x1f6088c2 => 151
	i32 527452488, ; 59: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 258
	i32 527885601, ; 60: Avalonia.Skia => 0x1f76e521 => 192
	i32 530272170, ; 61: System.Linq.Queryable => 0x1f9b4faa => 61
	i32 539058512, ; 62: Microsoft.Extensions.Logging => 0x20216150 => 206
	i32 540030774, ; 63: System.IO.FileSystem.dll => 0x20303736 => 52
	i32 545304856, ; 64: System.Runtime.Extensions => 0x2080b118 => 104
	i32 546455878, ; 65: System.Runtime.Serialization.Xml => 0x20924146 => 115
	i32 546678719, ; 66: Avalonia.Controls.DataGrid.dll => 0x2095a7bf => 187
	i32 549171840, ; 67: System.Globalization.Calendars => 0x20bbb280 => 41
	i32 577335427, ; 68: System.Security.Cryptography.Cng => 0x22697083 => 121
	i32 578483320, ; 69: ReactiveUI => 0x227af478 => 211
	i32 601371474, ; 70: System.IO.IsolatedStorage.dll => 0x23d83352 => 53
	i32 605376203, ; 71: System.IO.Compression.FileSystem => 0x24154ecb => 45
	i32 610194910, ; 72: System.Reactive.dll => 0x245ed5de => 215
	i32 613668793, ; 73: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 120
	i32 627609679, ; 74: Xamarin.AndroidX.CustomView => 0x2568904f => 232
	i32 637581149, ; 75: Avalonia.Controls => 0x2600b75d => 175
	i32 639843206, ; 76: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 235
	i32 643868501, ; 77: System.Net => 0x2660a755 => 82
	i32 662205335, ; 78: System.Text.Encodings.Web.dll => 0x27787397 => 137
	i32 662410334, ; 79: Avalonia.Themes.Simple.dll => 0x277b945e => 194
	i32 663517072, ; 80: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 252
	i32 666292255, ; 81: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 223
	i32 672442732, ; 82: System.Collections.Concurrent => 0x2814a96c => 10
	i32 683518922, ; 83: System.Net.Security => 0x28bdabca => 74
	i32 690569205, ; 84: System.Xml.Linq.dll => 0x29293ff5 => 156
	i32 691348768, ; 85: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 260
	i32 693804605, ; 86: System.Windows => 0x295a9e3d => 155
	i32 699345723, ; 87: System.Reflection.Emit => 0x29af2b3b => 93
	i32 700284507, ; 88: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 255
	i32 700358131, ; 89: System.IO.Compression.ZipFile => 0x29be9df3 => 46
	i32 720511267, ; 90: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 259
	i32 722857257, ; 91: System.Runtime.Loader.dll => 0x2b15ed29 => 110
	i32 731701662, ; 92: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 209
	i32 735137430, ; 93: System.Security.SecureString.dll => 0x2bd14e96 => 130
	i32 752232764, ; 94: System.Diagnostics.Contracts.dll => 0x2cd6293c => 27
	i32 759454413, ; 95: System.Net.Requests => 0x2d445acd => 73
	i32 762598435, ; 96: System.IO.Pipes.dll => 0x2d745423 => 56
	i32 775507847, ; 97: System.IO.Compression => 0x2e394f87 => 47
	i32 789151979, ; 98: Microsoft.Extensions.Options => 0x2f0980eb => 208
	i32 793404064, ; 99: Avalonia.Metal.dll => 0x2f4a62a0 => 180
	i32 795860074, ; 100: Avalonia.MicroCom.dll => 0x2f6fdc6a => 181
	i32 804715423, ; 101: System.Data.Common => 0x2ff6fb9f => 24
	i32 804967435, ; 102: ru-RU\ShessBord.resources => 0x2ffad40b => 1
	i32 823281589, ; 103: System.Private.Uri.dll => 0x311247b5 => 87
	i32 830298997, ; 104: System.IO.Compression.Brotli => 0x317d5b75 => 44
	i32 832635846, ; 105: System.Xml.XPath.dll => 0x31a103c6 => 161
	i32 834051424, ; 106: System.Net.Quic => 0x31b69d60 => 72
	i32 873119928, ; 107: Microsoft.VisualBasic => 0x340ac0b8 => 5
	i32 877678880, ; 108: System.Globalization.dll => 0x34505120 => 43
	i32 878954865, ; 109: System.Net.Http.Json => 0x3463c971 => 64
	i32 904024072, ; 110: System.ComponentModel.Primitives.dll => 0x35e25008 => 18
	i32 911108515, ; 111: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 54
	i32 928116545, ; 112: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 254
	i32 952186615, ; 113: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 106
	i32 956575887, ; 114: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 259
	i32 967690846, ; 115: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 238
	i32 975236339, ; 116: System.Diagnostics.Tracing => 0x3a20ecf3 => 35
	i32 975874589, ; 117: System.Xml.XDocument => 0x3a2aaa1d => 159
	i32 986514023, ; 118: System.Private.DataContractSerialization.dll => 0x3acd0267 => 86
	i32 987214855, ; 119: System.Diagnostics.Tools => 0x3ad7b407 => 33
	i32 992768348, ; 120: System.Collections.dll => 0x3b2c715c => 14
	i32 994442037, ; 121: System.IO.FileSystem => 0x3b45fb35 => 52
	i32 1001831731, ; 122: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 57
	i32 1008800730, ; 123: Avalonia.Base.dll => 0x3c2113da => 174
	i32 1012816738, ; 124: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 247
	i32 1019214401, ; 125: System.Drawing => 0x3cbffa41 => 37
	i32 1021638552, ; 126: ShessBord.Android.dll => 0x3ce4f798 => 2
	i32 1028951442, ; 127: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 202
	i32 1035644815, ; 128: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 221
	i32 1036536393, ; 129: System.Drawing.Primitives.dll => 0x3dc84a49 => 36
	i32 1044663988, ; 130: System.Linq.Expressions.dll => 0x3e444eb4 => 59
	i32 1048992957, ; 131: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 204
	i32 1052210849, ; 132: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 242
	i32 1052365087, ; 133: Avalonia.OpenGL.dll => 0x3eb9d11f => 182
	i32 1072331303, ; 134: Avalonia.Skia.dll => 0x3fea7a27 => 192
	i32 1082857460, ; 135: System.ComponentModel.TypeConverter => 0x408b17f4 => 19
	i32 1084122840, ; 136: Xamarin.Kotlin.StdLib => 0x409e66d8 => 256
	i32 1098259244, ; 137: System => 0x41761b2c => 165
	i32 1143774617, ; 138: MicroCom.Runtime.dll => 0x442c9d99 => 197
	i32 1150626690, ; 139: en-US/ShessBord.resources.dll => 0x44952b82 => 0
	i32 1170634674, ; 140: System.Web.dll => 0x45c677b2 => 154
	i32 1175144683, ; 141: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 251
	i32 1204270330, ; 142: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 223
	i32 1208641965, ; 143: System.Diagnostics.Process => 0x480a69ad => 30
	i32 1219128291, ; 144: System.IO.IsolatedStorage => 0x48aa6be3 => 53
	i32 1246548578, ; 145: Xamarin.AndroidX.Collection.Jvm.dll => 0x4a4cd262 => 226
	i32 1253011324, ; 146: Microsoft.Win32.Registry => 0x4aaf6f7c => 7
	i32 1260169368, ; 147: Avalonia.Vulkan.dll => 0x4b1ca898 => 183
	i32 1264511973, ; 148: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 248
	i32 1267360935, ; 149: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 250
	i32 1275534314, ; 150: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 260
	i32 1278448581, ; 151: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 220
	i32 1286058215, ; 152: Avalonia.Fonts.Inter => 0x4ca7b0e7 => 189
	i32 1293217323, ; 153: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 233
	i32 1309188875, ; 154: System.Private.DataContractSerialization => 0x4e08a30b => 86
	i32 1324164729, ; 155: System.Linq => 0x4eed2679 => 62
	i32 1335329327, ; 156: System.Runtime.Serialization.Json.dll => 0x4f97822f => 113
	i32 1364015309, ; 157: System.IO => 0x514d38cd => 58
	i32 1376866003, ; 158: Xamarin.AndroidX.SavedState => 0x52114ed3 => 247
	i32 1379779777, ; 159: System.Resources.ResourceManager => 0x523dc4c1 => 100
	i32 1397501879, ; 160: MicroCom.Runtime => 0x534c2fb7 => 197
	i32 1402170036, ; 161: System.Configuration.dll => 0x53936ab4 => 21
	i32 1408764838, ; 162: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 112
	i32 1411638395, ; 163: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 102
	i32 1422545099, ; 164: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 103
	i32 1434145427, ; 165: System.Runtime.Handles => 0x557b5293 => 105
	i32 1439761251, ; 166: System.Net.Quic.dll => 0x55d10363 => 72
	i32 1452070440, ; 167: System.Formats.Asn1.dll => 0x568cd628 => 39
	i32 1453312822, ; 168: System.Diagnostics.Tools.dll => 0x569fcb36 => 33
	i32 1457743152, ; 169: System.Runtime.Extensions.dll => 0x56e36530 => 104
	i32 1458022317, ; 170: System.Net.Security.dll => 0x56e7a7ad => 74
	i32 1461234159, ; 171: System.Collections.Immutable.dll => 0x5718a9ef => 11
	i32 1461719063, ; 172: System.Security.Cryptography.OpenSsl => 0x57201017 => 124
	i32 1462112819, ; 173: System.IO.Compression.dll => 0x57261233 => 47
	i32 1469204771, ; 174: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 222
	i32 1470490898, ; 175: Microsoft.Extensions.Primitives => 0x57a5e912 => 210
	i32 1479771757, ; 176: System.Collections.Immutable => 0x5833866d => 11
	i32 1480492111, ; 177: System.IO.Compression.Brotli.dll => 0x583e844f => 44
	i32 1487239319, ; 178: Microsoft.Win32.Primitives => 0x58a57897 => 6
	i32 1505131794, ; 179: Microsoft.Extensions.Http => 0x59b67d12 => 205
	i32 1536373174, ; 180: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 32
	i32 1543031311, ; 181: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 139
	i32 1543355203, ; 182: System.Reflection.Emit.dll => 0x5bfdbb43 => 93
	i32 1550322496, ; 183: System.Reflection.Extensions.dll => 0x5c680b40 => 94
	i32 1560592391, ; 184: Avalonia.Controls.DataGrid => 0x5d04c007 => 187
	i32 1565862583, ; 185: System.IO.FileSystem.Primitives => 0x5d552ab7 => 50
	i32 1566207040, ; 186: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 142
	i32 1573704789, ; 187: System.Runtime.Serialization.Json => 0x5dccd455 => 113
	i32 1580037396, ; 188: System.Threading.Overlapped => 0x5e2d7514 => 141
	i32 1592852173, ; 189: ReactiveUI.dll => 0x5ef0fecd => 211
	i32 1592978981, ; 190: System.Runtime.Serialization.dll => 0x5ef2ee25 => 116
	i32 1601112923, ; 191: System.Xml.Serialization => 0x5f6f0b5b => 158
	i32 1604827217, ; 192: System.Net.WebClient => 0x5fa7b851 => 77
	i32 1613762098, ; 193: Avalonia.Base => 0x60300e32 => 174
	i32 1618516317, ; 194: System.Net.WebSockets.Client.dll => 0x6078995d => 80
	i32 1622152042, ; 195: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 244
	i32 1622358360, ; 196: System.Dynamic.Runtime => 0x60b33958 => 38
	i32 1623297518, ; 197: Avalonia.Markup.Xaml.dll => 0x60c18dee => 178
	i32 1624330220, ; 198: Avalonia.Themes.Fluent.dll => 0x60d14fec => 193
	i32 1635184631, ; 199: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 235
	i32 1636350590, ; 200: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 231
	i32 1639515021, ; 201: System.Net.Http.dll => 0x61b9038d => 65
	i32 1639986890, ; 202: System.Text.RegularExpressions => 0x61c036ca => 139
	i32 1641389582, ; 203: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 17
	i32 1657153582, ; 204: System.Runtime => 0x62c6282e => 117
	i32 1658241508, ; 205: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 249
	i32 1675553242, ; 206: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 49
	i32 1677501392, ; 207: System.Net.Primitives.dll => 0x63fca3d0 => 71
	i32 1678508291, ; 208: System.Net.WebSockets => 0x640c0103 => 81
	i32 1679769178, ; 209: System.Security.Cryptography => 0x641f3e5a => 127
	i32 1691477237, ; 210: System.Reflection.Metadata => 0x64d1e4f5 => 95
	i32 1696967625, ; 211: System.Security.Cryptography.Csp => 0x6525abc9 => 122
	i32 1698840827, ; 212: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 257
	i32 1701541528, ; 213: System.Diagnostics.Debug.dll => 0x656b7698 => 28
	i32 1726116996, ; 214: System.Reflection.dll => 0x66e27484 => 98
	i32 1728033016, ; 215: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 29
	i32 1744735666, ; 216: System.Transactions.Local.dll => 0x67fe8db2 => 150
	i32 1746316138, ; 217: Mono.Android.Export => 0x6816ab6a => 170
	i32 1750313021, ; 218: Microsoft.Win32.Primitives.dll => 0x6853a83d => 6
	i32 1758240030, ; 219: System.Resources.Reader.dll => 0x68cc9d1e => 99
	i32 1763938596, ; 220: System.Diagnostics.TraceSource.dll => 0x69239124 => 34
	i32 1765942094, ; 221: System.Reflection.Extensions => 0x6942234e => 94
	i32 1770582343, ; 222: Microsoft.Extensions.Logging.dll => 0x6988f147 => 206
	i32 1776026572, ; 223: System.Core.dll => 0x69dc03cc => 23
	i32 1777075843, ; 224: System.Globalization.Extensions.dll => 0x69ec0683 => 42
	i32 1780572499, ; 225: Mono.Android.Runtime.dll => 0x6a216153 => 171
	i32 1788241197, ; 226: Xamarin.AndroidX.Fragment => 0x6a96652d => 236
	i32 1808609942, ; 227: Xamarin.AndroidX.Loader => 0x6bcd3296 => 244
	i32 1813058853, ; 228: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 256
	i32 1818787751, ; 229: Microsoft.VisualBasic.Core => 0x6c687fa7 => 4
	i32 1824175904, ; 230: System.Text.Encoding.Extensions => 0x6cbab720 => 135
	i32 1824722060, ; 231: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 112
	i32 1828688058, ; 232: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 207
	i32 1832351152, ; 233: Avalonia.Android => 0x6d3775b0 => 185
	i32 1836181115, ; 234: Avalonia.Fonts.Inter.dll => 0x6d71e67b => 189
	i32 1858542181, ; 235: System.Linq.Expressions => 0x6ec71a65 => 59
	i32 1870277092, ; 236: System.Reflection.Primitives => 0x6f7a29e4 => 96
	i32 1873312369, ; 237: ShessBord.Android => 0x6fa87a71 => 2
	i32 1879696579, ; 238: System.Formats.Tar.dll => 0x7009e4c3 => 40
	i32 1885316902, ; 239: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 224
	i32 1888955245, ; 240: System.Diagnostics.Contracts => 0x70972b6d => 27
	i32 1889954781, ; 241: System.Reflection.Metadata.dll => 0x70a66bdd => 95
	i32 1898237753, ; 242: System.Reflection.DispatchProxy => 0x7124cf39 => 90
	i32 1900610850, ; 243: System.Resources.ResourceManager.dll => 0x71490522 => 100
	i32 1910275211, ; 244: System.Collections.NonGeneric.dll => 0x71dc7c8b => 12
	i32 1919016533, ; 245: Xamarin.AndroidX.Core.SplashScreen.dll => 0x7261de55 => 230
	i32 1939592360, ; 246: System.Private.Xml.Linq => 0x739bd4a8 => 88
	i32 1956758971, ; 247: System.Resources.Writer => 0x74a1c5bb => 101
	i32 1968388702, ; 248: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 198
	i32 1983156543, ; 249: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 257
	i32 1994945615, ; 250: Avalonia.Metal => 0x76e8744f => 180
	i32 2011961780, ; 251: System.Buffers.dll => 0x77ec19b4 => 9
	i32 2019465201, ; 252: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 242
	i32 2045470958, ; 253: System.Private.Xml => 0x79eb68ee => 89
	i32 2048278909, ; 254: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 200
	i32 2055257422, ; 255: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 239
	i32 2060060697, ; 256: System.Windows.dll => 0x7aca0819 => 155
	i32 2070888862, ; 257: System.Diagnostics.TraceSource => 0x7b6f419e => 34
	i32 2079903147, ; 258: System.Runtime.dll => 0x7bf8cdab => 117
	i32 2090596640, ; 259: System.Numerics.Vectors => 0x7c9bf920 => 83
	i32 2127167465, ; 260: System.Console => 0x7ec9ffe9 => 22
	i32 2142473426, ; 261: System.Collections.Specialized => 0x7fb38cd2 => 13
	i32 2143790110, ; 262: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 163
	i32 2146852085, ; 263: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 5
	i32 2181898931, ; 264: Microsoft.Extensions.Options.dll => 0x820d22b3 => 208
	i32 2192057212, ; 265: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 207
	i32 2193016926, ; 266: System.ObjectModel.dll => 0x82b6c85e => 85
	i32 2201107256, ; 267: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 261
	i32 2201231467, ; 268: System.Net.Http => 0x8334206b => 65
	i32 2203033907, ; 269: Avalonia.MicroCom => 0x834fa133 => 181
	i32 2217644978, ; 270: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 251
	i32 2220367410, ; 271: Xamarin.AndroidX.Core.SplashScreen => 0x84581e32 => 230
	i32 2222056684, ; 272: System.Threading.Tasks.Parallel => 0x8471e4ec => 144
	i32 2252106437, ; 273: System.Xml.Serialization.dll => 0x863c6ac5 => 158
	i32 2256313426, ; 274: System.Globalization.Extensions => 0x867c9c52 => 42
	i32 2265110946, ; 275: System.Security.AccessControl.dll => 0x8702d9a2 => 118
	i32 2266799131, ; 276: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 199
	i32 2293034957, ; 277: System.ServiceModel.Web.dll => 0x88acefcd => 132
	i32 2295906218, ; 278: System.Net.Sockets => 0x88d8bfaa => 76
	i32 2298471582, ; 279: System.Net.Mail => 0x88ffe49e => 67
	i32 2305521784, ; 280: System.Private.CoreLib.dll => 0x896b7878 => 173
	i32 2309601686, ; 281: Avalonia => 0x89a9b996 => 184
	i32 2315684594, ; 282: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 218
	i32 2320631194, ; 283: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 144
	i32 2340441535, ; 284: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 107
	i32 2344264397, ; 285: System.ValueTuple => 0x8bbaa2cd => 152
	i32 2353062107, ; 286: System.Net.Primitives => 0x8c40e0db => 71
	i32 2368005991, ; 287: System.Xml.ReaderWriter.dll => 0x8d24e767 => 157
	i32 2371007202, ; 288: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 198
	i32 2378619854, ; 289: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 122
	i32 2383496789, ; 290: System.Security.Principal.Windows.dll => 0x8e114655 => 128
	i32 2384749489, ; 291: Avalonia.Android.dll => 0x8e2463b1 => 185
	i32 2401565422, ; 292: System.Web.HttpUtility => 0x8f24faee => 153
	i32 2403452196, ; 293: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 234
	i32 2408407545, ; 294: Avalonia.Markup.dll => 0x8f8d61f9 => 179
	i32 2421380589, ; 295: System.Threading.Tasks.Dataflow => 0x905355ed => 142
	i32 2435356389, ; 296: System.Console.dll => 0x912896e5 => 22
	i32 2435904999, ; 297: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 16
	i32 2454642406, ; 298: System.Text.Encoding.dll => 0x924edee6 => 136
	i32 2458678730, ; 299: System.Net.Sockets.dll => 0x928c75ca => 76
	i32 2459001652, ; 300: System.Linq.Parallel.dll => 0x92916334 => 60
	i32 2471841756, ; 301: netstandard.dll => 0x93554fdc => 168
	i32 2475788418, ; 302: Java.Interop.dll => 0x93918882 => 169
	i32 2483903535, ; 303: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 17
	i32 2484371297, ; 304: System.Net.ServicePoint => 0x94147f61 => 75
	i32 2490993605, ; 305: System.AppContext.dll => 0x94798bc5 => 8
	i32 2501346920, ; 306: System.Data.DataSetExtensions => 0x95178668 => 25
	i32 2505896520, ; 307: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 241
	i32 2538310050, ; 308: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 92
	i32 2562349572, ; 309: Microsoft.CSharp => 0x98ba5a04 => 3
	i32 2570120770, ; 310: System.Text.Encodings.Web => 0x9930ee42 => 137
	i32 2581819634, ; 311: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 250
	i32 2585220780, ; 312: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 135
	i32 2585805581, ; 313: System.Net.Ping => 0x9a20430d => 70
	i32 2589602615, ; 314: System.Threading.ThreadPool => 0x9a5a3337 => 147
	i32 2605712449, ; 315: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 261
	i32 2617129537, ; 316: System.Private.Xml.dll => 0x9bfe3a41 => 89
	i32 2618712057, ; 317: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 97
	i32 2620871830, ; 318: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 231
	i32 2627185994, ; 319: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 32
	i32 2629843544, ; 320: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 46
	i32 2660759594, ; 321: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 216
	i32 2663698177, ; 322: System.Runtime.Loader => 0x9ec4cf01 => 110
	i32 2664396074, ; 323: System.Xml.XDocument.dll => 0x9ecf752a => 159
	i32 2665622720, ; 324: System.Drawing.Primitives => 0x9ee22cc0 => 36
	i32 2668319029, ; 325: Avalonia.Controls.ColorPicker.dll => 0x9f0b5135 => 186
	i32 2676780864, ; 326: System.Data.Common.dll => 0x9f8c6f40 => 24
	i32 2686887180, ; 327: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 115
	i32 2693849962, ; 328: System.IO.dll => 0xa090e36a => 58
	i32 2701096212, ; 329: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 249
	i32 2705935227, ; 330: Avalonia.OpenGL => 0xa1494b7b => 182
	i32 2713243005, ; 331: Avalonia.Remote.Protocol.dll => 0xa1b8cd7d => 191
	i32 2715334215, ; 332: System.Threading.Tasks.dll => 0xa1d8b647 => 145
	i32 2717744543, ; 333: System.Security.Claims => 0xa1fd7d9f => 119
	i32 2719963679, ; 334: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 121
	i32 2724373263, ; 335: System.Runtime.Numerics.dll => 0xa262a30f => 111
	i32 2732626843, ; 336: Xamarin.AndroidX.Activity => 0xa2e0939b => 217
	i32 2735172069, ; 337: System.Threading.Channels => 0xa30769e5 => 140
	i32 2737747696, ; 338: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 222
	i32 2740948882, ; 339: System.IO.Pipes.AccessControl => 0xa35f8f92 => 55
	i32 2748088231, ; 340: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 106
	i32 2765824710, ; 341: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 134
	i32 2770495804, ; 342: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 255
	i32 2778768386, ; 343: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 253
	i32 2779977773, ; 344: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 246
	i32 2803228030, ; 345: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 160
	i32 2819470561, ; 346: System.Xml.dll => 0xa80db4e1 => 164
	i32 2819479764, ; 347: Avalonia.Controls.dll => 0xa80dd8d4 => 175
	i32 2821205001, ; 348: System.ServiceProcess.dll => 0xa8282c09 => 133
	i32 2821294376, ; 349: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 246
	i32 2824502124, ; 350: System.Xml.XmlDocument => 0xa85a7b6c => 162
	i32 2849599387, ; 351: System.Threading.Overlapped.dll => 0xa9d96f9b => 141
	i32 2853208004, ; 352: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 253
	i32 2861098320, ; 353: Mono.Android.Export.dll => 0xaa88e550 => 170
	i32 2867946736, ; 354: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 216
	i32 2875220617, ; 355: System.Globalization.Calendars.dll => 0xab606289 => 41
	i32 2887636118, ; 356: System.Net.dll => 0xac1dd496 => 82
	i32 2899753641, ; 357: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 57
	i32 2900621748, ; 358: System.Dynamic.Runtime.dll => 0xace3f9b4 => 38
	i32 2901442782, ; 359: System.Reflection => 0xacf080de => 98
	i32 2905242038, ; 360: mscorlib.dll => 0xad2a79b6 => 167
	i32 2909740682, ; 361: System.Private.CoreLib => 0xad6f1e8a => 173
	i32 2919462931, ; 362: System.Numerics.Vectors.dll => 0xae037813 => 83
	i32 2921128767, ; 363: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 219
	i32 2921597873, ; 364: Avalonia.Markup.Xaml => 0xae240bb1 => 178
	i32 2936416060, ; 365: System.Resources.Reader => 0xaf06273c => 99
	i32 2940926066, ; 366: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 31
	i32 2942453041, ; 367: System.Xml.XPath.XDocument => 0xaf624531 => 160
	i32 2959614098, ; 368: System.ComponentModel.dll => 0xb0682092 => 20
	i32 2968338931, ; 369: System.Security.Principal.Windows => 0xb0ed41f3 => 128
	i32 2971004615, ; 370: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 209
	i32 2972252294, ; 371: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 120
	i32 2977263743, ; 372: Avalonia.Controls.ColorPicker => 0xb175707f => 186
	i32 2978675010, ; 373: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 233
	i32 2996846495, ; 374: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 240
	i32 3016983068, ; 375: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 248
	i32 3020703001, ; 376: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 203
	i32 3023353419, ; 377: WindowsBase.dll => 0xb434b64b => 166
	i32 3038032645, ; 378: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 263
	i32 3048261857, ; 379: Splat.dll => 0xb5b0c8e1 => 213
	i32 3059408633, ; 380: Mono.Android.Runtime => 0xb65adef9 => 171
	i32 3059793426, ; 381: System.ComponentModel.Primitives => 0xb660be12 => 18
	i32 3075834255, ; 382: System.Threading.Tasks => 0xb755818f => 145
	i32 3082350816, ; 383: Avalonia.Themes.Simple => 0xb7b8f0e0 => 194
	i32 3090735792, ; 384: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 126
	i32 3091917316, ; 385: Avalonia.Vulkan => 0xb84aea04 => 183
	i32 3099732863, ; 386: System.Security.Claims.dll => 0xb8c22b7f => 119
	i32 3103600923, ; 387: System.Formats.Asn1 => 0xb8fd311b => 39
	i32 3111772706, ; 388: System.Runtime.Serialization => 0xb979e222 => 116
	i32 3121463068, ; 389: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 48
	i32 3124832203, ; 390: System.Threading.Tasks.Extensions => 0xba4127cb => 143
	i32 3132293585, ; 391: System.Security.AccessControl => 0xbab301d1 => 118
	i32 3147165239, ; 392: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 35
	i32 3159123045, ; 393: System.Reflection.Primitives.dll => 0xbc4c6465 => 96
	i32 3160747431, ; 394: System.IO.MemoryMappedFiles => 0xbc652da7 => 54
	i32 3192346100, ; 395: System.Security.SecureString => 0xbe4755f4 => 130
	i32 3193515020, ; 396: System.Web => 0xbe592c0c => 154
	i32 3196863873, ; 397: ShessBord => 0xbe8c4581 => 262
	i32 3204380047, ; 398: System.Data.dll => 0xbefef58f => 26
	i32 3209718065, ; 399: System.Xml.XmlDocument.dll => 0xbf506931 => 162
	i32 3220365878, ; 400: System.Threading => 0xbff2e236 => 149
	i32 3226221578, ; 401: System.Runtime.Handles.dll => 0xc04c3c0a => 105
	i32 3251039220, ; 402: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 90
	i32 3265493905, ; 403: System.Linq.Queryable.dll => 0xc2a37b91 => 61
	i32 3265893370, ; 404: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 143
	i32 3277815716, ; 405: System.Resources.Writer.dll => 0xc35f7fa4 => 101
	i32 3279906254, ; 406: Microsoft.Win32.Registry.dll => 0xc37f65ce => 7
	i32 3280506390, ; 407: System.ComponentModel.Annotations.dll => 0xc3888e16 => 15
	i32 3290767353, ; 408: System.Security.Cryptography.Encoding => 0xc4251ff9 => 123
	i32 3291646556, ; 409: Splat => 0xc4328a5c => 213
	i32 3299363146, ; 410: System.Text.Encoding => 0xc4a8494a => 136
	i32 3303498502, ; 411: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 29
	i32 3316684772, ; 412: System.Net.Requests.dll => 0xc5b097e4 => 73
	i32 3317135071, ; 413: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 232
	i32 3317144872, ; 414: System.Data => 0xc5b79d28 => 26
	i32 3340387945, ; 415: SkiaSharp => 0xc71a4669 => 212
	i32 3340431453, ; 416: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 224
	i32 3345895724, ; 417: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 245
	i32 3358260929, ; 418: System.Text.Json => 0xc82afec1 => 138
	i32 3362522851, ; 419: Xamarin.AndroidX.Core => 0xc86c06e3 => 228
	i32 3366347497, ; 420: Java.Interop => 0xc8a662e9 => 169
	i32 3395150330, ; 421: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 102
	i32 3403906625, ; 422: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 124
	i32 3421170118, ; 423: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 200
	i32 3428513518, ; 424: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 201
	i32 3429136800, ; 425: System.Xml => 0xcc6479a0 => 164
	i32 3430777524, ; 426: netstandard => 0xcc7d82b4 => 168
	i32 3432677996, ; 427: Avalonia.ReactiveUI.dll => 0xcc9a826c => 190
	i32 3445260447, ; 428: System.Formats.Tar => 0xcd5a809f => 40
	i32 3471940407, ; 429: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 19
	i32 3476120550, ; 430: Mono.Android => 0xcf3163e6 => 172
	i32 3485117614, ; 431: System.Text.Json.dll => 0xcfbaacae => 138
	i32 3486566296, ; 432: System.Transactions => 0xcfd0c798 => 151
	i32 3493954962, ; 433: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 227
	i32 3497148347, ; 434: Avalonia.Markup => 0xd0723fbb => 179
	i32 3509114376, ; 435: System.Xml.Linq => 0xd128d608 => 156
	i32 3515174580, ; 436: System.Security.dll => 0xd1854eb4 => 131
	i32 3530912306, ; 437: System.Configuration => 0xd2757232 => 21
	i32 3539954161, ; 438: System.Net.HttpListener => 0xd2ff69f1 => 66
	i32 3560100363, ; 439: System.Threading.Timer => 0xd432d20b => 148
	i32 3570554715, ; 440: System.IO.FileSystem.AccessControl => 0xd4d2575b => 48
	i32 3598340787, ; 441: System.Net.WebSockets.Client => 0xd67a52b3 => 80
	i32 3608519521, ; 442: System.Linq.dll => 0xd715a361 => 62
	i32 3615210680, ; 443: Avalonia.Dialogs.dll => 0xd77bbcb8 => 177
	i32 3624195450, ; 444: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 107
	i32 3625076442, ; 445: DynamicData.dll => 0xd81246da => 195
	i32 3633644679, ; 446: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 219
	i32 3638274909, ; 447: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 50
	i32 3641597786, ; 448: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 239
	i32 3645089577, ; 449: System.ComponentModel.DataAnnotations => 0xd943a729 => 16
	i32 3657292374, ; 450: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 199
	i32 3659473388, ; 451: Avalonia.ReactiveUI => 0xda1f21ec => 190
	i32 3660523487, ; 452: System.Net.NetworkInformation => 0xda2f27df => 69
	i32 3672681054, ; 453: Mono.Android.dll => 0xdae8aa5e => 172
	i32 3684561358, ; 454: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 227
	i32 3700866549, ; 455: System.Net.WebProxy.dll => 0xdc96bdf5 => 79
	i32 3706696989, ; 456: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 229
	i32 3716563718, ; 457: System.Runtime.Intrinsics => 0xdd864306 => 109
	i32 3718780102, ; 458: Xamarin.AndroidX.Annotation => 0xdda814c6 => 218
	i32 3731644420, ; 459: System.Reactive => 0xde6c6004 => 215
	i32 3732100267, ; 460: System.Net.NameResolution => 0xde7354ab => 68
	i32 3737834244, ; 461: System.Net.Http.Json.dll => 0xdecad304 => 64
	i32 3748608112, ; 462: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 214
	i32 3751444290, ; 463: System.Xml.XPath => 0xdf9a7f42 => 161
	i32 3777238188, ; 464: ru-RU/ShessBord.resources.dll => 0xe12414ac => 1
	i32 3786282454, ; 465: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 225
	i32 3792276235, ; 466: System.Collections.NonGeneric => 0xe2098b0b => 12
	i32 3792835768, ; 467: HarfBuzzSharp => 0xe21214b8 => 196
	i32 3802395368, ; 468: System.Collections.Specialized.dll => 0xe2a3f2e8 => 13
	i32 3819260425, ; 469: System.Net.WebProxy => 0xe3a54a09 => 79
	i32 3823082795, ; 470: System.Security.Cryptography.dll => 0xe3df9d2b => 127
	i32 3829621856, ; 471: System.Numerics.dll => 0xe4436460 => 84
	i32 3841636137, ; 472: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 202
	i32 3844307129, ; 473: System.Net.Mail.dll => 0xe52378b9 => 67
	i32 3849253459, ; 474: System.Runtime.InteropServices.dll => 0xe56ef253 => 108
	i32 3870376305, ; 475: System.Net.HttpListener.dll => 0xe6b14171 => 66
	i32 3873536506, ; 476: System.Security.Principal => 0xe6e179fa => 129
	i32 3875112723, ; 477: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 123
	i32 3885497537, ; 478: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 78
	i32 3888767677, ; 479: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 245
	i32 3896106733, ; 480: System.Collections.Concurrent.dll => 0xe839deed => 10
	i32 3896760992, ; 481: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 228
	i32 3901907137, ; 482: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 4
	i32 3910130544, ; 483: Xamarin.AndroidX.Collection.Jvm => 0xe90fdb70 => 226
	i32 3920810846, ; 484: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 45
	i32 3921031405, ; 485: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 252
	i32 3928044579, ; 486: System.Xml.ReaderWriter => 0xea213423 => 157
	i32 3930554604, ; 487: System.Security.Principal.dll => 0xea4780ec => 129
	i32 3945713374, ; 488: System.Data.DataSetExtensions.dll => 0xeb2ecede => 25
	i32 3953953790, ; 489: System.Text.Encoding.CodePages => 0xebac8bfe => 134
	i32 3955647286, ; 490: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 221
	i32 3959773229, ; 491: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 240
	i32 4000935579, ; 492: Avalonia.dll => 0xee796e9b => 184
	i32 4003436829, ; 493: System.Diagnostics.Process.dll => 0xee9f991d => 30
	i32 4003906742, ; 494: HarfBuzzSharp.dll => 0xeea6c4b6 => 196
	i32 4015948917, ; 495: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 220
	i32 4025784931, ; 496: System.Memory => 0xeff49a63 => 63
	i32 4054681211, ; 497: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 91
	i32 4063759658, ; 498: Avalonia.Remote.Protocol => 0xf2380d2a => 191
	i32 4068434129, ; 499: System.Private.Xml.Linq.dll => 0xf27f60d1 => 88
	i32 4071413702, ; 500: Avalonia.DesignerSupport => 0xf2acd7c6 => 176
	i32 4073602200, ; 501: System.Threading.dll => 0xf2ce3c98 => 149
	i32 4074166990, ; 502: Avalonia.Diagnostics => 0xf2d6dace => 188
	i32 4095183602, ; 503: en-US\ShessBord.resources => 0xf4178af2 => 0
	i32 4099507663, ; 504: System.Drawing.dll => 0xf45985cf => 37
	i32 4100113165, ; 505: System.Private.Uri => 0xf462c30d => 87
	i32 4101593132, ; 506: Xamarin.AndroidX.Emoji2 => 0xf479582c => 234
	i32 4126470640, ; 507: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 201
	i32 4127667938, ; 508: System.IO.FileSystem.Watcher => 0xf60736e2 => 51
	i32 4130442656, ; 509: System.AppContext => 0xf6318da0 => 8
	i32 4147896353, ; 510: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 91
	i32 4151237749, ; 511: System.Core => 0xf76edc75 => 23
	i32 4153554407, ; 512: Avalonia.Diagnostics.dll => 0xf79235e7 => 188
	i32 4159265925, ; 513: System.Xml.XmlSerializer => 0xf7e95c85 => 163
	i32 4161255271, ; 514: System.Reflection.TypeExtensions => 0xf807b767 => 97
	i32 4164802419, ; 515: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 51
	i32 4181436372, ; 516: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 114
	i32 4182413190, ; 517: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 243
	i32 4185676441, ; 518: System.Security => 0xf97c5a99 => 131
	i32 4196529839, ; 519: System.Net.WebClient.dll => 0xfa21f6af => 77
	i32 4213026141, ; 520: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 214
	i32 4228013989, ; 521: ShessBord.dll => 0xfc025fa5 => 262
	i32 4256097574, ; 522: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 229
	i32 4259647912, ; 523: DynamicData => 0xfde511a8 => 195
	i32 4260525087, ; 524: System.Buffers => 0xfdf2741f => 9
	i32 4261741634, ; 525: Avalonia.DesignerSupport.dll => 0xfe050442 => 176
	i32 4274976490, ; 526: System.Runtime.Numerics => 0xfecef6ea => 111
	i32 4292120959 ; 527: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 243
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [528 x i32] [
	i32 69, ; 0
	i32 68, ; 1
	i32 109, ; 2
	i32 241, ; 3
	i32 254, ; 4
	i32 49, ; 5
	i32 81, ; 6
	i32 146, ; 7
	i32 31, ; 8
	i32 125, ; 9
	i32 103, ; 10
	i32 204, ; 11
	i32 108, ; 12
	i32 140, ; 13
	i32 258, ; 14
	i32 78, ; 15
	i32 125, ; 16
	i32 15, ; 17
	i32 225, ; 18
	i32 133, ; 19
	i32 152, ; 20
	i32 20, ; 21
	i32 28, ; 22
	i32 203, ; 23
	i32 3, ; 24
	i32 60, ; 25
	i32 43, ; 26
	i32 92, ; 27
	i32 193, ; 28
	i32 148, ; 29
	i32 237, ; 30
	i32 55, ; 31
	i32 205, ; 32
	i32 70, ; 33
	i32 217, ; 34
	i32 84, ; 35
	i32 238, ; 36
	i32 132, ; 37
	i32 56, ; 38
	i32 150, ; 39
	i32 75, ; 40
	i32 146, ; 41
	i32 63, ; 42
	i32 147, ; 43
	i32 263, ; 44
	i32 166, ; 45
	i32 14, ; 46
	i32 236, ; 47
	i32 126, ; 48
	i32 153, ; 49
	i32 114, ; 50
	i32 167, ; 51
	i32 165, ; 52
	i32 237, ; 53
	i32 177, ; 54
	i32 85, ; 55
	i32 210, ; 56
	i32 212, ; 57
	i32 151, ; 58
	i32 258, ; 59
	i32 192, ; 60
	i32 61, ; 61
	i32 206, ; 62
	i32 52, ; 63
	i32 104, ; 64
	i32 115, ; 65
	i32 187, ; 66
	i32 41, ; 67
	i32 121, ; 68
	i32 211, ; 69
	i32 53, ; 70
	i32 45, ; 71
	i32 215, ; 72
	i32 120, ; 73
	i32 232, ; 74
	i32 175, ; 75
	i32 235, ; 76
	i32 82, ; 77
	i32 137, ; 78
	i32 194, ; 79
	i32 252, ; 80
	i32 223, ; 81
	i32 10, ; 82
	i32 74, ; 83
	i32 156, ; 84
	i32 260, ; 85
	i32 155, ; 86
	i32 93, ; 87
	i32 255, ; 88
	i32 46, ; 89
	i32 259, ; 90
	i32 110, ; 91
	i32 209, ; 92
	i32 130, ; 93
	i32 27, ; 94
	i32 73, ; 95
	i32 56, ; 96
	i32 47, ; 97
	i32 208, ; 98
	i32 180, ; 99
	i32 181, ; 100
	i32 24, ; 101
	i32 1, ; 102
	i32 87, ; 103
	i32 44, ; 104
	i32 161, ; 105
	i32 72, ; 106
	i32 5, ; 107
	i32 43, ; 108
	i32 64, ; 109
	i32 18, ; 110
	i32 54, ; 111
	i32 254, ; 112
	i32 106, ; 113
	i32 259, ; 114
	i32 238, ; 115
	i32 35, ; 116
	i32 159, ; 117
	i32 86, ; 118
	i32 33, ; 119
	i32 14, ; 120
	i32 52, ; 121
	i32 57, ; 122
	i32 174, ; 123
	i32 247, ; 124
	i32 37, ; 125
	i32 2, ; 126
	i32 202, ; 127
	i32 221, ; 128
	i32 36, ; 129
	i32 59, ; 130
	i32 204, ; 131
	i32 242, ; 132
	i32 182, ; 133
	i32 192, ; 134
	i32 19, ; 135
	i32 256, ; 136
	i32 165, ; 137
	i32 197, ; 138
	i32 0, ; 139
	i32 154, ; 140
	i32 251, ; 141
	i32 223, ; 142
	i32 30, ; 143
	i32 53, ; 144
	i32 226, ; 145
	i32 7, ; 146
	i32 183, ; 147
	i32 248, ; 148
	i32 250, ; 149
	i32 260, ; 150
	i32 220, ; 151
	i32 189, ; 152
	i32 233, ; 153
	i32 86, ; 154
	i32 62, ; 155
	i32 113, ; 156
	i32 58, ; 157
	i32 247, ; 158
	i32 100, ; 159
	i32 197, ; 160
	i32 21, ; 161
	i32 112, ; 162
	i32 102, ; 163
	i32 103, ; 164
	i32 105, ; 165
	i32 72, ; 166
	i32 39, ; 167
	i32 33, ; 168
	i32 104, ; 169
	i32 74, ; 170
	i32 11, ; 171
	i32 124, ; 172
	i32 47, ; 173
	i32 222, ; 174
	i32 210, ; 175
	i32 11, ; 176
	i32 44, ; 177
	i32 6, ; 178
	i32 205, ; 179
	i32 32, ; 180
	i32 139, ; 181
	i32 93, ; 182
	i32 94, ; 183
	i32 187, ; 184
	i32 50, ; 185
	i32 142, ; 186
	i32 113, ; 187
	i32 141, ; 188
	i32 211, ; 189
	i32 116, ; 190
	i32 158, ; 191
	i32 77, ; 192
	i32 174, ; 193
	i32 80, ; 194
	i32 244, ; 195
	i32 38, ; 196
	i32 178, ; 197
	i32 193, ; 198
	i32 235, ; 199
	i32 231, ; 200
	i32 65, ; 201
	i32 139, ; 202
	i32 17, ; 203
	i32 117, ; 204
	i32 249, ; 205
	i32 49, ; 206
	i32 71, ; 207
	i32 81, ; 208
	i32 127, ; 209
	i32 95, ; 210
	i32 122, ; 211
	i32 257, ; 212
	i32 28, ; 213
	i32 98, ; 214
	i32 29, ; 215
	i32 150, ; 216
	i32 170, ; 217
	i32 6, ; 218
	i32 99, ; 219
	i32 34, ; 220
	i32 94, ; 221
	i32 206, ; 222
	i32 23, ; 223
	i32 42, ; 224
	i32 171, ; 225
	i32 236, ; 226
	i32 244, ; 227
	i32 256, ; 228
	i32 4, ; 229
	i32 135, ; 230
	i32 112, ; 231
	i32 207, ; 232
	i32 185, ; 233
	i32 189, ; 234
	i32 59, ; 235
	i32 96, ; 236
	i32 2, ; 237
	i32 40, ; 238
	i32 224, ; 239
	i32 27, ; 240
	i32 95, ; 241
	i32 90, ; 242
	i32 100, ; 243
	i32 12, ; 244
	i32 230, ; 245
	i32 88, ; 246
	i32 101, ; 247
	i32 198, ; 248
	i32 257, ; 249
	i32 180, ; 250
	i32 9, ; 251
	i32 242, ; 252
	i32 89, ; 253
	i32 200, ; 254
	i32 239, ; 255
	i32 155, ; 256
	i32 34, ; 257
	i32 117, ; 258
	i32 83, ; 259
	i32 22, ; 260
	i32 13, ; 261
	i32 163, ; 262
	i32 5, ; 263
	i32 208, ; 264
	i32 207, ; 265
	i32 85, ; 266
	i32 261, ; 267
	i32 65, ; 268
	i32 181, ; 269
	i32 251, ; 270
	i32 230, ; 271
	i32 144, ; 272
	i32 158, ; 273
	i32 42, ; 274
	i32 118, ; 275
	i32 199, ; 276
	i32 132, ; 277
	i32 76, ; 278
	i32 67, ; 279
	i32 173, ; 280
	i32 184, ; 281
	i32 218, ; 282
	i32 144, ; 283
	i32 107, ; 284
	i32 152, ; 285
	i32 71, ; 286
	i32 157, ; 287
	i32 198, ; 288
	i32 122, ; 289
	i32 128, ; 290
	i32 185, ; 291
	i32 153, ; 292
	i32 234, ; 293
	i32 179, ; 294
	i32 142, ; 295
	i32 22, ; 296
	i32 16, ; 297
	i32 136, ; 298
	i32 76, ; 299
	i32 60, ; 300
	i32 168, ; 301
	i32 169, ; 302
	i32 17, ; 303
	i32 75, ; 304
	i32 8, ; 305
	i32 25, ; 306
	i32 241, ; 307
	i32 92, ; 308
	i32 3, ; 309
	i32 137, ; 310
	i32 250, ; 311
	i32 135, ; 312
	i32 70, ; 313
	i32 147, ; 314
	i32 261, ; 315
	i32 89, ; 316
	i32 97, ; 317
	i32 231, ; 318
	i32 32, ; 319
	i32 46, ; 320
	i32 216, ; 321
	i32 110, ; 322
	i32 159, ; 323
	i32 36, ; 324
	i32 186, ; 325
	i32 24, ; 326
	i32 115, ; 327
	i32 58, ; 328
	i32 249, ; 329
	i32 182, ; 330
	i32 191, ; 331
	i32 145, ; 332
	i32 119, ; 333
	i32 121, ; 334
	i32 111, ; 335
	i32 217, ; 336
	i32 140, ; 337
	i32 222, ; 338
	i32 55, ; 339
	i32 106, ; 340
	i32 134, ; 341
	i32 255, ; 342
	i32 253, ; 343
	i32 246, ; 344
	i32 160, ; 345
	i32 164, ; 346
	i32 175, ; 347
	i32 133, ; 348
	i32 246, ; 349
	i32 162, ; 350
	i32 141, ; 351
	i32 253, ; 352
	i32 170, ; 353
	i32 216, ; 354
	i32 41, ; 355
	i32 82, ; 356
	i32 57, ; 357
	i32 38, ; 358
	i32 98, ; 359
	i32 167, ; 360
	i32 173, ; 361
	i32 83, ; 362
	i32 219, ; 363
	i32 178, ; 364
	i32 99, ; 365
	i32 31, ; 366
	i32 160, ; 367
	i32 20, ; 368
	i32 128, ; 369
	i32 209, ; 370
	i32 120, ; 371
	i32 186, ; 372
	i32 233, ; 373
	i32 240, ; 374
	i32 248, ; 375
	i32 203, ; 376
	i32 166, ; 377
	i32 263, ; 378
	i32 213, ; 379
	i32 171, ; 380
	i32 18, ; 381
	i32 145, ; 382
	i32 194, ; 383
	i32 126, ; 384
	i32 183, ; 385
	i32 119, ; 386
	i32 39, ; 387
	i32 116, ; 388
	i32 48, ; 389
	i32 143, ; 390
	i32 118, ; 391
	i32 35, ; 392
	i32 96, ; 393
	i32 54, ; 394
	i32 130, ; 395
	i32 154, ; 396
	i32 262, ; 397
	i32 26, ; 398
	i32 162, ; 399
	i32 149, ; 400
	i32 105, ; 401
	i32 90, ; 402
	i32 61, ; 403
	i32 143, ; 404
	i32 101, ; 405
	i32 7, ; 406
	i32 15, ; 407
	i32 123, ; 408
	i32 213, ; 409
	i32 136, ; 410
	i32 29, ; 411
	i32 73, ; 412
	i32 232, ; 413
	i32 26, ; 414
	i32 212, ; 415
	i32 224, ; 416
	i32 245, ; 417
	i32 138, ; 418
	i32 228, ; 419
	i32 169, ; 420
	i32 102, ; 421
	i32 124, ; 422
	i32 200, ; 423
	i32 201, ; 424
	i32 164, ; 425
	i32 168, ; 426
	i32 190, ; 427
	i32 40, ; 428
	i32 19, ; 429
	i32 172, ; 430
	i32 138, ; 431
	i32 151, ; 432
	i32 227, ; 433
	i32 179, ; 434
	i32 156, ; 435
	i32 131, ; 436
	i32 21, ; 437
	i32 66, ; 438
	i32 148, ; 439
	i32 48, ; 440
	i32 80, ; 441
	i32 62, ; 442
	i32 177, ; 443
	i32 107, ; 444
	i32 195, ; 445
	i32 219, ; 446
	i32 50, ; 447
	i32 239, ; 448
	i32 16, ; 449
	i32 199, ; 450
	i32 190, ; 451
	i32 69, ; 452
	i32 172, ; 453
	i32 227, ; 454
	i32 79, ; 455
	i32 229, ; 456
	i32 109, ; 457
	i32 218, ; 458
	i32 215, ; 459
	i32 68, ; 460
	i32 64, ; 461
	i32 214, ; 462
	i32 161, ; 463
	i32 1, ; 464
	i32 225, ; 465
	i32 12, ; 466
	i32 196, ; 467
	i32 13, ; 468
	i32 79, ; 469
	i32 127, ; 470
	i32 84, ; 471
	i32 202, ; 472
	i32 67, ; 473
	i32 108, ; 474
	i32 66, ; 475
	i32 129, ; 476
	i32 123, ; 477
	i32 78, ; 478
	i32 245, ; 479
	i32 10, ; 480
	i32 228, ; 481
	i32 4, ; 482
	i32 226, ; 483
	i32 45, ; 484
	i32 252, ; 485
	i32 157, ; 486
	i32 129, ; 487
	i32 25, ; 488
	i32 134, ; 489
	i32 221, ; 490
	i32 240, ; 491
	i32 184, ; 492
	i32 30, ; 493
	i32 196, ; 494
	i32 220, ; 495
	i32 63, ; 496
	i32 91, ; 497
	i32 191, ; 498
	i32 88, ; 499
	i32 176, ; 500
	i32 149, ; 501
	i32 188, ; 502
	i32 0, ; 503
	i32 37, ; 504
	i32 87, ; 505
	i32 234, ; 506
	i32 201, ; 507
	i32 51, ; 508
	i32 8, ; 509
	i32 91, ; 510
	i32 23, ; 511
	i32 188, ; 512
	i32 163, ; 513
	i32 97, ; 514
	i32 51, ; 515
	i32 114, ; 516
	i32 243, ; 517
	i32 131, ; 518
	i32 77, ; 519
	i32 214, ; 520
	i32 262, ; 521
	i32 229, ; 522
	i32 195, ; 523
	i32 9, ; 524
	i32 176, ; 525
	i32 111, ; 526
	i32 243 ; 527
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ df9aaf29a52042a4fbf800daf2f3a38964b9e958"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
