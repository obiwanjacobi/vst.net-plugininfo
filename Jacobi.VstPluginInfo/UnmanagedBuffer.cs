using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo;

internal unsafe sealed class UnmanagedBuffer : IDisposable
{
    private readonly IntPtr _buffer;

    public UnmanagedBuffer(int bufferSizeInBytes)
    {
        _buffer = Marshal.AllocHGlobal(bufferSizeInBytes);
    }

    public IntPtr GetPointer()
    {
        _str = null;
        return _buffer;
    }

    private string? _str;

    public override string ToString()
    {
        _str ??= new string((sbyte*)_buffer.ToPointer());
        return _str;
    }

    public void Dispose()
    {
        Marshal.FreeHGlobal(_buffer);
        GC.SuppressFinalize(this);
    }

    ~UnmanagedBuffer()
    {
        Marshal.FreeHGlobal(_buffer);
    }
}
