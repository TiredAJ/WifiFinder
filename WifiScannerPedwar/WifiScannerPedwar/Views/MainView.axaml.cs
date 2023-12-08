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
        btn_SaveData.Click += Btn_SaveData_Click;
    }

    private void Btn_SaveData_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    { MVM.SaveData(TopLevel.GetTopLevel(btn_SaveData).StorageProvider); }

    private async void Snapshot_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!MainViewModel.IsInitialised)
        {
            var box = MessageBoxManager
                                        .GetMessageBoxStandard("Service error", "Service is not initialised!");

            var result = await box.ShowAsync();
        }

        MVM.TriggerScan();

        SDD.WriteLine("Snapshot taken!");
    }
}
