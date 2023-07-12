using System.Diagnostics.CodeAnalysis;

namespace Jacobi.VstPluginInfo;

/// <summary>
/// Represent the plugin information for a VST2 plugin.
/// </summary>
public sealed class Vst2PluginInfo
{
    private Vst2PluginInfo(string fileName)
    {
        FileName = fileName;
    }

    /// <summary>
    /// The file name (without path) of the plugin.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// The number of programs this plugin supports.
    /// </summary>
    public Int32 ProgramCount { get; init; }
    
    /// <summary>The number of parameters the plugin supports.</summary>
    public Int32 ParameterCount { get; init; }
    
    /// <summary>The number of audio inputs the plugin supports.</summary>
    public Int32 InputCount { get; init; }
    
    /// <summary>The number of audio outputs the plugin supports.</summary>
    public Int32 OutputCount { get; init; }
    
    /// <summary>Plugin feature flags.</summary>
    public Vst2PluginFlags Flags { get; init; }

    /// <summary>The name of the plugin.</summary>
    public string? Name { get; init; }
    
    /// <summary>The product name the plugin is part of.</summary>
    public string? ProductName { get; init; }
    
    /// <summary>The vendor (manufacturer) that created the plugin.</summary>
    public string? Vendor { get; init; }
    
    /// <summary>The plugin (vendor) version.</summary>
    public int VendorVersion { get; init; }
    
    /// <summary>The VST SDK version this plugin was written for.</summary>
    public int VstVersion { get; init; }

    /// <summary>
    /// Attempts to load the plugin pointed to by <paramref name="pluginPath"/> as VST2 plugin and retrieve its information.
    /// </summary>
    /// <param name="pluginPath">The file path that points to the plugin to query.</param>
    /// <param name="pluginInfo">Receives the plugin information if successful, otherwise set to null.</param>
    /// <returns>Returns true when the plugin was successfully loaded and the <paramref name="pluginInfo"/> was filled.</returns>
    public static bool TryGetPluginInfo(string pluginPath, [NotNullWhen(true)] out Vst2PluginInfo? pluginInfo)
    {
        if (Vst2PluginModule.TryLoadPlugin(pluginPath, out var module))
        {
            pluginInfo = new Vst2PluginInfo(Path.GetFileName(pluginPath))
            {
                ProgramCount = module.ProgramCount,
                ParameterCount = module.ParameterCount,
                InputCount = module.InputCount,
                OutputCount = module.OutputCount,
                Flags = module.Flags,

                Name = module.Name,
                ProductName = module.ProductName,
                Vendor = module.VendorString,
                VendorVersion = module.VendorVersion,
                VstVersion = module.VstVersion
            };

            module.Dispose();
            return true;
        }

        pluginInfo = null;
        return false;
    }
}
