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
        }

        //"list items"
        public ObservableCollection<AvWifiInfoItem> Items { get; }
    }
}
