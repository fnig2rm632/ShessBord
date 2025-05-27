; ModuleID = 'marshal_methods.arm64-v8a.ll'
source_filename = "marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [267 x ptr] zeroinitializer, align 8

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [528 x i64] [
	i64 98382396393917666, ; 0: Microsoft.Extensions.Primitives.dll => 0x15d8644ad360ce2 => 210
	i64 120698629574877762, ; 1: Mono.Android => 0x1accec39cafe242 => 172
	i64 186530032027918916, ; 2: Avalonia.ReactiveUI => 0x296b0136af76244 => 190
	i64 196720943101637631, ; 3: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 59
	i64 210515253464952879, ; 4: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 225
	i64 229794953483747371, ; 5: System.ValueTuple.dll => 0x330654aed93802b => 152
	i64 232391251801502327, ; 6: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 247
	i64 233177144301842968, ; 7: Xamarin.AndroidX.Collection.Jvm.dll => 0x33c696097d9f218 => 226
	i64 316157742385208084, ; 8: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 229
	i64 350667413455104241, ; 9: System.ServiceProcess.dll => 0x4ddd227954be8f1 => 133
	i64 354178770117062970, ; 10: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0x4ea4bb703cff13a => 209
	i64 422779754995088667, ; 11: System.IO.UnmanagedMemoryStream => 0x5de03f27ab57d1b => 57
	i64 464346026994987652, ; 12: System.Reactive.dll => 0x671b04057e67284 => 215
	i64 560278790331054453, ; 13: System.Reflection.Primitives => 0x7c6829760de3975 => 96
	i64 563128987812417704, ; 14: Avalonia.Base.dll => 0x7d0a2d4b148d0a8 => 174
	i64 634308326490598313, ; 15: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 241
	i64 649145001856603771, ; 16: System.Security.SecureString => 0x90239f09b62167b => 130
	i64 668723562677762733, ; 17: Microsoft.Extensions.Configuration.Binder.dll => 0x947c88986577aad => 200
	i64 684024108737575474, ; 18: Splat => 0x97e244d831b3a32 => 213
	i64 689551008517048957, ; 19: MicroCom.Runtime.dll => 0x991c6fd252bca7d => 197
	i64 707452703347108429, ; 20: Avalonia.Controls.dll => 0x9d1607c4664ea4d => 175
	i64 750875890346172408, ; 21: System.Threading.Thread => 0xa6ba5a4da7d1ff8 => 146
	i64 799765834175365804, ; 22: System.ComponentModel.dll => 0xb1956c9f18442ac => 20
	i64 872800313462103108, ; 23: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 233
	i64 940822596282819491, ; 24: System.Transactions => 0xd0e792aa81923a3 => 151
	i64 960778385402502048, ; 25: System.Runtime.Handles.dll => 0xd555ed9e1ca1ba0 => 105
	i64 1010599046655515943, ; 26: System.Reflection.Primitives.dll => 0xe065e7a82401d27 => 96
	i64 1092282731782904681, ; 27: Avalonia.Markup.Xaml.dll => 0xf28915b7e369b69 => 178
	i64 1268860745194512059, ; 28: System.Drawing.dll => 0x119be62002c19ebb => 37
	i64 1274477032785669217, ; 29: Splat.dll => 0x11afda1bdd956c61 => 213
	i64 1301626418029409250, ; 30: System.Diagnostics.FileVersionInfo => 0x12104e54b4e833e2 => 29
	i64 1315114680217950157, ; 31: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 223
	i64 1404195534211153682, ; 32: System.IO.FileSystem.Watcher.dll => 0x137cb4660bd87f12 => 51
	i64 1425944114962822056, ; 33: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 116
	i64 1476839205573959279, ; 34: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 71
	i64 1492954217099365037, ; 35: System.Net.HttpListener => 0x14b809f350210aad => 66
	i64 1513467482682125403, ; 36: Mono.Android.Runtime => 0x1500eaa8245f6c5b => 171
	i64 1537168428375924959, ; 37: System.Threading.Thread.dll => 0x15551e8a954ae0df => 146
	i64 1624659445732251991, ; 38: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 222
	i64 1628611045998245443, ; 39: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 243
	i64 1651782184287836205, ; 40: System.Globalization.Calendars => 0x16ec4f2524cb982d => 41
	i64 1659332977923810219, ; 41: System.Reflection.DispatchProxy => 0x1707228d493d63ab => 90
	i64 1682513316613008342, ; 42: System.Net.dll => 0x17597cf276952bd6 => 82
	i64 1735388228521408345, ; 43: System.Net.Mail.dll => 0x181556663c69b759 => 67
	i64 1743969030606105336, ; 44: System.Memory.dll => 0x1833d297e88f2af8 => 63
	i64 1767386781656293639, ; 45: System.Private.Uri.dll => 0x188704e9f5582107 => 87
	i64 1795316252682057001, ; 46: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 221
	i64 1825687700144851180, ; 47: System.Runtime.InteropServices.RuntimeInformation.dll => 0x1956254a55ef08ec => 107
	i64 1836611346387731153, ; 48: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 247
	i64 1854145951182283680, ; 49: System.Runtime.CompilerServices.VisualC => 0x19bb3feb3df2e3a0 => 103
	i64 1875417405349196092, ; 50: System.Drawing.Primitives => 0x1a06d2319b6c713c => 36
	i64 1875917498431009007, ; 51: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 218
	i64 1972385128188460614, ; 52: System.Security.Cryptography.Algorithms => 0x1b5f51d2edefbe46 => 120
	i64 1981742497975770890, ; 53: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 242
	i64 2040001226662520565, ; 54: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 143
	i64 2049694801020856142, ; 55: Avalonia.Themes.Fluent => 0x1c71fa8fd0d0274e => 193
	i64 2062890601515140263, ; 56: System.Threading.Tasks.Dataflow => 0x1ca0dc1289cd44a7 => 142
	i64 2064708342624596306, ; 57: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 258
	i64 2080945842184875448, ; 58: System.IO.MemoryMappedFiles => 0x1ce10137d8416db8 => 54
	i64 2102659300918482391, ; 59: System.Drawing.Primitives.dll => 0x1d2e257e6aead5d7 => 36
	i64 2106033277907880740, ; 60: System.Threading.Tasks.Dataflow.dll => 0x1d3a221ba6d9cb24 => 142
	i64 2228773263802574538, ; 61: ShessBord.dll => 0x1eee317991d726ca => 262
	i64 2262844636196693701, ; 62: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 233
	i64 2287834202362508563, ; 63: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 10
	i64 2287887973817120656, ; 64: System.ComponentModel.DataAnnotations.dll => 0x1fc035fd8d41f790 => 16
	i64 2304837677853103545, ; 65: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 246
	i64 2315304989185124968, ; 66: System.IO.FileSystem.dll => 0x20219d9ee311aa68 => 52
	i64 2329709569556905518, ; 67: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 239
	i64 2335503487726329082, ; 68: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 137
	i64 2337758774805907496, ; 69: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 102
	i64 2455219140186822125, ; 70: Avalonia.Controls.ColorPicker.dll => 0x2212b0ccb89355ed => 186
	i64 2479423007379663237, ; 71: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 251
	i64 2497223385847772520, ; 72: System.Runtime => 0x22a7eb7046413568 => 117
	i64 2516498815742412887, ; 73: Xamarin.AndroidX.Core.SplashScreen.dll => 0x22ec665706048857 => 230
	i64 2547086958574651984, ; 74: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 217
	i64 2592350477072141967, ; 75: System.Xml.dll => 0x23f9e10627330e8f => 164
	i64 2624866290265602282, ; 76: mscorlib.dll => 0x246d65fbde2db8ea => 167
	i64 2632269733008246987, ; 77: System.Net.NameResolution => 0x2487b36034f808cb => 68
	i64 2656907746661064104, ; 78: Microsoft.Extensions.DependencyInjection => 0x24df3b84c8b75da8 => 201
	i64 2706075432581334785, ; 79: System.Net.WebSockets => 0x258de944be6c0701 => 81
	i64 2783046991838674048, ; 80: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 102
	i64 2787234703088983483, ; 81: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 248
	i64 2815524396660695947, ; 82: System.Security.AccessControl => 0x2712c0857f68238b => 118
	i64 2833633450228989597, ; 83: Avalonia.Metal => 0x2753169c18903e9d => 180
	i64 3017136373564924869, ; 84: System.Net.WebProxy => 0x29df058bd93f63c5 => 79
	i64 3028909397620569818, ; 85: Avalonia.dll => 0x2a08d90c9e0436da => 184
	i64 3106852385031680087, ; 86: System.Runtime.Serialization.Xml => 0x2b1dc1c88b637057 => 115
	i64 3110390492489056344, ; 87: System.Security.Cryptography.Csp.dll => 0x2b2a53ac61900058 => 122
	i64 3135773902340015556, ; 88: System.IO.FileSystem.DriveInfo.dll => 0x2b8481c008eac5c4 => 49
	i64 3281594302220646930, ; 89: System.Security.Principal => 0x2d8a90a198ceba12 => 129
	i64 3289520064315143713, ; 90: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 238
	i64 3303437397778967116, ; 91: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 219
	i64 3311221304742556517, ; 92: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 83
	i64 3325875462027654285, ; 93: System.Runtime.Numerics => 0x2e27e21c8958b48d => 111
	i64 3328853167529574890, ; 94: System.Net.Sockets.dll => 0x2e327651a008c1ea => 76
	i64 3344514922410554693, ; 95: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 261
	i64 3350556814715231811, ; 96: ShessBord.Android => 0x2e7f91abc4dd9e43 => 2
	i64 3437845325506641314, ; 97: System.IO.MemoryMappedFiles.dll => 0x2fb5ae1beb8f7da2 => 54
	i64 3493805808809882663, ; 98: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 249
	i64 3494946837667399002, ; 99: Microsoft.Extensions.Configuration => 0x30808ba1c00a455a => 198
	i64 3508450208084372758, ; 100: System.Net.Ping => 0x30b084e02d03ad16 => 70
	i64 3531994851595924923, ; 101: System.Numerics => 0x31042a9aade235bb => 84
	i64 3551103847008531295, ; 102: System.Private.CoreLib.dll => 0x31480e226177735f => 173
	i64 3571415421602489686, ; 103: System.Runtime.dll => 0x319037675df7e556 => 117
	i64 3638003163729360188, ; 104: Microsoft.Extensions.Configuration.Abstractions => 0x327cc89a39d5f53c => 199
	i64 3647754201059316852, ; 105: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 157
	i64 3655542548057982301, ; 106: Microsoft.Extensions.Configuration.dll => 0x32bb18945e52855d => 198
	i64 3716579019761409177, ; 107: netstandard.dll => 0x3393f0ed5c8c5c99 => 168
	i64 3869221888984012293, ; 108: Microsoft.Extensions.Logging.dll => 0x35b23cceda0ed605 => 206
	i64 3869649043256705283, ; 109: System.Diagnostics.Tools => 0x35b3c14d74bf0103 => 33
	i64 3919223565570527920, ; 110: System.Security.Cryptography.Encoding => 0x3663e111652bd2b0 => 123
	i64 3933965368022646939, ; 111: System.Net.Requests => 0x369840a8bfadc09b => 73
	i64 3966267475168208030, ; 112: System.Memory => 0x370b03412596249e => 63
	i64 4006972109285359177, ; 113: System.Xml.XmlDocument => 0x379b9fe74ed9fe49 => 162
	i64 4009997192427317104, ; 114: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 114
	i64 4073500526318903918, ; 115: System.Private.Xml.dll => 0x3887fb25779ae26e => 89
	i64 4108717018440987017, ; 116: Avalonia.Diagnostics => 0x3905185bfec6f189 => 188
	i64 4148881117810174540, ; 117: System.Runtime.InteropServices.JavaScript.dll => 0x3993c9651a66aa4c => 106
	i64 4154383907710350974, ; 118: System.ComponentModel => 0x39a7562737acb67e => 20
	i64 4167269041631776580, ; 119: System.Threading.ThreadPool => 0x39d51d1d3df1cf44 => 147
	i64 4168469861834746866, ; 120: System.Security.Claims.dll => 0x39d96140fb94ebf2 => 119
	i64 4187479170553454871, ; 121: System.Linq.Expressions => 0x3a1cea1e912fa117 => 59
	i64 4201423742386704971, ; 122: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 229
	i64 4205801962323029395, ; 123: System.ComponentModel.TypeConverter => 0x3a5e0299f7e7ad93 => 19
	i64 4235503420553921860, ; 124: System.IO.IsolatedStorage.dll => 0x3ac787eb9b118544 => 53
	i64 4281341464560019236, ; 125: Avalonia.ReactiveUI.dll => 0x3b6a6160e5402724 => 190
	i64 4282138915307457788, ; 126: System.Reflection.Emit => 0x3b6d36a7ddc70cfc => 93
	i64 4373617458794931033, ; 127: System.IO.Pipes.dll => 0x3cb235e806eb2359 => 56
	i64 4397634830160618470, ; 128: System.Security.SecureString.dll => 0x3d0789940f9be3e6 => 130
	i64 4477672992252076438, ; 129: System.Web.HttpUtility.dll => 0x3e23e3dcdb8ba196 => 153
	i64 4484706122338676047, ; 130: System.Globalization.Extensions.dll => 0x3e3ce07510042d4f => 42
	i64 4524629528625074585, ; 131: Avalonia.OpenGL => 0x3ecab69571f0d199 => 182
	i64 4533124835995628778, ; 132: System.Reflection.Emit.dll => 0x3ee8e505540534ea => 93
	i64 4636684751163556186, ; 133: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 252
	i64 4657212095206026001, ; 134: Microsoft.Extensions.Http.dll => 0x40a1bdb9c2686b11 => 205
	i64 4672453897036726049, ; 135: System.IO.FileSystem.Watcher => 0x40d7e4104a437f21 => 51
	i64 4674346441075111055, ; 136: Avalonia.Controls.DataGrid => 0x40de9d528964b48f => 187
	i64 4716677666592453464, ; 137: System.Xml.XmlSerializer => 0x417501590542f358 => 163
	i64 4743821336939966868, ; 138: System.ComponentModel.Annotations => 0x41d5705f4239b194 => 15
	i64 4759461199762736555, ; 139: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 240
	i64 4794310189461587505, ; 140: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 217
	i64 4809057822547766521, ; 141: System.Drawing => 0x42bd349c3145ecf9 => 37
	i64 4814660307502931973, ; 142: System.Net.NameResolution.dll => 0x42d11c0a5ee2a005 => 68
	i64 4853321196694829351, ; 143: System.Runtime.Loader.dll => 0x435a75ea15de7927 => 110
	i64 5081566143765835342, ; 144: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 100
	i64 5099468265966638712, ; 145: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 100
	i64 5103417709280584325, ; 146: System.Collections.Specialized => 0x46d2fb5e161b6285 => 13
	i64 5182934613077526976, ; 147: System.Collections.Specialized.dll => 0x47ed7b91fa9009c0 => 13
	i64 5238143335679712267, ; 148: DynamicData.dll => 0x48b19f9c65c4dc0b => 195
	i64 5244375036463807528, ; 149: System.Diagnostics.Contracts.dll => 0x48c7c34f4d59fc28 => 27
	i64 5262971552273843408, ; 150: System.Security.Principal.dll => 0x4909d4be0c44c4d0 => 129
	i64 5278787618751394462, ; 151: System.Net.WebClient.dll => 0x4942055efc68329e => 77
	i64 5290786973231294105, ; 152: System.Runtime.Loader => 0x496ca6b869b72699 => 110
	i64 5376510917114486089, ; 153: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 251
	i64 5415038968808478604, ; 154: ru-RU\ShessBord.resources => 0x4b26154084ba178c => 1
	i64 5423376490970181369, ; 155: System.Runtime.InteropServices.RuntimeInformation => 0x4b43b42f2b7b6ef9 => 107
	i64 5440320908473006344, ; 156: Microsoft.VisualBasic.Core => 0x4b7fe70acda9f908 => 4
	i64 5446034149219586269, ; 157: System.Diagnostics.Debug => 0x4b94333452e150dd => 28
	i64 5454055681045341991, ; 158: ReactiveUI.dll => 0x4bb0b2bebde8a727 => 211
	i64 5457765010617926378, ; 159: System.Xml.Serialization => 0x4bbde05c557002ea => 158
	i64 5507995362134886206, ; 160: System.Core.dll => 0x4c705499688c873e => 23
	i64 5527431512186326818, ; 161: System.IO.FileSystem.Primitives.dll => 0x4cb561acbc2a8f22 => 50
	i64 5549668961369458392, ; 162: Avalonia.Skia => 0x4d046284577006d8 => 192
	i64 5570799893513421663, ; 163: System.IO.Compression.Brotli => 0x4d4f74fcdfa6c35f => 44
	i64 5573260873512690141, ; 164: System.Security.Cryptography.dll => 0x4d58333c6e4ea1dd => 127
	i64 5574231584441077149, ; 165: Xamarin.AndroidX.Annotation.Jvm => 0x4d5ba617ae5f8d9d => 220
	i64 5591791169662171124, ; 166: System.Linq.Parallel => 0x4d9a087135e137f4 => 60
	i64 5610343915401270973, ; 167: Avalonia.Markup.dll => 0x4ddbf210f14456bd => 179
	i64 5650097808083101034, ; 168: System.Security.Cryptography.Algorithms.dll => 0x4e692e055d01a56a => 120
	i64 5757522595884336624, ; 169: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 227
	i64 5783556987928984683, ; 170: Microsoft.VisualBasic => 0x504352701bbc3c6b => 5
	i64 5979151488806146654, ; 171: System.Formats.Asn1 => 0x52fa3699a489d25e => 39
	i64 5984759512290286505, ; 172: System.Security.Cryptography.Primitives => 0x530e23115c33dba9 => 125
	i64 6010974535988770325, ; 173: Microsoft.Extensions.Diagnostics.dll => 0x536b457e33877615 => 203
	i64 6222399776351216807, ; 174: System.Text.Json.dll => 0x565a67a0ffe264a7 => 138
	i64 6251069312384999852, ; 175: System.Transactions.Local => 0x56c0426b870da1ac => 150
	i64 6278736998281604212, ; 176: System.Private.DataContractSerialization => 0x57228e08a4ad6c74 => 86
	i64 6284145129771520194, ; 177: System.Reflection.Emit.ILGeneration => 0x5735c4b3610850c2 => 91
	i64 6319713645133255417, ; 178: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 241
	i64 6321005050625483161, ; 179: Avalonia.Vulkan.dll => 0x57b8b89a79fb6199 => 183
	i64 6328501323422750843, ; 180: Avalonia.Dialogs => 0x57d35a6c7f33d87b => 177
	i64 6357457916754632952, ; 181: _Microsoft.Android.Resource.Designer => 0x583a3a4ac2a7a0f8 => 263
	i64 6380710567210293407, ; 182: Avalonia.Base => 0x588cd6745526989f => 174
	i64 6384864380059521739, ; 183: Avalonia.OpenGL.dll => 0x589b9853407e12cb => 182
	i64 6401687960814735282, ; 184: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 239
	i64 6548213210057960872, ; 185: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 232
	i64 6560151584539558821, ; 186: Microsoft.Extensions.Options => 0x5b0a571be53243a5 => 208
	i64 6561785447785963248, ; 187: ru-RU/ShessBord.resources.dll => 0x5b102519539c1ef0 => 1
	i64 6617685658146568858, ; 188: System.Text.Encoding.CodePages => 0x5bd6be0b4905fa9a => 134
	i64 6671798237668743565, ; 189: SkiaSharp => 0x5c96fd260152998d => 212
	i64 6713440830605852118, ; 190: System.Reflection.TypeExtensions.dll => 0x5d2aeeddb8dd7dd6 => 97
	i64 6739853162153639747, ; 191: Microsoft.VisualBasic.dll => 0x5d88c4bde075ff43 => 5
	i64 6772837112740759457, ; 192: System.Runtime.InteropServices.JavaScript => 0x5dfdf378527ec7a1 => 106
	i64 6786606130239981554, ; 193: System.Diagnostics.TraceSource => 0x5e2ede51877147f2 => 34
	i64 6798329586179154312, ; 194: System.Windows => 0x5e5884bd523ca188 => 155
	i64 6814185388980153342, ; 195: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 159
	i64 6816440634817214622, ; 196: Avalonia.Dialogs.dll => 0x5e98dca46ed1789e => 177
	i64 6824903559247452477, ; 197: Avalonia.Remote.Protocol.dll => 0x5eb6eda0933e593d => 191
	i64 6833352825668324144, ; 198: Avalonia.Controls.ColorPicker => 0x5ed4f230b6df4b30 => 186
	i64 6876862101832370452, ; 199: System.Xml.Linq => 0x5f6f85a57d108914 => 156
	i64 6894844156784520562, ; 200: System.Numerics.Vectors => 0x5faf683aead1ad72 => 83
	i64 7060896174307865760, ; 201: System.Threading.Tasks.Parallel.dll => 0x61fd57a90988f4a0 => 144
	i64 7083547580668757502, ; 202: System.Private.Xml.Linq.dll => 0x624dd0fe8f56c5fe => 88
	i64 7101497697220435230, ; 203: System.Configuration => 0x628d9687c0141d1e => 21
	i64 7103753931438454322, ; 204: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 237
	i64 7112547816752919026, ; 205: System.IO.FileSystem => 0x62b4d88e3189b1f2 => 52
	i64 7191440545564009955, ; 206: en-US/ShessBord.resources.dll => 0x63cd211305af25e3 => 0
	i64 7270811800166795866, ; 207: System.Linq => 0x64e71ccf51a90a5a => 62
	i64 7299370801165188114, ; 208: System.IO.Pipes.AccessControl.dll => 0x654c9311e74f3c12 => 55
	i64 7316205155833392065, ; 209: Microsoft.Win32.Primitives => 0x658861d38954abc1 => 6
	i64 7338192458477945005, ; 210: System.Reflection => 0x65d67f295d0740ad => 98
	i64 7377312882064240630, ; 211: System.ComponentModel.TypeConverter.dll => 0x66617afac45a2ff6 => 19
	i64 7488575175965059935, ; 212: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 156
	i64 7489048572193775167, ; 213: System.ObjectModel => 0x67ee71ff6b419e3f => 85
	i64 7553498704560191638, ; 214: Avalonia.Skia.dll => 0x68d36b0d38aef896 => 192
	i64 7592535546311173087, ; 215: Avalonia.Fonts.Inter => 0x695e1ada366763df => 189
	i64 7592577537120840276, ; 216: System.Diagnostics.Process => 0x695e410af5b2aa54 => 30
	i64 7602111570124318452, ; 217: System.Reactive => 0x698020320025a6f4 => 215
	i64 7637303409920963731, ; 218: System.IO.Compression.ZipFile.dll => 0x69fd26fcb637f493 => 46
	i64 7654504624184590948, ; 219: System.Net.Http => 0x6a3a4366801b8264 => 65
	i64 7694700312542370399, ; 220: System.Net.Mail => 0x6ac9112a7e2cda5f => 67
	i64 7714652370974252055, ; 221: System.Private.CoreLib => 0x6b0ff375198b9c17 => 173
	i64 7735176074855944702, ; 222: Microsoft.CSharp => 0x6b58dda848e391fe => 3
	i64 7735352534559001595, ; 223: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 256
	i64 7791074099216502080, ; 224: System.IO.FileSystem.AccessControl.dll => 0x6c1f749d468bcd40 => 48
	i64 7820441508502274321, ; 225: System.Data => 0x6c87ca1e14ff8111 => 26
	i64 7836164640616011524, ; 226: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 222
	i64 7919757340696389605, ; 227: Microsoft.Extensions.Diagnostics.Abstractions => 0x6de8a157378027e5 => 204
	i64 8025517457475554965, ; 228: WindowsBase => 0x6f605d9b4786ce95 => 166
	i64 8031450141206250471, ; 229: System.Runtime.Intrinsics.dll => 0x6f757159d9dc03e7 => 109
	i64 8064050204834738623, ; 230: System.Collections.dll => 0x6fe942efa61731bf => 14
	i64 8083354569033831015, ; 231: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 238
	i64 8085230611270010360, ; 232: System.Net.Http.Json.dll => 0x703482674fdd05f8 => 64
	i64 8087206902342787202, ; 233: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 214
	i64 8103644804370223335, ; 234: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 25
	i64 8113615946733131500, ; 235: System.Reflection.Extensions => 0x70995ab73cf916ec => 94
	i64 8167236081217502503, ; 236: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 169
	i64 8185542183669246576, ; 237: System.Collections => 0x7198e33f4794aa70 => 14
	i64 8187640529827139739, ; 238: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 260
	i64 8235972910441640993, ; 239: Avalonia.Vulkan => 0x724c0db9da9d3421 => 183
	i64 8264926008854159966, ; 240: System.Diagnostics.Process.dll => 0x72b2ea6a64a3a25e => 30
	i64 8290740647658429042, ; 241: System.Runtime.Extensions => 0x730ea0b15c929a72 => 104
	i64 8307185734628499939, ; 242: Avalonia.Android => 0x73490d698bb961e3 => 185
	i64 8318905602908530212, ; 243: System.ComponentModel.DataAnnotations => 0x7372b092055ea624 => 16
	i64 8343033485683067408, ; 244: Avalonia.MicroCom.dll => 0x73c868c07f540a10 => 181
	i64 8368701292315763008, ; 245: System.Security.Cryptography => 0x7423997c6fd56140 => 127
	i64 8410671156615598628, ; 246: System.Reflection.Emit.Lightweight.dll => 0x74b8b4daf4b25224 => 92
	i64 8426919725312979251, ; 247: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 240
	i64 8518412311883997971, ; 248: System.Collections.Immutable => 0x76377add7c28e313 => 11
	i64 8563666267364444763, ; 249: System.Private.Uri => 0x76d841191140ca5b => 87
	i64 8598790081731763592, ; 250: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 235
	i64 8623059219396073920, ; 251: System.Net.Quic.dll => 0x77ab42ac514299c0 => 72
	i64 8626175481042262068, ; 252: Java.Interop => 0x77b654e585b55834 => 169
	i64 8638972117149407195, ; 253: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 3
	i64 8648495978913578441, ; 254: Microsoft.Win32.Registry.dll => 0x7805a1456889bdc9 => 7
	i64 8684531736582871431, ; 255: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 45
	i64 8725526185868997716, ; 256: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 214
	i64 8816904670177563593, ; 257: Microsoft.Extensions.Diagnostics => 0x7a5bf015646a93c9 => 203
	i64 8853378295825400934, ; 258: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 257
	i64 8941376889969657626, ; 259: System.Xml.XDocument => 0x7c1626e87187471a => 159
	i64 8951477988056063522, ; 260: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 245
	i64 8954753533646919997, ; 261: System.Runtime.Serialization.Json => 0x7c45ace50032d93d => 113
	i64 8962916719318709492, ; 262: Avalonia.Themes.Fluent.dll => 0x7c62ad44c666b4f4 => 193
	i64 8987845817414925545, ; 263: Avalonia.Remote.Protocol => 0x7cbb3e26baff3ce9 => 191
	i64 9125701779017665707, ; 264: ShessBord => 0x7ea5016b007f8cab => 262
	i64 9138683372487561558, ; 265: System.Security.Cryptography.Csp => 0x7ed3201bc3e3d156 => 122
	i64 9324707631942237306, ; 266: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 221
	i64 9468215723722196442, ; 267: System.Xml.XPath.XDocument.dll => 0x8365dc09353ac5da => 160
	i64 9554839972845591462, ; 268: System.ServiceModel.Web => 0x84999c54e32a1ba6 => 132
	i64 9584643793929893533, ; 269: System.IO.dll => 0x85037ebfbbd7f69d => 58
	i64 9659729154652888475, ; 270: System.Text.RegularExpressions => 0x860e407c9991dd9b => 139
	i64 9662334977499516867, ; 271: System.Numerics.dll => 0x8617827802b0cfc3 => 84
	i64 9667360217193089419, ; 272: System.Diagnostics.StackTrace => 0x86295ce5cd89898b => 31
	i64 9702891218465930390, ; 273: System.Collections.NonGeneric.dll => 0x86a79827b2eb3c96 => 12
	i64 9808709177481450983, ; 274: Mono.Android.dll => 0x881f890734e555e7 => 172
	i64 9825649861376906464, ; 275: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 227
	i64 9834056768316610435, ; 276: System.Transactions.dll => 0x8879968718899783 => 151
	i64 9836529246295212050, ; 277: System.Reflection.Metadata => 0x88825f3bbc2ac012 => 95
	i64 9907349773706910547, ; 278: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 235
	i64 9933555792566666578, ; 279: System.Linq.Queryable.dll => 0x89db145cf475c552 => 61
	i64 9974604633896246661, ; 280: System.Xml.Serialization.dll => 0x8a6cea111a59dd85 => 158
	i64 10004628763249497471, ; 281: Avalonia.Themes.Simple => 0x8ad794da7724557f => 194
	i64 10038780035334861115, ; 282: System.Net.Http.dll => 0x8b50e941206af13b => 65
	i64 10051358222726253779, ; 283: System.Private.Xml => 0x8b7d990c97ccccd3 => 89
	i64 10078727084704864206, ; 284: System.Net.WebSockets.Client => 0x8bded4e257f117ce => 80
	i64 10089571585547156312, ; 285: System.IO.FileSystem.AccessControl => 0x8c055be67469bb58 => 48
	i64 10105485790837105934, ; 286: System.Threading.Tasks.Parallel => 0x8c3de5c91d9a650e => 144
	i64 10127564692711858555, ; 287: en-US\ShessBord.resources => 0x8c8c566e0ce0e17b => 0
	i64 10205853378024263619, ; 288: Microsoft.Extensions.Configuration.Binder => 0x8da279930adb4fc3 => 200
	i64 10226222362177979215, ; 289: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 258
	i64 10229024438826829339, ; 290: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 232
	i64 10236703004850800690, ; 291: System.Net.ServicePoint.dll => 0x8e101325834e4832 => 75
	i64 10245369515835430794, ; 292: System.Reflection.Emit.Lightweight => 0x8e2edd4ad7fc978a => 92
	i64 10253386210042008591, ; 293: Avalonia.Markup.Xaml => 0x8e4b586eea71540f => 178
	i64 10321854143672141184, ; 294: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 255
	i64 10360651442923773544, ; 295: System.Text.Encoding => 0x8fc86d98211c1e68 => 136
	i64 10364469296367737616, ; 296: System.Reflection.Emit.ILGeneration.dll => 0x8fd5fde967711b10 => 91
	i64 10376576884623852283, ; 297: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 249
	i64 10406448008575299332, ; 298: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 261
	i64 10430153318873392755, ; 299: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 228
	i64 10466034231130677304, ; 300: MicroCom.Runtime => 0x913ed2ae899f6038 => 197
	i64 10546663366131771576, ; 301: System.Runtime.Serialization.Json.dll => 0x925d4673efe8e8b8 => 113
	i64 10566960649245365243, ; 302: System.Globalization.dll => 0x92a562b96dcd13fb => 43
	i64 10595762989148858956, ; 303: System.Xml.XPath.XDocument => 0x930bb64cc472ea4c => 160
	i64 10670374202010151210, ; 304: Microsoft.Win32.Primitives.dll => 0x9414c8cd7b4ea92a => 6
	i64 10714184849103829812, ; 305: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 104
	i64 10785150219063592792, ; 306: System.Net.Primitives => 0x95ac8cfb68830758 => 71
	i64 10809043855025277762, ; 307: Microsoft.Extensions.Options.ConfigurationExtensions => 0x9601701e0c668b42 => 209
	i64 10822644899632537592, ; 308: System.Linq.Queryable => 0x9631c23204ca5ff8 => 61
	i64 10830817578243619689, ; 309: System.Formats.Tar => 0x964ecb340a447b69 => 40
	i64 10847732767863316357, ; 310: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 223
	i64 10899834349646441345, ; 311: System.Web => 0x9743fd975946eb81 => 154
	i64 10943875058216066601, ; 312: System.IO.UnmanagedMemoryStream.dll => 0x97e07461df39de29 => 57
	i64 10964653383833615866, ; 313: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 35
	i64 11002576679268595294, ; 314: Microsoft.Extensions.Logging.Abstractions => 0x98b1013215cd365e => 207
	i64 11019817191295005410, ; 315: Xamarin.AndroidX.Annotation.Jvm.dll => 0x98ee415998e1b2e2 => 220
	i64 11023048688141570732, ; 316: System.Core => 0x98f9bc61168392ac => 23
	i64 11037814507248023548, ; 317: System.Xml => 0x992e31d0412bf7fc => 164
	i64 11162124722117608902, ; 318: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 253
	i64 11188319605227840848, ; 319: System.Threading.Overlapped => 0x9b44e5671724e550 => 141
	i64 11226290749488709958, ; 320: Microsoft.Extensions.Options.dll => 0x9bcbcbf50c874146 => 208
	i64 11235648312900863002, ; 321: System.Reflection.DispatchProxy.dll => 0x9bed0a9c8fac441a => 90
	i64 11299661109949763898, ; 322: Xamarin.AndroidX.Collection.Jvm => 0x9cd075e94cda113a => 226
	i64 11329751333533450475, ; 323: System.Threading.Timer.dll => 0x9d3b5ccf6cc500eb => 148
	i64 11340910727871153756, ; 324: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 231
	i64 11347436699239206956, ; 325: System.Xml.XmlSerializer.dll => 0x9d7a318e8162502c => 163
	i64 11432101114902388181, ; 326: System.AppContext => 0x9ea6fb64e61a9dd5 => 8
	i64 11446671985764974897, ; 327: Mono.Android.Export => 0x9edabf8623efc131 => 170
	i64 11448276831755070604, ; 328: System.Diagnostics.TextWriterTraceListener => 0x9ee0731f77186c8c => 32
	i64 11485890710487134646, ; 329: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 108
	i64 11529969570048099689, ; 330: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 253
	i64 11530571088791430846, ; 331: Microsoft.Extensions.Logging => 0xa004d1504ccd66be => 206
	i64 11580057168383206117, ; 332: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 218
	i64 11591352189662810718, ; 333: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 248
	i64 11597940890313164233, ; 334: netstandard => 0xa0f429ca8d1805c9 => 168
	i64 11672361001936329215, ; 335: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 237
	i64 11692977985522001935, ; 336: System.Threading.Overlapped.dll => 0xa245cd869980680f => 141
	i64 11707554492040141440, ; 337: System.Linq.Parallel.dll => 0xa27996c7fe94da80 => 60
	i64 11743665907891708234, ; 338: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 145
	i64 11889455492535845257, ; 339: ShessBord.Android.dll => 0xa4ffd4c7056a8189 => 2
	i64 11917635639362386622, ; 340: ReactiveUI => 0xa563f278bebafabe => 211
	i64 11991047634523762324, ; 341: System.Net => 0xa668c24ad493ae94 => 82
	i64 11997227339607644824, ; 342: Avalonia.Controls.DataGrid.dll => 0xa67eb6b38aeb1a98 => 187
	i64 12011556116648931059, ; 343: System.Security.Cryptography.ProtectedData => 0xa6b19ea5ec87aef3 => 216
	i64 12040886584167504988, ; 344: System.Net.ServicePoint => 0xa719d28d8e121c5c => 75
	i64 12063623837170009990, ; 345: System.Security => 0xa76a99f6ce740786 => 131
	i64 12096697103934194533, ; 346: System.Diagnostics.Contracts => 0xa7e019eccb7e8365 => 27
	i64 12102847907131387746, ; 347: System.Buffers => 0xa7f5f40c43256f62 => 9
	i64 12107153474885735932, ; 348: Avalonia.DesignerSupport.dll => 0xa8053ff05fb355fc => 176
	i64 12123043025855404482, ; 349: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 94
	i64 12137774235383566651, ; 350: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 250
	i64 12145679461940342714, ; 351: System.Text.Json => 0xa88e1f1ebcb62fba => 138
	i64 12201331334810686224, ; 352: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 114
	i64 12269460666702402136, ; 353: System.Collections.Immutable.dll => 0xaa45e178506c9258 => 11
	i64 12319827376581239000, ; 354: Avalonia => 0xaaf8d1b9cb41e0d8 => 184
	i64 12332222936682028543, ; 355: System.Runtime.Handles => 0xab24db6c07db5dff => 105
	i64 12375446203996702057, ; 356: System.Configuration.dll => 0xabbe6ac12e2e0569 => 21
	i64 12451044538927396471, ; 357: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 236
	i64 12466513435562512481, ; 358: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 244
	i64 12475113361194491050, ; 359: _Microsoft.Android.Resource.Designer.dll => 0xad2081818aba1caa => 263
	i64 12517810545449516888, ; 360: System.Diagnostics.TraceSource.dll => 0xadb8325e6f283f58 => 34
	i64 12550732019250633519, ; 361: System.IO.Compression => 0xae2d28465e8e1b2f => 47
	i64 12699999919562409296, ; 362: System.Diagnostics.StackTrace.dll => 0xb03f76a3ad01c550 => 31
	i64 12700543734426720211, ; 363: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 225
	i64 12708238894395270091, ; 364: System.IO => 0xb05cbbf17d3ba3cb => 58
	i64 12708922737231849740, ; 365: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 135
	i64 12717050818822477433, ; 366: System.Runtime.Serialization.Xml.dll => 0xb07c0a5786811679 => 115
	i64 12828192437253469131, ; 367: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 259
	i64 12835242264250840079, ; 368: System.IO.Pipes => 0xb21ff0d5d6c0740f => 56
	i64 12843321153144804894, ; 369: Microsoft.Extensions.Primitives => 0xb23ca48abd74d61e => 210
	i64 12843770487262409629, ; 370: System.AppContext.dll => 0xb23e3d357debf39d => 8
	i64 12859557719246324186, ; 371: System.Net.WebHeaderCollection.dll => 0xb276539ce04f41da => 78
	i64 12998524970302822152, ; 372: Avalonia.Fonts.Inter.dll => 0xb464099762f1d708 => 189
	i64 13068258254871114833, ; 373: System.Runtime.Serialization.Formatters.dll => 0xb55bc7a4eaa8b451 => 112
	i64 13106026140046202731, ; 374: HarfBuzzSharp.dll => 0xb5e1f555ee70176b => 196
	i64 13129914918964716986, ; 375: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 234
	i64 13173818576982874404, ; 376: System.Runtime.CompilerServices.VisualC.dll => 0xb6d2ce32a8819924 => 103
	i64 13343850469010654401, ; 377: Mono.Android.Runtime.dll => 0xb92ee14d854f44c1 => 171
	i64 13370592475155966277, ; 378: System.Runtime.Serialization => 0xb98de304062ea945 => 116
	i64 13401370062847626945, ; 379: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 250
	i64 13431476299110033919, ; 380: System.Net.WebClient => 0xba663087f18829ff => 77
	i64 13454009404024712428, ; 381: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 254
	i64 13463706743370286408, ; 382: System.Private.DataContractSerialization.dll => 0xbad8b1f3069e0548 => 86
	i64 13465488254036897740, ; 383: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 256
	i64 13491513212026656886, ; 384: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 224
	i64 13578472628727169633, ; 385: System.Xml.XPath => 0xbc706ce9fba5c261 => 161
	i64 13580399111273692417, ; 386: Microsoft.VisualBasic.Core.dll => 0xbc77450a277fbd01 => 4
	i64 13647894001087880694, ; 387: System.Data.dll => 0xbd670f48cb071df6 => 26
	i64 13702626353344114072, ; 388: System.Diagnostics.Tools.dll => 0xbe29821198fb6d98 => 33
	i64 13710614125866346983, ; 389: System.Security.AccessControl.dll => 0xbe45e2e7d0b769e7 => 118
	i64 13713329104121190199, ; 390: System.Dynamic.Runtime => 0xbe4f8829f32b5737 => 38
	i64 13717397318615465333, ; 391: System.ComponentModel.Primitives.dll => 0xbe5dfc2ef2f87d75 => 18
	i64 13768883594457632599, ; 392: System.IO.IsolatedStorage => 0xbf14e6adb159cf57 => 53
	i64 13828521679616088467, ; 393: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 257
	i64 13838448951611437224, ; 394: Avalonia.Markup => 0xc00c0c00932d30a8 => 179
	i64 13881769479078963060, ; 395: System.Console.dll => 0xc0a5f3cade5c6774 => 22
	i64 13911222732217019342, ; 396: System.Security.Cryptography.OpenSsl.dll => 0xc10e975ec1226bce => 124
	i64 13928444506500929300, ; 397: System.Windows.dll => 0xc14bc67b8bba9714 => 155
	i64 13959074834287824816, ; 398: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 236
	i64 14075334701871371868, ; 399: System.ServiceModel.Web.dll => 0xc355a25647c5965c => 132
	i64 14125464355221830302, ; 400: System.Threading.dll => 0xc407bafdbc707a9e => 149
	i64 14212104595480609394, ; 401: System.Security.Cryptography.Cng.dll => 0xc53b89d4a4518272 => 121
	i64 14220608275227875801, ; 402: System.Diagnostics.FileVersionInfo.dll => 0xc559bfe1def019d9 => 29
	i64 14226382999226559092, ; 403: System.ServiceProcess => 0xc56e43f6938e2a74 => 133
	i64 14232023429000439693, ; 404: System.Resources.Writer.dll => 0xc5824de7789ba78d => 101
	i64 14254574811015963973, ; 405: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 135
	i64 14298246716367104064, ; 406: System.Web.dll => 0xc66d93a217f4e840 => 154
	i64 14327695147300244862, ; 407: System.Reflection.dll => 0xc6d632d338eb4d7e => 98
	i64 14327709162229390963, ; 408: System.Security.Cryptography.X509Certificates => 0xc6d63f9253cade73 => 126
	i64 14346402571976470310, ; 409: System.Net.Ping.dll => 0xc718a920f3686f26 => 70
	i64 14461014870687870182, ; 410: System.Net.Requests.dll => 0xc8afd8683afdece6 => 73
	i64 14495724990987328804, ; 411: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 246
	i64 14551742072151931844, ; 412: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 137
	i64 14561513370130550166, ; 413: System.Security.Cryptography.Primitives.dll => 0xca14e3428abb8d96 => 125
	i64 14574160591280636898, ; 414: System.Net.Quic => 0xca41d1d72ec783e2 => 72
	i64 14622043554576106986, ; 415: System.Runtime.Serialization.Formatters => 0xcaebef2458cc85ea => 112
	i64 14669215534098758659, ; 416: Microsoft.Extensions.DependencyInjection.dll => 0xcb9385ceb3993c03 => 201
	i64 14690985099581930927, ; 417: System.Web.HttpUtility => 0xcbe0dd1ca5233daf => 153
	i64 14792063746108907174, ; 418: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 254
	i64 14832630590065248058, ; 419: System.Security.Claims => 0xcdd816ef5d6e873a => 119
	i64 14852515768018889994, ; 420: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 231
	i64 14912225920358050525, ; 421: System.Security.Principal.Windows => 0xcef2de7759506add => 128
	i64 14931407803744742450, ; 422: HarfBuzzSharp => 0xcf3704499ab36c32 => 196
	i64 14935719434541007538, ; 423: System.Text.Encoding.CodePages.dll => 0xcf4655b160b702b2 => 134
	i64 14954917835170835695, ; 424: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xcf8a8a895a82ecef => 202
	i64 14984936317414011727, ; 425: System.Net.WebHeaderCollection => 0xcff5302fe54ff34f => 78
	i64 14987728460634540364, ; 426: System.IO.Compression.dll => 0xcfff1ba06622494c => 47
	i64 15015154896917945444, ; 427: System.Net.Security.dll => 0xd0608bd33642dc64 => 74
	i64 15024878362326791334, ; 428: System.Net.Http.Json => 0xd0831743ebf0f4a6 => 64
	i64 15051741671811457419, ; 429: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0xd0e2874d8f44218b => 204
	i64 15071021337266399595, ; 430: System.Resources.Reader.dll => 0xd127060e7a18a96b => 99
	i64 15076659072870671916, ; 431: System.ObjectModel.dll => 0xd13b0d8c1620662c => 85
	i64 15115185479366240210, ; 432: System.IO.Compression.Brotli.dll => 0xd1c3ed1c1bc467d2 => 44
	i64 15133485256822086103, ; 433: System.Linq.dll => 0xd204f0a9127dd9d7 => 62
	i64 15150743910298169673, ; 434: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 245
	i64 15227001540531775957, ; 435: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd3512d3999b8e9d5 => 199
	i64 15234786388537674379, ; 436: System.Dynamic.Runtime.dll => 0xd36cd580c5be8a8b => 38
	i64 15250465174479574862, ; 437: System.Globalization.Calendars.dll => 0xd3a489469852174e => 41
	i64 15279429628684179188, ; 438: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 260
	i64 15299439993936780255, ; 439: System.Xml.XPath.dll => 0xd452879d55019bdf => 161
	i64 15338463749992804988, ; 440: System.Resources.Reader => 0xd4dd2b839286f27c => 99
	i64 15370334346939861994, ; 441: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 228
	i64 15391712275433856905, ; 442: Microsoft.Extensions.DependencyInjection.Abstractions => 0xd59a58c406411f89 => 202
	i64 15526743539506359484, ; 443: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 136
	i64 15527772828719725935, ; 444: System.Console => 0xd77dbb1e38cd3d6f => 22
	i64 15530465045505749832, ; 445: System.Net.HttpListener.dll => 0xd7874bacc9fdb348 => 66
	i64 15541854775306130054, ; 446: System.Security.Cryptography.X509Certificates.dll => 0xd7afc292e8d49286 => 126
	i64 15557562860424774966, ; 447: System.Net.Sockets => 0xd7e790fe7a6dc536 => 76
	i64 15582737692548360875, ; 448: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 243
	i64 15609085926864131306, ; 449: System.dll => 0xd89e9cf3334914ea => 165
	i64 15661133872274321916, ; 450: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 157
	i64 15710114879900314733, ; 451: Microsoft.Win32.Registry => 0xda058a3f5d096c6d => 7
	i64 15755368083429170162, ; 452: System.IO.FileSystem.Primitives => 0xdaa64fcbde529bf2 => 50
	i64 15817206913877585035, ; 453: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 145
	i64 15825747108975065274, ; 454: DynamicData => 0xdba05925af9fb8ba => 195
	i64 15847085070278954535, ; 455: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 140
	i64 15885744048853936810, ; 456: System.Resources.Writer => 0xdc75800bd0b6eaaa => 101
	i64 15934062614519587357, ; 457: System.Security.Cryptography.OpenSsl => 0xdd2129868f45a21d => 124
	i64 15937190497610202713, ; 458: System.Security.Cryptography.Cng => 0xdd2c465197c97e59 => 121
	i64 15963349826457351533, ; 459: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 143
	i64 15971679995444160383, ; 460: System.Formats.Tar.dll => 0xdda6ce5592a9677f => 40
	i64 16018552496348375205, ; 461: System.Net.NetworkInformation.dll => 0xde4d54a020caa8a5 => 69
	i64 16054465462676478687, ; 462: System.Globalization.Extensions => 0xdecceb47319bdadf => 42
	i64 16063929133569271981, ; 463: Avalonia.Android.dll => 0xdeee8a6fc771e8ad => 185
	i64 16083117170034450818, ; 464: Avalonia.Controls => 0xdf32b5daa8e3a182 => 175
	i64 16154507427712707110, ; 465: System => 0xe03056ea4e39aa26 => 165
	i64 16219561732052121626, ; 466: System.Net.Security => 0xe1177575db7c781a => 74
	i64 16315482530584035869, ; 467: WindowsBase.dll => 0xe26c3ceb1e8d821d => 166
	i64 16321164108206115771, ; 468: Microsoft.Extensions.Logging.Abstractions.dll => 0xe2806c487e7b0bbb => 207
	i64 16324796876805858114, ; 469: SkiaSharp.dll => 0xe28d5444586b6342 => 212
	i64 16337011941688632206, ; 470: System.Security.Principal.Windows.dll => 0xe2b8b9cdc3aa638e => 128
	i64 16423015068819898779, ; 471: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 259
	i64 16454459195343277943, ; 472: System.Net.NetworkInformation => 0xe459fb756d988f77 => 69
	i64 16496768397145114574, ; 473: Mono.Android.Export.dll => 0xe4f04b741db987ce => 170
	i64 16555111656825353880, ; 474: Avalonia.Metal.dll => 0xe5bf9256d200f698 => 180
	i64 16558262036769511634, ; 475: Microsoft.Extensions.Http => 0xe5cac397cf7b98d2 => 205
	i64 16702652415771857902, ; 476: System.ValueTuple => 0xe7cbbde0b0e6d3ee => 152
	i64 16709499819875633724, ; 477: System.IO.Compression.ZipFile => 0xe7e4118e32240a3c => 46
	i64 16737807731308835127, ; 478: System.Runtime.Intrinsics => 0xe848a3736f733137 => 109
	i64 16758309481308491337, ; 479: System.IO.FileSystem.DriveInfo => 0xe89179af15740e49 => 49
	i64 16762783179241323229, ; 480: System.Reflection.TypeExtensions => 0xe8a15e7d0d927add => 97
	i64 16765015072123548030, ; 481: System.Diagnostics.TextWriterTraceListener.dll => 0xe8a94c621bfe717e => 32
	i64 16822611501064131242, ; 482: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 25
	i64 16833383113903931215, ; 483: mscorlib => 0xe99c30c1484d7f4f => 167
	i64 16856067890322379635, ; 484: System.Data.Common.dll => 0xe9ecc87060889373 => 24
	i64 16890310621557459193, ; 485: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 139
	i64 16933958494752847024, ; 486: System.Net.WebProxy.dll => 0xeb018187f0f3b4b0 => 79
	i64 16977952268158210142, ; 487: System.IO.Pipes.AccessControl => 0xeb9dcda2851b905e => 55
	i64 17008137082415910100, ; 488: System.Collections.NonGeneric => 0xec090a90408c8cd4 => 12
	i64 17024911836938395553, ; 489: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 219
	i64 17062143951396181894, ; 490: System.ComponentModel.Primitives => 0xecc8e986518c9786 => 18
	i64 17118171214553292978, ; 491: System.Threading.Channels => 0xed8ff6060fc420b2 => 140
	i64 17134447395689342536, ; 492: Avalonia.DesignerSupport => 0xedc9c91fcaae2648 => 176
	i64 17187273293601214786, ; 493: System.ComponentModel.Annotations.dll => 0xee8575ff9aa89142 => 15
	i64 17201328579425343169, ; 494: System.ComponentModel.EventBasedAsync => 0xeeb76534d96c16c1 => 17
	i64 17202182880784296190, ; 495: System.Security.Cryptography.Encoding.dll => 0xeeba6e30627428fe => 123
	i64 17214520524539272569, ; 496: Avalonia.Themes.Simple.dll => 0xeee64335ebd59d79 => 194
	i64 17230721278011714856, ; 497: System.Private.Xml.Linq => 0xef1fd1b5c7a72d28 => 88
	i64 17234219099804750107, ; 498: System.Transactions.Local.dll => 0xef2c3ef5e11d511b => 150
	i64 17260702271250283638, ; 499: System.Data.Common => 0xef8a5543bba6bc76 => 24
	i64 17333249706306540043, ; 500: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 35
	i64 17338386382517543202, ; 501: System.Net.WebSockets.Client.dll => 0xf09e528d5c6da122 => 80
	i64 17375958953653056453, ; 502: Avalonia.MicroCom => 0xf123ce9b484233c5 => 181
	i64 17470386307322966175, ; 503: System.Threading.Timer => 0xf27347c8d0d5709f => 148
	i64 17509662556995089465, ; 504: System.Net.WebSockets.dll => 0xf2fed1534ea67439 => 81
	i64 17509666368860808140, ; 505: Xamarin.AndroidX.Core.SplashScreen => 0xf2fed4cad38d63cc => 230
	i64 17523946658117960076, ; 506: System.Security.Cryptography.ProtectedData.dll => 0xf33190a3c403c18c => 216
	i64 17627500474728259406, ; 507: System.Globalization => 0xf4a176498a351f4e => 43
	i64 17685921127322830888, ; 508: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 28
	i64 17704177640604968747, ; 509: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 244
	i64 17710060891934109755, ; 510: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 242
	i64 17712670374920797664, ; 511: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 108
	i64 17748157696115377834, ; 512: Avalonia.Diagnostics.dll => 0xf64e1f640e9286aa => 188
	i64 17777860260071588075, ; 513: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 111
	i64 17838668724098252521, ; 514: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 9
	i64 17891337867145587222, ; 515: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 255
	i64 17928294245072900555, ; 516: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 45
	i64 17992315986609351877, ; 517: System.Xml.XmlDocument.dll => 0xf9b18c0ffc6eacc5 => 162
	i64 18025913125965088385, ; 518: System.Threading => 0xfa28e87b91334681 => 149
	i64 18116111925905154859, ; 519: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 224
	i64 18146411883821974900, ; 520: System.Formats.Asn1.dll => 0xfbd50176eb22c574 => 39
	i64 18146811631844267958, ; 521: System.ComponentModel.EventBasedAsync.dll => 0xfbd66d08820117b6 => 17
	i64 18225059387460068507, ; 522: System.Threading.ThreadPool.dll => 0xfcec6af3cff4a49b => 147
	i64 18245806341561545090, ; 523: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 10
	i64 18260797123374478311, ; 524: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 234
	i64 18318849532986632368, ; 525: System.Security.dll => 0xfe39a097c37fa8b0 => 131
	i64 18380184030268848184, ; 526: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 252
	i64 18439108438687598470 ; 527: System.Reflection.Metadata.dll => 0xffe4df6e2ee1c786 => 95
], align 8

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [528 x i32] [
	i32 210, ; 0
	i32 172, ; 1
	i32 190, ; 2
	i32 59, ; 3
	i32 225, ; 4
	i32 152, ; 5
	i32 247, ; 6
	i32 226, ; 7
	i32 229, ; 8
	i32 133, ; 9
	i32 209, ; 10
	i32 57, ; 11
	i32 215, ; 12
	i32 96, ; 13
	i32 174, ; 14
	i32 241, ; 15
	i32 130, ; 16
	i32 200, ; 17
	i32 213, ; 18
	i32 197, ; 19
	i32 175, ; 20
	i32 146, ; 21
	i32 20, ; 22
	i32 233, ; 23
	i32 151, ; 24
	i32 105, ; 25
	i32 96, ; 26
	i32 178, ; 27
	i32 37, ; 28
	i32 213, ; 29
	i32 29, ; 30
	i32 223, ; 31
	i32 51, ; 32
	i32 116, ; 33
	i32 71, ; 34
	i32 66, ; 35
	i32 171, ; 36
	i32 146, ; 37
	i32 222, ; 38
	i32 243, ; 39
	i32 41, ; 40
	i32 90, ; 41
	i32 82, ; 42
	i32 67, ; 43
	i32 63, ; 44
	i32 87, ; 45
	i32 221, ; 46
	i32 107, ; 47
	i32 247, ; 48
	i32 103, ; 49
	i32 36, ; 50
	i32 218, ; 51
	i32 120, ; 52
	i32 242, ; 53
	i32 143, ; 54
	i32 193, ; 55
	i32 142, ; 56
	i32 258, ; 57
	i32 54, ; 58
	i32 36, ; 59
	i32 142, ; 60
	i32 262, ; 61
	i32 233, ; 62
	i32 10, ; 63
	i32 16, ; 64
	i32 246, ; 65
	i32 52, ; 66
	i32 239, ; 67
	i32 137, ; 68
	i32 102, ; 69
	i32 186, ; 70
	i32 251, ; 71
	i32 117, ; 72
	i32 230, ; 73
	i32 217, ; 74
	i32 164, ; 75
	i32 167, ; 76
	i32 68, ; 77
	i32 201, ; 78
	i32 81, ; 79
	i32 102, ; 80
	i32 248, ; 81
	i32 118, ; 82
	i32 180, ; 83
	i32 79, ; 84
	i32 184, ; 85
	i32 115, ; 86
	i32 122, ; 87
	i32 49, ; 88
	i32 129, ; 89
	i32 238, ; 90
	i32 219, ; 91
	i32 83, ; 92
	i32 111, ; 93
	i32 76, ; 94
	i32 261, ; 95
	i32 2, ; 96
	i32 54, ; 97
	i32 249, ; 98
	i32 198, ; 99
	i32 70, ; 100
	i32 84, ; 101
	i32 173, ; 102
	i32 117, ; 103
	i32 199, ; 104
	i32 157, ; 105
	i32 198, ; 106
	i32 168, ; 107
	i32 206, ; 108
	i32 33, ; 109
	i32 123, ; 110
	i32 73, ; 111
	i32 63, ; 112
	i32 162, ; 113
	i32 114, ; 114
	i32 89, ; 115
	i32 188, ; 116
	i32 106, ; 117
	i32 20, ; 118
	i32 147, ; 119
	i32 119, ; 120
	i32 59, ; 121
	i32 229, ; 122
	i32 19, ; 123
	i32 53, ; 124
	i32 190, ; 125
	i32 93, ; 126
	i32 56, ; 127
	i32 130, ; 128
	i32 153, ; 129
	i32 42, ; 130
	i32 182, ; 131
	i32 93, ; 132
	i32 252, ; 133
	i32 205, ; 134
	i32 51, ; 135
	i32 187, ; 136
	i32 163, ; 137
	i32 15, ; 138
	i32 240, ; 139
	i32 217, ; 140
	i32 37, ; 141
	i32 68, ; 142
	i32 110, ; 143
	i32 100, ; 144
	i32 100, ; 145
	i32 13, ; 146
	i32 13, ; 147
	i32 195, ; 148
	i32 27, ; 149
	i32 129, ; 150
	i32 77, ; 151
	i32 110, ; 152
	i32 251, ; 153
	i32 1, ; 154
	i32 107, ; 155
	i32 4, ; 156
	i32 28, ; 157
	i32 211, ; 158
	i32 158, ; 159
	i32 23, ; 160
	i32 50, ; 161
	i32 192, ; 162
	i32 44, ; 163
	i32 127, ; 164
	i32 220, ; 165
	i32 60, ; 166
	i32 179, ; 167
	i32 120, ; 168
	i32 227, ; 169
	i32 5, ; 170
	i32 39, ; 171
	i32 125, ; 172
	i32 203, ; 173
	i32 138, ; 174
	i32 150, ; 175
	i32 86, ; 176
	i32 91, ; 177
	i32 241, ; 178
	i32 183, ; 179
	i32 177, ; 180
	i32 263, ; 181
	i32 174, ; 182
	i32 182, ; 183
	i32 239, ; 184
	i32 232, ; 185
	i32 208, ; 186
	i32 1, ; 187
	i32 134, ; 188
	i32 212, ; 189
	i32 97, ; 190
	i32 5, ; 191
	i32 106, ; 192
	i32 34, ; 193
	i32 155, ; 194
	i32 159, ; 195
	i32 177, ; 196
	i32 191, ; 197
	i32 186, ; 198
	i32 156, ; 199
	i32 83, ; 200
	i32 144, ; 201
	i32 88, ; 202
	i32 21, ; 203
	i32 237, ; 204
	i32 52, ; 205
	i32 0, ; 206
	i32 62, ; 207
	i32 55, ; 208
	i32 6, ; 209
	i32 98, ; 210
	i32 19, ; 211
	i32 156, ; 212
	i32 85, ; 213
	i32 192, ; 214
	i32 189, ; 215
	i32 30, ; 216
	i32 215, ; 217
	i32 46, ; 218
	i32 65, ; 219
	i32 67, ; 220
	i32 173, ; 221
	i32 3, ; 222
	i32 256, ; 223
	i32 48, ; 224
	i32 26, ; 225
	i32 222, ; 226
	i32 204, ; 227
	i32 166, ; 228
	i32 109, ; 229
	i32 14, ; 230
	i32 238, ; 231
	i32 64, ; 232
	i32 214, ; 233
	i32 25, ; 234
	i32 94, ; 235
	i32 169, ; 236
	i32 14, ; 237
	i32 260, ; 238
	i32 183, ; 239
	i32 30, ; 240
	i32 104, ; 241
	i32 185, ; 242
	i32 16, ; 243
	i32 181, ; 244
	i32 127, ; 245
	i32 92, ; 246
	i32 240, ; 247
	i32 11, ; 248
	i32 87, ; 249
	i32 235, ; 250
	i32 72, ; 251
	i32 169, ; 252
	i32 3, ; 253
	i32 7, ; 254
	i32 45, ; 255
	i32 214, ; 256
	i32 203, ; 257
	i32 257, ; 258
	i32 159, ; 259
	i32 245, ; 260
	i32 113, ; 261
	i32 193, ; 262
	i32 191, ; 263
	i32 262, ; 264
	i32 122, ; 265
	i32 221, ; 266
	i32 160, ; 267
	i32 132, ; 268
	i32 58, ; 269
	i32 139, ; 270
	i32 84, ; 271
	i32 31, ; 272
	i32 12, ; 273
	i32 172, ; 274
	i32 227, ; 275
	i32 151, ; 276
	i32 95, ; 277
	i32 235, ; 278
	i32 61, ; 279
	i32 158, ; 280
	i32 194, ; 281
	i32 65, ; 282
	i32 89, ; 283
	i32 80, ; 284
	i32 48, ; 285
	i32 144, ; 286
	i32 0, ; 287
	i32 200, ; 288
	i32 258, ; 289
	i32 232, ; 290
	i32 75, ; 291
	i32 92, ; 292
	i32 178, ; 293
	i32 255, ; 294
	i32 136, ; 295
	i32 91, ; 296
	i32 249, ; 297
	i32 261, ; 298
	i32 228, ; 299
	i32 197, ; 300
	i32 113, ; 301
	i32 43, ; 302
	i32 160, ; 303
	i32 6, ; 304
	i32 104, ; 305
	i32 71, ; 306
	i32 209, ; 307
	i32 61, ; 308
	i32 40, ; 309
	i32 223, ; 310
	i32 154, ; 311
	i32 57, ; 312
	i32 35, ; 313
	i32 207, ; 314
	i32 220, ; 315
	i32 23, ; 316
	i32 164, ; 317
	i32 253, ; 318
	i32 141, ; 319
	i32 208, ; 320
	i32 90, ; 321
	i32 226, ; 322
	i32 148, ; 323
	i32 231, ; 324
	i32 163, ; 325
	i32 8, ; 326
	i32 170, ; 327
	i32 32, ; 328
	i32 108, ; 329
	i32 253, ; 330
	i32 206, ; 331
	i32 218, ; 332
	i32 248, ; 333
	i32 168, ; 334
	i32 237, ; 335
	i32 141, ; 336
	i32 60, ; 337
	i32 145, ; 338
	i32 2, ; 339
	i32 211, ; 340
	i32 82, ; 341
	i32 187, ; 342
	i32 216, ; 343
	i32 75, ; 344
	i32 131, ; 345
	i32 27, ; 346
	i32 9, ; 347
	i32 176, ; 348
	i32 94, ; 349
	i32 250, ; 350
	i32 138, ; 351
	i32 114, ; 352
	i32 11, ; 353
	i32 184, ; 354
	i32 105, ; 355
	i32 21, ; 356
	i32 236, ; 357
	i32 244, ; 358
	i32 263, ; 359
	i32 34, ; 360
	i32 47, ; 361
	i32 31, ; 362
	i32 225, ; 363
	i32 58, ; 364
	i32 135, ; 365
	i32 115, ; 366
	i32 259, ; 367
	i32 56, ; 368
	i32 210, ; 369
	i32 8, ; 370
	i32 78, ; 371
	i32 189, ; 372
	i32 112, ; 373
	i32 196, ; 374
	i32 234, ; 375
	i32 103, ; 376
	i32 171, ; 377
	i32 116, ; 378
	i32 250, ; 379
	i32 77, ; 380
	i32 254, ; 381
	i32 86, ; 382
	i32 256, ; 383
	i32 224, ; 384
	i32 161, ; 385
	i32 4, ; 386
	i32 26, ; 387
	i32 33, ; 388
	i32 118, ; 389
	i32 38, ; 390
	i32 18, ; 391
	i32 53, ; 392
	i32 257, ; 393
	i32 179, ; 394
	i32 22, ; 395
	i32 124, ; 396
	i32 155, ; 397
	i32 236, ; 398
	i32 132, ; 399
	i32 149, ; 400
	i32 121, ; 401
	i32 29, ; 402
	i32 133, ; 403
	i32 101, ; 404
	i32 135, ; 405
	i32 154, ; 406
	i32 98, ; 407
	i32 126, ; 408
	i32 70, ; 409
	i32 73, ; 410
	i32 246, ; 411
	i32 137, ; 412
	i32 125, ; 413
	i32 72, ; 414
	i32 112, ; 415
	i32 201, ; 416
	i32 153, ; 417
	i32 254, ; 418
	i32 119, ; 419
	i32 231, ; 420
	i32 128, ; 421
	i32 196, ; 422
	i32 134, ; 423
	i32 202, ; 424
	i32 78, ; 425
	i32 47, ; 426
	i32 74, ; 427
	i32 64, ; 428
	i32 204, ; 429
	i32 99, ; 430
	i32 85, ; 431
	i32 44, ; 432
	i32 62, ; 433
	i32 245, ; 434
	i32 199, ; 435
	i32 38, ; 436
	i32 41, ; 437
	i32 260, ; 438
	i32 161, ; 439
	i32 99, ; 440
	i32 228, ; 441
	i32 202, ; 442
	i32 136, ; 443
	i32 22, ; 444
	i32 66, ; 445
	i32 126, ; 446
	i32 76, ; 447
	i32 243, ; 448
	i32 165, ; 449
	i32 157, ; 450
	i32 7, ; 451
	i32 50, ; 452
	i32 145, ; 453
	i32 195, ; 454
	i32 140, ; 455
	i32 101, ; 456
	i32 124, ; 457
	i32 121, ; 458
	i32 143, ; 459
	i32 40, ; 460
	i32 69, ; 461
	i32 42, ; 462
	i32 185, ; 463
	i32 175, ; 464
	i32 165, ; 465
	i32 74, ; 466
	i32 166, ; 467
	i32 207, ; 468
	i32 212, ; 469
	i32 128, ; 470
	i32 259, ; 471
	i32 69, ; 472
	i32 170, ; 473
	i32 180, ; 474
	i32 205, ; 475
	i32 152, ; 476
	i32 46, ; 477
	i32 109, ; 478
	i32 49, ; 479
	i32 97, ; 480
	i32 32, ; 481
	i32 25, ; 482
	i32 167, ; 483
	i32 24, ; 484
	i32 139, ; 485
	i32 79, ; 486
	i32 55, ; 487
	i32 12, ; 488
	i32 219, ; 489
	i32 18, ; 490
	i32 140, ; 491
	i32 176, ; 492
	i32 15, ; 493
	i32 17, ; 494
	i32 123, ; 495
	i32 194, ; 496
	i32 88, ; 497
	i32 150, ; 498
	i32 24, ; 499
	i32 35, ; 500
	i32 80, ; 501
	i32 181, ; 502
	i32 148, ; 503
	i32 81, ; 504
	i32 230, ; 505
	i32 216, ; 506
	i32 43, ; 507
	i32 28, ; 508
	i32 244, ; 509
	i32 242, ; 510
	i32 108, ; 511
	i32 188, ; 512
	i32 111, ; 513
	i32 9, ; 514
	i32 255, ; 515
	i32 45, ; 516
	i32 162, ; 517
	i32 149, ; 518
	i32 224, ; 519
	i32 39, ; 520
	i32 17, ; 521
	i32 147, ; 522
	i32 10, ; 523
	i32 234, ; 524
	i32 131, ; 525
	i32 252, ; 526
	i32 95 ; 527
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

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
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
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
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" }

; Metadata
!llvm.module.flags = !{!0, !1, !7, !8, !9, !10}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ df9aaf29a52042a4fbf800daf2f3a38964b9e958"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"branch-target-enforcement", i32 0}
!8 = !{i32 1, !"sign-return-address", i32 0}
!9 = !{i32 1, !"sign-return-address-all", i32 0}
!10 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
