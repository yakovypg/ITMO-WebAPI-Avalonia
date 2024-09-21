using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using StarWars;
using System.Runtime.Versioning;
using System.Threading.Tasks;

[assembly: SupportedOSPlatform("browser")]
internal sealed partial class Program
{
	private static Task Main(string[] args)
	{
		return BuildAvaloniaApp()
			.WithInterFont()
			.UseReactiveUI()
			.StartBrowserAppAsync("out");
	}

	public static AppBuilder BuildAvaloniaApp()
	{
		return AppBuilder.Configure<App>();
	}
}