using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using HarryPotter;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using System.Runtime.Versioning;
using System.Threading.Tasks;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main()
    {
        return BuildAvaloniaApp()
            .WithInterFont()
            .UseReactiveUI()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        _ = IconProvider.Current.Register<FontAwesomeIconProvider>();
        return AppBuilder.Configure<App>();
    }
}