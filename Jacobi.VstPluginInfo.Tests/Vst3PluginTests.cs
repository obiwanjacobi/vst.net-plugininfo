namespace Jacobi.VstPluginInfo.Tests;

public class Vst3PluginTests
{
    private static Vst3PluginInfo GetPluginInfo(string path)
    {
        Vst3PluginInfo.TryGetPluginInfo(path, out var pluginInfo)
            .Should().BeTrue();

        return pluginInfo!;
    }

    [Fact]
    public void Vst3TryGetPluginInfo()
    {
        const string plugin = @"C:\Program Files\VSTPlugins\bucketone_1_0_0\bucketone.vst3";

        var pluginInfo = GetPluginInfo(plugin);

        pluginInfo.Should().NotBeNull();
        pluginInfo.FileName.Should().Be("bucketone.vst3");
        pluginInfo.Category.Should().NotBeEmpty();
        pluginInfo.Email.Should().NotBeEmpty();
        pluginInfo.Name.Should().NotBeEmpty();
        pluginInfo.SdkVersion.Should().NotBeEmpty();
        pluginInfo.SubCategories.Should().NotBeEmpty();
        pluginInfo.Url.Should().NotBeEmpty();
        pluginInfo.Vendor.Should().NotBeEmpty();
        pluginInfo.Version.Should().NotBeEmpty();
    }
}