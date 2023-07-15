using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.VstPluginInfo;

internal static partial class NativeMethods
{
    // .NET7
    //[LibraryImport("kernel32.dll", EntryPoint = "LoadLibraryW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    //public static partial IntPtr LoadLibrary(string libraryPath);

    //[LibraryImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
    //public static partial IntPtr GetProcAddress(IntPtr hModule, string procedure);

    //[LibraryImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false)]
    //public static partial UInt32 GetLastError();

    // .NET6
    [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr LoadLibrary(string libraryPath);

    [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr handle);

    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true, CharSet = CharSet.Ansi)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedure);

    [Flags]
    public enum FORMAT_MESSAGE : uint
    {
        ALLOCATE_BUFFER = 0x00000100,
        IGNORE_INSERTS = 0x00000200,
        FROM_SYSTEM = 0x00001000,
        ARGUMENT_ARRAY = 0x00002000,
        FROM_HMODULE = 0x00000800,
        FROM_STRING = 0x00000400
    }

    [DllImport("kernel32.dll")]
    public static extern int FormatMessage(FORMAT_MESSAGE dwFlags, IntPtr lpSource, int dwMessageId, uint dwLanguageId, out StringBuilder msgOut, int nSize, IntPtr arguments);
}
