using Avalonia.Controls;
using Avalonia.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using WifiScannerLib;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        //sets the default sorting (Rssi, best > worst signal)
        Dispatcher.UIThread.InvokeAsync(() =>
        this.FindControl<DataGrid>("DGRD_APData").Columns[2].Sort());

        BTN_Snapshot.ClickMode = ClickMode.Press;

        BTN_Snapshot.Click += Snapshot_Click;

        DGRD_APData.SelectionChanged += SelectionChanged;
    }

    private void SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        //int Index = DGRD_APData.SelectedIndex;
        WifiInfoItem? WII = DGRD_APData.SelectedItem as WifiInfoItem;

        if (WII != null)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            { 
                TXT_BSSID.Text = WII.BSSID;
                TXT_SSID.Text = WII.SSID;
                TXT_Strength.Text = WII.RSSI;
            });
        }

        SDD.WriteLine($"Selected item: {DGRD_APData.SelectedItem}");
    }

    private void Snapshot_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SDD.WriteLine("Snapshot taken!");
    }
}
