using Avalonia.Controls;
using Avalonia.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using WifiScannerPedwar.Models;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        //DRGD_Data.ItemsSource = new List<AvWifiInfoItem>() 
        //{
        //    new AvWifiInfoItem() {BSSID = "ac76b623b", RSSI = -60, SSID = "Ur Dad"}
        //};

        Dispatcher.UIThread.InvokeAsync(() =>
        this.FindControl<DataGrid>("DGRD_APData").Columns[2].Sort());


        //DGRD_APData.Columns[2].Sort(ListSortDirection.Descending);
        //DGRD_APData.UpdateLayout();
    }
}
