# VST Plugin Info

This library can retrieve information about VST plugins. Both Vst2 and Vst3 are supported.

The API for both VST2 and VST3 plugins is very similar, although the properties differ.

## Usage

For retrieving VST2 information:

```csharp
using Jacobi.VstPluginInfo;

// File path to the plugin (dll) to query
var pluginPath = ...;

if (Vst2PluginInfo.TryGetPluginInfo(pluginPath, out var pluginInfo))
{
    // it is a VST2 plugin, you can use the pluginInfo
    Console.WriteLine(pluginInfo.Name)
}
else
{
    // it is not a VST2 plugin!
}
```

For retrieving VST3 information:

```csharp
using Jacobi.VstPluginInfo;

// File path to the plugin (dll) to query
var pluginPath = ...;

if (Vst3PluginInfo.TryGetPluginInfo(pluginPath, out var pluginInfo))
{
    // it is a VST3 plugin, you can use the pluginInfo
    Console.WriteLine(pluginInfo.Name)
}
else
{
    // it is not a VST3 plugin!
}
```

## Plugin Scanner

The plugin scanner is a command line tool that is started with a base path and will find all `.dll` and `.vst3` files recursively and attempt to load them and print a status line to the console. If the plugin could be loaded an extra line with some plugin data is printed.

```cmd
Jacobi.VstPluginInfo.Scanner "C:\Program Files\Vst Plugins"
```

> Use quotes for file paths that contain spaces.
