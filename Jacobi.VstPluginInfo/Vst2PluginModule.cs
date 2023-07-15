using System.Diagnostics.CodeAnalysis;

namespace Jacobi.VstPluginInfo;

internal unsafe sealed class Vst2PluginModule : Module, IDisposable
{
    private readonly IntPtr _handle;
    private readonly Vst2Plugin* _plugin;
    private readonly Vst2PluginCommand _command;
    private readonly UnmanagedBuffer _buffer;

    private Vst2PluginModule(IntPtr handle, Vst2Plugin* plugin, Vst2PluginCommand command)
    {
        _handle = handle;
        _plugin = plugin;
        _command = command;
        _buffer = new UnmanagedBuffer(256);
    }

    public int ProgramCount => _plugin->programCount;
    public int ParameterCount => _plugin->parameterCount;
    public int InputCount => _plugin->inputCount;
    public int OutputCount => _plugin->outputCount;
    public Vst2PluginFlags Flags => _plugin->flags;

    public string Name
    {
        get
        {
            _command(_plugin, Vst2PluginCommands.PluginGetName, 0, IntPtr.Zero, _buffer.GetPointer(), 0f);
            return _buffer.ToString();
        }
    }

    public string ProductName
    {
        get
        {
            _command(_plugin, Vst2PluginCommands.ProductGetString, 0, IntPtr.Zero, _buffer.GetPointer(), 0f);
            return _buffer.ToString();
        }
    }

    public string VendorString
    {
        get
        {
            _command(_plugin, Vst2PluginCommands.VendorGetString, 0, IntPtr.Zero, _buffer.GetPointer(), 0f);
            return _buffer.ToString();
        }
    }

    public int VendorVersion
    {
        get
        {
            return (int)_command(_plugin, Vst2PluginCommands.VendorGetVersion, 0, IntPtr.Zero, IntPtr.Zero, 0f);
        }
    }

    public int VstVersion
    {
        get
        {
            return (int)_command(_plugin, Vst2PluginCommands.GetVstVersion, 0, IntPtr.Zero, IntPtr.Zero, 0f);
        }
    }

    public void Close()
    {
        _command(_plugin, Vst2PluginCommands.Close, 0, IntPtr.Zero, IntPtr.Zero, 0f);
    }

    public void Dispose()
    {
        Close();
        _buffer.Dispose();
        NativeMethods.FreeLibrary(_handle);
        GC.SuppressFinalize(this);
    }

    public static bool TryLoadPlugin(string pluginPath, [NotNullWhen(true)] out Vst2PluginModule? module)
    {
        var hLib = LoadLibrary(pluginPath);

        var procAddress = NativeMethods.GetProcAddress(hLib, "VSTPluginMain");
        if (procAddress == IntPtr.Zero)
            procAddress = NativeMethods.GetProcAddress(hLib, "main");

        if (procAddress != IntPtr.Zero)
        {
            var pHost = ToPointer<Vst2HostCommand>(Vst2HostCommand);
            var proc = ToDelegate<Vst2PluginMain>(procAddress);

            var vstPlugin = proc!(pHost);
            if (vstPlugin is not null)
            {
                var pluginCmd = ToDelegate<Vst2PluginCommand>(vstPlugin->command);

                module = new Vst2PluginModule(hLib, vstPlugin, pluginCmd!);
                return true;
            }

            NativeMethods.FreeLibrary(hLib);

            throw new NotSupportedException(
                $"The VST2 plugin '{pluginPath}' loaded but failed to initialize, which can be due to this not implementing a full VST host.");
        }

        NativeMethods.FreeLibrary(hLib);
        module = null;
        return false;
    }

    private static IntPtr Vst2HostCommand(Vst2Plugin* plugin, Vst2HostCommands hostCommand, Int32 index, IntPtr value, IntPtr ptr, float opt)
    {
        switch (hostCommand)
        {
            // some plugins check the version of the host.
            case Vst2HostCommands.Version:
                return new IntPtr(2400);

            default:
                return IntPtr.Zero;
        }
    }
}
