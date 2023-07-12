using System.Diagnostics.CodeAnalysis;
using Jacobi.VstPluginInfo.Vst3;

namespace Jacobi.VstPluginInfo;

internal sealed class Vst3PluginModule : Module, IDisposable
{
    private readonly Vst3ExitDll _exitDll;
    private readonly PFactoryInfo _factoryInfo;
    private readonly List<PClassInfo2> _classInfos = new();

    private Vst3PluginModule(IPluginFactory pluginFactory, IPluginFactory2? pluginFactory2, Vst3ExitDll exitDll)
    {
        _exitDll = exitDll;

        _factoryInfo = new PFactoryInfo();
        pluginFactory.GetFactoryInfo(ref _factoryInfo);

        if (pluginFactory2 is not null)
        {
            for (int i = 0; i < pluginFactory2.CountClasses(); i++)
            {
                var classInfo = new PClassInfo2();
                pluginFactory2.GetClassInfo2(i, ref classInfo);

                _classInfos.Add(classInfo);
            }
        }
        else
        {
            for (int i = 0; i < pluginFactory.CountClasses(); i++)
            {
                var classInfo = new PClassInfo();
                pluginFactory.GetClassInfo(i, ref classInfo);

                // copy into PClassInfo2
                var classInfo2 = new PClassInfo2
                {
                    Cardinality = classInfo.Cardinality,
                    Category = classInfo.Category,
                    ClassId = classInfo.ClassId,
                    Name = classInfo.Name
                };
                _classInfos.Add(classInfo2);
            }
        }
    }

    public string Vendor => _factoryInfo.Vendor;
    public string Email => _factoryInfo.Email;
    public string Url => _factoryInfo.Url;

    public string Name
    {
        get
        {
            return _classInfos
                .Select(info => info.Name)
                .FirstOrDefault(value => !String.IsNullOrEmpty(value))
                ?? String.Empty;
        }
    }

    public string Category
    {
        get
        {
            return _classInfos
                .Select(info => info.Category)
                .FirstOrDefault(value => !String.IsNullOrEmpty(value))
                ?? String.Empty;
        }
    }

    public string SubCategories
    {
        get
        {
            return _classInfos
                .Select(info => info.SubCategories)
                .FirstOrDefault(value => !String.IsNullOrEmpty(value))
                ?? String.Empty;
        }
    }

    public string Version
    {
        get
        {
            return _classInfos
                .Select(info => info.Version)
                .FirstOrDefault(value => !String.IsNullOrEmpty(value))
                ?? String.Empty;
        }
    }

    public string SdkVersion
    {
        get
        {
            return _classInfos
                .Select(info => info.SdkVersion)
                .FirstOrDefault(value => !String.IsNullOrEmpty(value))
                ?? String.Empty;
        }
    }

    public void Dispose()
    {
        _exitDll();
    }

    public static bool TryLoadPlugin(string pluginPath, [NotNullWhen(true)] out Vst3PluginModule? module)
    {
        if (Directory.Exists(pluginPath))
            throw new NotSupportedException("VST3 Bundles are not supported.");

        var hLib = LoadLibrary(pluginPath);
        var getPluginFactoryProc = NativeMethods.GetProcAddress(hLib, "GetPluginFactory");

        if (getPluginFactoryProc != IntPtr.Zero)
        {
            var initDllProc = NativeMethods.GetProcAddress(hLib, "InitDll");
            var exitDllProc = NativeMethods.GetProcAddress(hLib, "ExitDll");

            var getPluginFactory = ToDelegate<Vst3GetPluginFactory>(getPluginFactoryProc);

            var unmPluginFactory = getPluginFactory!();
            var pluginFactory = ToInterface<IPluginFactory>(unmPluginFactory);
            var pluginFactory2 = ToInterface<IPluginFactory2>(unmPluginFactory);

            var initDll = ToDelegate<Vst3InitDll>(initDllProc);
            var exitDll = ToDelegate<Vst3ExitDll>(exitDllProc);

            if (initDll!())
            {
                module = new Vst3PluginModule(pluginFactory, pluginFactory2, exitDll!);
                return true;
            }
        }

        module = null;
        return false;
    }
}
