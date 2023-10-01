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
        public WifiViewModel(IEnumerable<WifiInfoItem> _Items)
        {
            //SDD.WriteLine(_Items.Count());

            Items = new ObservableCollection<WifiInfoItem>(_Items);
        }

        public void AddItems(IEnumerable<WifiInfoItem> _Wifis)
        {
            int Index;

            //Items.Clear();

            //SDD.WriteLine("wtf is going on?");

            //Items.AddOrInsertRange(_Wifis, 0);

            //foreach (var W in _Wifis)
            //{ Items.Add(W.Clone()); }

            foreach (WifiInfoItem W in _Wifis.ToList())
            {
                Index = FindIndex(W);

                if (Index != -1)
                { Items[Index] = W; }
                else
                { Items.Add(W); }
            }
        }

        private int FindIndex(WifiInfoItem _WII)
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                if (Items[i].BSSID == _WII.BSSID)
                {return i;}
            }

            return -1;
        }

        //"list items"
        public ObservableCollection<WifiInfoItem> Items { get; private set;}

        public int Update { get; set; }
    }
}
