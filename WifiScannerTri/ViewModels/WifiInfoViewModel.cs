using System.Collections.Generic;
using System.Collections.ObjectModel;
using WifiScannerLib;

namespace WifiScannerTri.ViewModels
{
    public class WifiInfoViewModel
    {
        public WifiInfoViewModel(IEnumerable<WifiInfoItem> _WifiItems)
        { WifiInfoItems = new ObservableCollection<WifiInfoItem>(_WifiItems); }

        public ObservableCollection<WifiInfoItem> WifiInfoItems { get; internal set; }
    }
}
