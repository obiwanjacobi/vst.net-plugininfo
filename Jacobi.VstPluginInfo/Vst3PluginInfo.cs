using System.Diagnostics.CodeAnalysis;

namespace Jacobi.VstPluginInfo;

/// <summary>
/// Represent the plugin information for a VST3 plugin.
/// </summary>
public sealed class Vst3PluginInfo
{
    private Vst3PluginInfo(string fileName)
    {
        FileName = fileName;
    }

    /// <summary>
    /// The file name (without path) of the plugin.
    /// </summary>
    public string FileName { get; }
    
    /// <summary>The vendor (manufacturer) that created the plugin.</summary>
    public string? Vendor { get; init; }

    /// <summary>The email address to contact the vendor.</summary>
    public string? Email { get; init; }

    /// <summary>The web site url for the plugin product.</summary>
    public string? Url { get; init; }

    /// <summary>The name of the plugin.</summary>
    public string? Name { get; init; }

    /// <summary>The (main) category that identifies the type of plugin.</summary>
    public string? Category { get; init; }

    /// <summary>The sub categories for this plugin.</summary>
    public string? SubCategories { get; init; }

    /// <summary>The plugin (vendor) version.</summary>
    public string? Version { get; init; }

    /// <summary>The VST SDK version this plugin was written for.</summary>
    public string? SdkVersion { get; init; }

    /// <summary>
    /// Attempts to load the plugin pointed to by <paramref name="pluginPath"/> as VST3 plugin and retrieve its information.
    /// </summary>
    /// <param name="pluginPath">The file path that points to the plugin to query.</param>
    /// <param name="pluginInfo">Receives the plugin information if successful, otherwise set to null.</param>
    /// <returns>Returns true when the plugin was successfully loaded and the <paramref name="pluginInfo"/> was filled.</returns>
    public static bool TryGetPluginInfo(string pluginPath, [NotNullWhen(true)] out Vst3PluginInfo? pluginInfo)
    {
        if (Vst3PluginModule.TryLoadPlugin(pluginPath, out var module))
        {
            pluginInfo = new Vst3PluginInfo(Path.GetFileName(pluginPath))
            {
                Category = module.Category,
                Email = module.Email,
                Name = module.Name,
                SdkVersion = module.SdkVersion,
                SubCategories = module.SubCategories,
                Url = module.Url,
                Vendor = module.Vendor,
                Version = module.Version
            };

            module.Dispose();
            return true;
        }

        pluginInfo = null;
        return false;
    }
}
