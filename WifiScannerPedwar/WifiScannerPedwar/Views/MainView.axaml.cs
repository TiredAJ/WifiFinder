using Avalonia.Controls;
using WifiScannerPedwar.Services;
using WifiScannerPedwar.ViewModels;
using WifiScannerPedwar.Services;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        WifiService.Storage = TopLevel.GetTopLevel(btn_ScanNow).StorageProvider;

        SDD.WriteLine("eyo, storey boi found!");
    }

    private void Snapshot_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SDD.WriteLine("Snapshot taken!");
    }
}
