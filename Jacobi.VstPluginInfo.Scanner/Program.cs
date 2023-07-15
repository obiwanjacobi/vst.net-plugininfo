namespace Jacobi.VstPluginInfo.Scanner;

internal static class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Specify a directory to scan...");
            return;
        }

        ScanPlugins(args[0]);
    }

    private static void ScanPlugins(string path)
    {
        foreach (var file in Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories))
        {
            ScanFile(file);
        }

        foreach (var file in Directory.EnumerateFiles(path, "*.vst3", SearchOption.AllDirectories))
        {
            ScanFile(file);
        }
    }

    private static void ScanFile(string file)
    {
        var saveColor = Console.ForegroundColor;

        try
        {
            Console.ForegroundColor = ConsoleColor.Green;

            if (Vst3PluginInfo.TryGetPluginInfo(file, out var plugin3Info))
            {
                Console.WriteLine($"File {file} is a VST3 plugin.");
                Console.ForegroundColor = saveColor;
                Console.WriteLine($"{plugin3Info.Name} {plugin3Info.Category} {plugin3Info.SubCategories} {plugin3Info.Vendor} {plugin3Info.Version}");
                return;
            }

            if (Vst2PluginInfo.TryGetPluginInfo(file, out var plugin2Info))
            {
                Console.WriteLine($"File {file} is a VST2 plugin.");
                Console.ForegroundColor = saveColor;
                Console.WriteLine($"{plugin2Info.Name} {plugin2Info.ProductName} {plugin2Info.Vendor} {plugin2Info.VendorVersion}");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"File {file} is not a VST2 or VST3 plugin.");
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"File {file} resulted in error: {e.Message}");
        }

        Console.ForegroundColor = saveColor;
    }
}