using Avalonia.Controls;
using System.Collections.Generic;
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
    }
}
