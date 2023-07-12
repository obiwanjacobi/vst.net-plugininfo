namespace Jacobi.VstPluginInfo.Tests;

public class Vst2PluginTests
{
    private static Vst2PluginInfo GetPluginInfo(string path)
    {
        Vst2PluginInfo.TryGetPluginInfo(path, out var pluginInfo)
            .Should().BeTrue();

        return pluginInfo!;
    }

    [Fact]
    public void Vst2TryGetPluginInfo()
    {
        const string plugin = @"C:\Program Files\VSTPlugins\ReaPlugs\reacomp-standalone.dll";

        var pluginInfo = GetPluginInfo(plugin);

        pluginInfo.Should().NotBeNull();
        pluginInfo.FileName.Should().Be("reacomp-standalone.dll");
        pluginInfo.ProgramCount.Should().BeGreaterThan(0);
        pluginInfo.ParameterCount.Should().BeGreaterThan(0);
        pluginInfo.InputCount.Should().BeGreaterThan(0);
        pluginInfo.OutputCount.Should().BeGreaterThan(0);
    }
}