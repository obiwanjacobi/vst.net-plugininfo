using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate IntPtr Vst3GetPluginFactory();
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate bool Vst3InitDll();
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void Vst3ExitDll();
