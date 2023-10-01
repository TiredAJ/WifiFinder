using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDD = System.Diagnostics.Debug;

using WifiScannerLib;
using WifiScannerPedwar.Models;
using DynamicData;
using Avalonia.Controls;

namespace WifiScannerPedwar.ViewModels
{
    public class WifiViewModel : ViewModelBase
    {
        public WifiViewModel(IEnumerable<AvWifiInfoItem> _Items)
        {
            //SDD.WriteLine(_Items.Count());

            Items = new ObservableCollection<AvWifiInfoItem>(_Items);
        }

        public void AddItems(IEnumerable<AvWifiInfoItem> _Wifis)
        {
            int Index;

            //Items.Clear();

            //foreach (var W in _Wifis)
            //{Items.Add(W);}

            foreach (var W in _Wifis)
            {
                Index = FindIndex(W);

                if (Index != -1)
                { Items[Index] = W; }
                else
                { Items.Add(W); }
            }
        }

        private int FindIndex(AvWifiInfoItem _WII)
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                if (Items[i] == _WII)
                {return i;}
            }

            return -1;
        }

        //"list items"
        public ObservableCollection<AvWifiInfoItem> Items { get; private set;}
    }
}
