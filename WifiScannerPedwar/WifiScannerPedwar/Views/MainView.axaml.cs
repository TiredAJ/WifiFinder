using Avalonia.Controls;
using MsBox.Avalonia;
using WifiScannerPedwar.ViewModels;

using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    MainViewModel MVM = new MainViewModel();

    public MainView()
    {
        InitializeComponent();

        //WifiService.Storage = TopLevel.GetTopLevel(btn_ScanNow).StorageProvider;

        btn_ScanNow.Click += Snapshot_Click;
    }

    private async void Snapshot_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainViewModel.ISP = TopLevel.GetTopLevel(btn_ScanNow).StorageProvider;

        if (!MainViewModel.IsInitialised)
        {
            var box = MessageBoxManager
                                        .GetMessageBoxStandard("SErvice error", "Service is not initialised!");

            var result = await box.ShowAsync();
        }

        MVM.TriggerScan();

        SDD.WriteLine("Snapshot taken!");
    }
}
