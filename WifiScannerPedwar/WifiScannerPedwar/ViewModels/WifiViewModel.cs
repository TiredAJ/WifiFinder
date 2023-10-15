using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using Avalonia.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDD = System.Diagnostics.Debug;

using WifiScannerLib;
using DynamicData;
using Avalonia.Controls;

namespace WifiScannerPedwar.ViewModels
{
    public class WifiViewModel : ViewModelBase
    {
        //default constructor
        public WifiViewModel(IEnumerable<WifiInfoItem> _Items)
        {
            //Items = new Avalonia.Collections.AvaloniaDictionary<string, WifiInfoItem>();
            Items = new ObservableCollection<WifiInfoItem>();

            AddItems(_Items);

            //Items = new ObservableCollection<WifiInfoItem>(_Items);
        }

        public WifiViewModel()
        {
            //Items = new Avalonia.Collections.AvaloniaDictionary<string, WifiInfoItem>();
            Items = new ObservableCollection<WifiInfoItem>();
        }

        //adds items to the Items collection
        public void AddItems(IEnumerable<WifiInfoItem> _Wifis)
        {
            //int Index;

            //loops through given collection
            foreach (WifiInfoItem W in _Wifis.ToList())
            {
                if (!Items.TryAdd(W.BSSID, W))
                {Items[W.BSSID] = W;}

                ////checks if the entry already exists
                //Index = FindIndex(W);

                ////checks the result and updates or adds accordingly
                //if (Index != -1)
                //{ Items[Index] = W; }
                //else
                //{ Items.Add(W); }
            }
        }

        //holds the items
        public ObservableCollection<WifiInfoItem> Items { get; private set;}

        //public ObservableConcurrentDictionary<string, WifiInfoItem> Items { get; private set; }

        //public Avalonia.Collections.AvaloniaDictionary<string, WifiInfoItem> Items { get; private set; }

        //for future?
        public int Update { get; set; }

        //notes for later AJ:
        //fix the shit with the scanner, u know what I'm talking about:
        //  - have the scanner hold an internal concurrent dictionary which is updated /independently/
        //      data is obtained by reading the dictionary
        //  - replace IEnumerable with dictionary.tolist() or something and loop through that
        //  - get data into observable collection?
        //  - map the whole program, u keep getting lost
    }
}
