using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo;

internal static partial class NativeMethods
{
    [LibraryImport("kernel32.dll", EntryPoint = "LoadLibraryW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    public static partial IntPtr LoadLibrary(string libraryPath);

    [LibraryImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr GetProcAddress(IntPtr hModule, string procedure);

    [LibraryImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false)]
    public static partial UInt32 GetLastError();
}
