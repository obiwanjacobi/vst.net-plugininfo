using System.Runtime.InteropServices;

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
            if (NativeMethods.GetLastError() == 193)    // bad file format
                throw new BadImageFormatException("Could not load the plugin.");

            throw new ArgumentException("Could not load the plugin");
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
}
