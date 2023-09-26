using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using WifiScannerLib;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerTri.ViewModels
{
    public class WifiInfoViewModel
    {
        public WifiInfoViewModel(List<WifiInfoItem> _WifiItems)
        {
            List<WifiInfoItem> Temp = _WifiItems;

            try
            {
                WifiInfoItems = new ObservableCollection<WifiInfoItem>(_WifiItems);
            }
            catch (System.Exception EXC)
            {throw EXC;}

            SDD.WriteLine(WifiInfoItems.Count);
        }

        public WifiInfoViewModel()
        { }

        public ObservableCollection<WifiInfoItem> WifiInfoItems { get; set; } = new ObservableCollection<WifiInfoItem>();
    }
}
