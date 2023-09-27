using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using WifiScannerTri.ViewModels;
using WifiScannerTri.Views;
using WifiScannerLib;

namespace WifiScannerTri;
public partial class App : Application
{
    public static IWS? IWScanner;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(IWScanner)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(IWScanner)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
