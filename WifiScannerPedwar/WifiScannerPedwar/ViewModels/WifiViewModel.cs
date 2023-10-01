using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDD = System.Diagnostics.Debug;

using WifiScannerLib;
using WifiScannerPedwar.Models;

namespace WifiScannerPedwar.ViewModels
{
    public class WifiViewModel : ViewModelBase
    {
        public WifiViewModel(IEnumerable<AvWifiInfoItem> _Items)
        {
            //SDD.WriteLine(_Items.Count());

            Items = new ObservableCollection<AvWifiInfoItem>(_Items);

            Items.Add
            (new AvWifiInfoItem()
            {
                BSSID = "bc23ba3a2343",
                SSID = "Ur mom",
                RSSI = new Random((int)DateTime.Now.Ticks).Next(-40, -30),
                LastUpdated = new TimeSpan(0, 0, 2),
                Capabilities = "WiFi 6e"
            });
        }

        //"list items"
        public ObservableCollection<AvWifiInfoItem> Items { get; }
    }
}
