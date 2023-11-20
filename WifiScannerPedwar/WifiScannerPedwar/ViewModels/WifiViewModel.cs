using System.Collections.Generic;
using System.Linq;

using WifiScannerLib;

namespace WifiScannerPedwar.ViewModels
{
    public class WifiViewModel : ViewModelBase
    {
        //default constructor
        public WifiViewModel(IEnumerable<WifiInfoItem> _Items)
        {
            Items = new Dictionary<string, WifiInfoItem>();

            AddItems(_Items);
        }

        public WifiViewModel()
        {
            //Items = new Avalonia.Collections.AvaloniaDictionary<string, WifiInfoItem>();
            Items = new Dictionary<string, WifiInfoItem>();
        }

        //adds items to the Items collection
        public void AddItems(IEnumerable<WifiInfoItem> _Wifis)
        {
            //loops through given collection
            foreach (WifiInfoItem W in _Wifis.ToList())
            {
                if (!Items.TryAdd(W.BSSID, W))
                { Items[W.BSSID] = W; }
            }
        }

        private Dictionary<string, WifiInfoItem> Items = new Dictionary<string, WifiInfoItem>();

        //for future?
        public int NoAccessPoints { get; set; }

        //notes for later AJ:
        //fix the shit with the scanner, u know what I'm talking about:
        //  - have the scanner hold an internal concurrent dictionary which is updated /independently/
        //      data is obtained by reading the dictionary
        //  - replace IEnumerable with dictionary.tolist() or something and loop through that
        //  - get data into observable collection?
        //  - map the whole program, u keep getting lost
    }
}
