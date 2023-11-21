using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using WifiScannerLib;
using WifiScannerPedwar.ViewModels;
using WifiScannerPedwar.Views;

namespace WifiScannerPedwar;

public partial class App : Application
{
    //global IWS
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
            {//here...
                DataContext = new MainViewModel(IWScanner)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {//...and here, IWS is passed into the view model
                DataContext = new MainViewModel(IWScanner)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
