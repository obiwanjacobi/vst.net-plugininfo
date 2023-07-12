using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo.Vst3;

internal static class Platform
{
#if X86
        public const int StructurePack = 8;
//#endif
//#if X64
#else
        public const int StructurePack = 16;
#endif

    public const CharSet CharacterSet = CharSet.Unicode;
    public const CallingConvention DefaultCallingConvention = CallingConvention.StdCall;
}