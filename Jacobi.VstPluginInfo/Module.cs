using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.VstPluginInfo;

internal abstract class Module
{
    protected static IntPtr LoadLibrary(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Could not find file '{path}'.");

        var hLib = NativeMethods.LoadLibrary(path);
        if (hLib == IntPtr.Zero)
        {
            var errTxt = GetLastErrorMessage();
            errTxt = errTxt?.Replace("%1", path);
            throw new ArgumentException(errTxt ?? "Could not load plugin.");

            //if (Marshal.GetLastWin32Error() == 193)    // bad file format
            //    throw new BadImageFormatException("Could not load the plugin because it is not 64-bit.");

            //throw new ArgumentException("Could not load the plugin");
        }

        return hLib;
    }

    protected static T? ToDelegate<T>(IntPtr procAddress)
        where T : Delegate
    {
        if (procAddress != IntPtr.Zero)
        {
            return Marshal.GetDelegateForFunctionPointer<T>(procAddress);
        }

        return null;
    }

    protected static IntPtr ToPointer<T>(T typedDelegate)
        where T : Delegate
    {
        return Marshal.GetFunctionPointerForDelegate<T>(typedDelegate);
    }

    protected static T ToInterface<T>(nint ptr)
    {
        return (T)Marshal.GetTypedObjectForIUnknown(ptr, typeof(T));
    }

    protected static string? GetLastErrorMessage()
    {
        var lastError = Marshal.GetLastWin32Error();
        if (lastError == 0)
            return null;

        var msgOut = new StringBuilder(256);
        int size = NativeMethods.FormatMessage(
            NativeMethods.FORMAT_MESSAGE.ALLOCATE_BUFFER | NativeMethods.FORMAT_MESSAGE.FROM_SYSTEM | NativeMethods.FORMAT_MESSAGE.IGNORE_INSERTS,
            IntPtr.Zero, lastError, 0, out msgOut, msgOut.Capacity, IntPtr.Zero);

        return msgOut.ToString().Trim();
    }
}
