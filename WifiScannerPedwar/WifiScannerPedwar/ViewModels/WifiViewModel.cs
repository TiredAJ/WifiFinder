using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiScannerLib;
using WifiScannerPedwar.Models;

namespace WifiScannerPedwar.ViewModels
{
    public class WifiViewModel : ViewModelBase
    {
        public WifiViewModel(IEnumerable<AvWifiInfoItem> _Items)
        {
            Items = new ObservableCollection<AvWifiInfoItem>(_Items)
            {
                new AvWifiInfoItem()
                {
                    BSSID = "bc23ba3a2343",
                    SSID = "Ur mom",
                    RSSI = -20,
                    LastUpdated = new TimeSpan(0, 0, 2),
                    Capabilities = "WiFi 6e"
                }
            };
        }

        //"list items"
        public ObservableCollection<AvWifiInfoItem> Items { get; }
    }
}
