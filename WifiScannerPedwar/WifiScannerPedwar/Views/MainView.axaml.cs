using Avalonia.Controls;

using MsBox.Avalonia;

using WifiScannerPedwar.ViewModels;

using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    MainViewModel MVM = new MainViewModel();

    public int Index = 0;

    public MainView()
    {
        InitializeComponent();

        //WifiService.Storage = TopLevel.GetTopLevel(btn_ScanNow).StorageProvider;

        btn_ScanNow.Click += Snapshot_Click;
        btn_SaveData.Click += Btn_SaveData_Click;
        btn_ClearData.Click += Btn_ClearData_Click;
    }

    private void Btn_ClearData_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MVM.ClearData();

        Index = 0;

        txt_Index.Text = $"Index {Index}";
    }

    private void Btn_SaveData_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MVM.SaveData(TopLevel.GetTopLevel(btn_SaveData).StorageProvider);

        Index = 0;

        txt_Index.Text = $"Index {Index}";
    }

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

        Index++;

        txt_Index.Text = $"Index {Index}";
    }
}
