using System.Collections.Generic;
using System.Collections.ObjectModel;
using WifiScannerLib;

namespace WifiScannerTri.ViewModels
{
    public class WifiInfoViewModel
    {
        public WifiInfoViewModel(IEnumerable<WifiInfoItem> _WifiItems)
        { WifiInfoItems = new ObservableCollection<WifiInfoItem>(_WifiItems); }

        public ObservableCollection<WifiInfoItem> WifiInfoItems { get; set; } =
            new ObservableCollection<WifiInfoItem>()
            {
                new WifiInfoItem() { BSSID="034c2cba1", SSID="Ur mom"},
                new WifiInfoItem() { BSSID="093bc2a1b", SSID="Ur other mom"},
            };

        public string TempTest { get; set; } = "Hello there";
    }
}
