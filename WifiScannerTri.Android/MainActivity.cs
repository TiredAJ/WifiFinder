using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using System.Threading.Tasks;
using WifiScannerLib;
using WifiScannerTri.Services;
using WifiScannerTri.ViewModels;

namespace WifiScannerTri.Android;

[Activity(
    Label = "WifiScannerTri.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        //MainViewModel.IWScanner = new WifiScannerLib.AndroidWS();

        WifiScannerTri.App.IWScanner = new WifiScannerLib.AndroidWS(this.ApplicationContext);

        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }
}
